using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public enum PlayerState
    {
        RUNNING,
        FALLING,
        FALLTOHANG,
        HANGING,
        ROLLING,
        HURDLING
    }

    public Canvas crosshairUI;

    public PlayerState state;
    Animator _animator;
    public Camera _camera;
    public Camera _aimCamera;
    Rigidbody _rb;
    public Transform model;

    public float speed = 4.0f;
    public float jumpPower = 5.0f;
    float h;
    float v;
    public bool toggleCameraRotation;

    Vector3 dir;
    Vector3 characterRotation;
    Vector3 move = Vector3.zero;

    public float smoothness = 10f;

    Vector3 playerVelocity;
    bool isGrounded;
    bool jumpDown;

    bool isTurn;
    bool backDown;
    bool isRoll;
    bool isHurdle;
    public Transform hurdlePosition;
    AnimationCallback _animCallBack;

    IEnumerator fallToHang;
    LayerMask hangable;
    void Start()
    {
        hangable = LayerMask.GetMask("Hangable");
        state = PlayerState.RUNNING;
        _rb = GetComponent<Rigidbody>();
        _animator = model.GetComponent<Animator>();
        _animCallBack = model.GetComponent<AnimationCallback>();
        isTurn = false;
        isRoll = false;
        isHurdle = false;
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case PlayerState.RUNNING: { InputMovement(); } break;
            case PlayerState.FALLING: { Falling(); } break;
            case PlayerState.FALLTOHANG: { TurnFallToHang(); } break;
            case PlayerState.HANGING: { Hang(); } break; 
            case PlayerState.ROLLING: { Roll(); } break;
            case PlayerState.HURDLING: { Hurdle(); } break;
        }

        isGrounded = IsCheckGrounded();
        isHurdle = IsCheckHurdling();
        if (isGrounded)
        {
            _animator.SetBool("isJump", false);
            if (isRoll)
            {
                Debug.Log("state Rolling");
                state = PlayerState.ROLLING;
            }
            else
            {
                state = PlayerState.RUNNING;
            }
        }else if(state == PlayerState.RUNNING)
        {
            state = PlayerState.FALLING;
        }
        _rb.useGravity = !((state == PlayerState.HANGING) || (state == PlayerState.FALLTOHANG));

        IsRoll();
        jumpDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("isZoom", true);
            crosshairUI.gameObject.SetActive(true);
            toggleCameraRotation = true; //에임시
            _camera.enabled = false;
            _aimCamera.enabled = true;
        }
        else
        {
            _animator.SetBool("isZoom", false);
            crosshairUI.gameObject.SetActive(false);
            toggleCameraRotation = false; //평상시
            _camera.enabled = true;
            _aimCamera.enabled = false;
        }
        if (!jumpDown)
        {
            jumpDown = Input.GetButtonDown("Jump");
        }

        backDown = Input.GetKey(KeyCode.S);
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //GoHangPosition();
        //Debug.Log(state);
    }

    private void LateUpdate()
    {
        PlayerRotation();
    }

    void InputMovement()
    {
        Vector3 postVelo = _rb.velocity;
        move = new Vector3(h, 0, v);
        move = _camera.transform.TransformDirection(move);
        move = Vector3.Scale(move, new Vector3(1, 0, 1));
        move = move.normalized;
        playerVelocity = move * speed;
        playerVelocity.y = postVelo.y;

        characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));

        if(jumpDown)
        {
            playerVelocity.y = jumpPower;

            if (isHurdle)
            {
                _animator.SetBool("isHurdle", true);
            }
            state = PlayerState.FALLING;

        }
        

        _rb.velocity = playerVelocity;

        float percent = 0.5f * move.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);


    }

    void Falling()
    {
        _animator.SetBool("isJump", true);
        _animator.SetBool("isHang", false);
        _animator.SetBool("isHurdle", false );
        move = new Vector3(h, 0, v);
        move = _camera.transform.TransformDirection(move);
        if (jumpDown && Physics.Raycast(transform.position, transform.forward, 0.3f, 1<<8))
        {
            state = PlayerState.FALLTOHANG;
            Debug.Log("goto hang");

        }
    }

    void PlayerRotation()
    {
        if(state != PlayerState.HANGING)
        {
            if (toggleCameraRotation)//줌 했을 시 즉시
            {
                Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));//카메라 방향으로 캐릭터 회전
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
            }
            else
            {
                if (move.magnitude > 0.05f)//줌 하지 않고 움직일 시
                {

                    characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(characterRotation), Time.deltaTime * smoothness);
                }
            }
        }

    }

    void Hang()
    {
        //_rb.useGravity = false;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1f, hangable))
        {
            isTurn = false;
            Vector3 playerVelocity = new Vector3(h, 0, 0);
            _rb.velocity = transform.TransformDirection(playerVelocity) * 1f;
            _animator.SetFloat("Hang Blend", h);
            if (jumpDown)
            {
                if (backDown)
                {
                    _rb.velocity = Vector3.up * 5f + hit.normal * 2f;
                }

                state = PlayerState.FALLING;
            }
        }
        else
        {
            if (!isTurn)
            {
                isTurn = true;
                transform.rotation *= Quaternion.Euler(0f, -90f * h, 0f);
                transform.position = transform.position + transform.right * h * 0.5f;
            }
        }

        



        //int layerMask;
        //layerMask = 1 << 8;
        //RaycastHit hit;
        //if(Physics.Raycast(transform.position, transform.forward, out hit, 0.3f, layerMask))
        //{
        //    Transform hangPosition = hit.collider.transform.Find("ParkourPoint");

        //    float distance = Vector3.Distance(hangPosition.position, handPosition.position);//벽에서 잡는 곳, 손과의 거리
            
        //    if (!isHang) 
        //    { 
        //        if (jumpDown && distance < 0.2f)
        //        {
        //            hangDist = handPosition.position - hangPosition.position;
        //            Debug.Log(hangDist);
        //            isHang = true;
        //            playerVelocity = Vector3.zero;
        //            _rb.useGravity = false;
        //            _animator.SetBool("isHang", true);

        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("isHang: " + isHang);
        //        if (jumpDown)
        //        {
        //            _rb.useGravity = true;
        //            _animator.SetBool("isHang", false);
        //            isHang = false;
        //            if (Input.GetKey(KeyCode.S))
        //            {
        //                playerVelocity.y = jumpPower;
        //            }
        //            else if (Input.GetKey(KeyCode.W))
        //            {

        //            }
        //        }
        //        transform.forward = -hit.normal;
        //        Vector3 grabPostion = hit.point + hit.normal * 0.18f;
        //        grabPostion.y = hit.collider.transform.position.y + hit.collider.transform.localScale.y / 2 - 0.7f;
        //        Debug.Log(grabPostion);
        //        transform.position = Vector3.Lerp(transform.position,
        //                                grabPostion,
        //                                10f * Time.fixedDeltaTime);
        //    }

        //}

    }

    void TurnFallToHang()
    {
        _animator.SetBool("isHang", true);
        bool ready = false;
        Vector3 offset = transform.TransformDirection(Vector2.one * 0.17f);
        Vector3 checkDirection = Vector3.zero;
        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit checkHit;
            Debug.DrawRay(transform.position + offset, transform.forward, Color.red);
            if (Physics.Raycast(transform.position + offset, transform.forward, out checkHit, 1f, hangable))
            {
                checkDirection += checkHit.normal;
                k++;
            }
            offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
        }

        checkDirection /= k;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -checkDirection, out hit, 1f, hangable))
        {
            transform.forward = -hit.normal;
            Vector3 grabPostion = hit.point + hit.normal * 0.18f;
            grabPostion.y = hit.collider.transform.position.y + hit.collider.transform.localScale.y / 2 - 0.15f;
            Debug.Log((grabPostion-_rb.position).magnitude);
            _rb.position = Vector3.Lerp(_rb.position, grabPostion, 10f);
            transform.forward = Vector3.Lerp(transform.forward, -hit.normal, 10f * Time.fixedDeltaTime);
            if((grabPostion - _rb.position).magnitude <0.1f)
            {
                ready = true;
            }
        }
        if (ready)
        {
            state = PlayerState.HANGING;
        }

    }

    private bool IsCheckGrounded()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,
            Vector3.down,
            out hit, 0.57f))
        {
            return true;
        }
        return false;
    }

    void IsRoll()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit,5f))
        {
            if(hit.distance > 3f)
            {
                isRoll =  true;
                _animator.SetBool("isRoll", true);
            }
        }
    }

    void Roll()
    {
        Debug.Log("do Roll");

        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward.normalized * 0.2f, 0.1f);
        if (_animCallBack.endRollAnim)
        {
            _animator.SetBool("isRoll", false);
            isRoll = false;
            _animCallBack.endRollAnim = false;
            state = PlayerState.RUNNING;
        }
    }

    public void ResetRoll()
    {
        _animator.SetBool("isRoll", false);
        isRoll = false;
        state = PlayerState.RUNNING;
    }

    public bool IsCheckHurdling()
    {
        int layerMask;
        layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(hurdlePosition.position, hurdlePosition.forward, out hit, 5f, layerMask))
        {
            Debug.Log("Go Hurdle");
            if(hit.distance >= 1f && hit.distance < 2.5f)
                return true;
        }
        return false;
    }

    public void Hurdle()
    {
        _animator.SetBool("isHurdle", true);
        if (_animCallBack.startHurdle)
        {
            Debug.Log("do Hurdle");
            transform.position = Vector3.Slerp(transform.position, transform.position + transform.forward.normalized * 5f, 0.1f);
        }
        if (_animCallBack.endHurdle)
        {
            _animCallBack.startHurdle = false;
            _animator.SetBool("isHurdle", false);
            isHurdle = false;
            _animCallBack.endHurdle = false;
            state = PlayerState.RUNNING;

        }
    }

    IEnumerator RotateOnce()
    {

        yield return new WaitForSeconds(1f);
    }
}
