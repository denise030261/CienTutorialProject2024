using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update



    Animator _animator;
    public Camera _camera;
    public Camera _aimCamera;
    CharacterController _controller;
    bool isZoom;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpPower = 1.0f;
    float yVelocity = 0;
    public bool toggleCameraRotation;

    Vector3 dir;
    Vector3 characterRotation;
    Vector3 move = Vector3.zero;

    public float smoothness = 10f;

    Vector3 playerVelocity;
    bool isGrounded;
   
    bool isHang;
    bool isHangPosition;
    Vector3 hangDist;

   Transform handPosition;
    void Start()
    {

        _animator = gameObject.GetComponent<Animator>();
        _controller = gameObject.GetComponent<CharacterController>();
        handPosition = gameObject.transform.Find("HandPoint");
        isHang = false;
        isHangPosition = false;
        hangDist = Vector3.zero;
        isZoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            toggleCameraRotation = true; //에임시
            _camera.enabled = false;
            _aimCamera.enabled = true;
            isZoom = true;
            _animator.SetBool("isZoom", true);
        }
        else
        {
            toggleCameraRotation = false; //평상시
            _camera.enabled = true;
            _aimCamera.enabled = false;
            isZoom = false;
            _animator.SetBool("isZoom", false);
        }

        InputMovement();
        Hang();
        Roll();
        //GoHangPosition();
    }

    private void LateUpdate()
    {
        PlayerRotation();
    }

    void InputMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        isGrounded = IsCheckGrounded();

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            _animator.SetBool("isJump", false);
        }

        if (!isGrounded)
        {
            _animator.SetBool("isJump", true);
        }

        if(!isHang)
        {
            move = new Vector3(h, 0, v);
            speed = 5f;
            move = _camera.transform.TransformDirection(move);
            move = Vector3.Scale(move, new Vector3(1, 0, 1));
        }
        else
        {
            move = new Vector3(h, 0, 0);
            speed = 0.7f;
        }



        _controller.Move(move * 0.01f * speed);

        characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            _animator.SetBool("isJump", true);
        }


        playerVelocity.y += gravity * 0.01f;
        _controller.Move(playerVelocity * 0.01f);


        float percent = 0.5f * move.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, 0.01f);

        _animator.SetFloat("Hang Blend", h);
    }

    void PlayerRotation()
    {
        if(!_animator.GetBool("isHang"))
        {
            if (toggleCameraRotation)//줌 했을 시 즉시
            {
                Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));//카메라 방향으로 캐릭터 회전
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), 0.01f * smoothness);
            }
            else
            {
                if (move.magnitude > 0.05f)//줌 하지 않고 움직일 시
                {
                    characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(characterRotation), 0.01f * smoothness);
                }
            }
        }

    }

    void Hang()
    {
        int layerMask;
        layerMask = 1 << 8;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 30f, layerMask))
        {
            Transform hangPosition = hit.collider.transform.Find("ParkourPoint");
            

            float distance = Vector3.Distance(hangPosition.position, handPosition.position);//벽에서 잡는 곳, 손과의 거리

            if (!isHang) 
            { 
                if (Input.GetButtonDown("Jump") && distance < 0.2f)
                {
                    hangDist = handPosition.position - hangPosition.position;
                    Debug.Log(hangDist);
                    //gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, transform.position + delta, 0.01f * smoothness);
                    isHang = true;
                    playerVelocity = Vector3.zero;
                    gravity = 0;
                    _animator.SetBool("isHang", true);

                }
            }
            else
            {
                if (Input.GetButtonDown("Jump"))
                {
                    gravity = -9.81f;
                    _animator.SetBool("isHang", false);
                    isHang = false;
                    if (Input.GetKey(KeyCode.S))
                    {
                        playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {

                    }
                }
                
            }

        }
        else
        {
            _animator.SetBool("isHang", false);
            gravity = -9.81f;
        }
    }

    private bool IsCheckGrounded()
    {
        if (_controller.isGrounded) return true;
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);

        var maxDistance = 0.1f;

        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);

        return Physics.Raycast(ray, maxDistance);
    }

    void Roll()
    {
        RaycastHit floor;

        if(Physics.Raycast(transform.position, Vector3.down, out floor, 50f))
        {
            if(floor.distance > 2f)
            {
                _animator.SetBool("isRoll", true);
            }
        }


    }

    public void ResetRoll()
    {
        _animator.SetBool("isRoll", false);
    }


}
