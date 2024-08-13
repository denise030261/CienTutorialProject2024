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

   Transform handPosition;
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
        handPosition = this.transform.Find("HandPoint");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            toggleCameraRotation = true; //에임시
            _camera.enabled = false;
            _aimCamera.enabled = true;
        }
        else
        {
            toggleCameraRotation = false; //평상시
            _camera.enabled = true;
            _aimCamera.enabled = false;
        }

        InputMovement();
        Hang();
        Roll();
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

        if(!_animator.GetBool("isHang"))
        {
            move = new Vector3(h, 0, v);
            speed = 5f;
            move = _camera.transform.TransformDirection(move);
            move = Vector3.Scale(move, new Vector3(1, 0, 1));
        }
        else
        {
            move = new Vector3(h, 0, 0);
            speed = 0.5f;
        }



        _controller.Move(move * Time.deltaTime * speed);

        characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            _animator.SetBool("isJump", true);
        }


        playerVelocity.y += gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);


        float percent = 0.5f * move.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        _animator.SetFloat("Hang Blend", h);
    }

    void PlayerRotation()
    {
        if(!_animator.GetBool("isHang"))
        {
            if (toggleCameraRotation)
            {
                Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));//카메라 방향으로 캐릭터 회전
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
            }
            else
            {
                if (move.magnitude > 0.05f)
                {
                    characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(characterRotation), Time.deltaTime * smoothness);
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
            

            float distance = Vector3.Distance(hangPosition.position, handPosition.position);

            if (!_animator.GetBool("isHang")) 
            { 
                if (Input.GetButtonDown("Jump") && distance < 0.2f)
                {
                    Vector3 delta = handPosition.position - hangPosition.position;
                    this.transform.position = Vector3.Slerp(this.transform.position, transform.position + delta, Time.deltaTime * smoothness);
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
