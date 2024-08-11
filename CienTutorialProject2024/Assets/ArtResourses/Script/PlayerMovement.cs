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
    bool groundedPlayer;
    public bool isGrounded;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
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
    }

    private void LateUpdate()
    {
        //if (toggleCameraRotation)
        //{
        //    Vector3 playerRotate = Vector3.Scale(_aimCamera.transform.forward, new Vector3(1, 0, 1));//카메라 방향으로 캐릭터 회전
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        //}
        PlayerRotation();
    }

    void InputMovement()
    {
        //Vector3 cameraForward = transform.TransformDirection(_camera.transform.forward);
        //Vector3 cameraRight = transform.TransformDirection(_camera.transform.right);

        //Vector3 moveDirection = cameraForward * Input.GetAxisRaw("Vertical") + cameraRight * Input.GetAxisRaw("Horizontal");
        //yVelocity += gravity * Time.deltaTime;
        //moveDirection.y = yVelocity;
        //if(moveDirection.magnitude != 0)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * smoothness);
        //}
        //_controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        //float percent = 0.5f * moveDirection.magnitude;
        //_animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);


        //if (_controller.isGrounded)
        //{
        //    Debug.Log("is Grounded!");
        //    float h = Input.GetAxis("Horizontal");
        //    float v = Input.GetAxis("Vertical");

        //    dir = new Vector3(h, 0, v);
        //    dir = dir.normalized;
        //    dir = _camera.transform.TransformDirection(dir);

        //    characterRotation = Vector3.Scale(dir, new Vector3(1, 0, 1));

        //    if (dir.magnitude != 0)
        //    {
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(characterRotation), Time.deltaTime * smoothness);

        //    }

        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        yVelocity = Mathf.Sqrt(jumpPower * -3.0f * gravity);
        //    }
        //}

        //yVelocity += gravity * Time.deltaTime;
        //dir.y = yVelocity;

        //_controller.Move(dir * speed * Time.deltaTime);
        //float percent = 0.5f * characterRotation.magnitude;
        //_animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = _camera.transform.TransformDirection(move);
        _controller.Move(move * Time.deltaTime * speed);

        characterRotation = Vector3.Scale(move, new Vector3(1, 0, 1));


        if(Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);

        }

        playerVelocity.y += gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);

        float percent = 0.5f * move.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
    }

    void PlayerRotation()
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

    void Hang()
    {
        int layerMask;
        layerMask = 1 << 8;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 30f, layerMask))
        {
            Transform hangPosition = hit.collider.transform.Find("ParkourPoint");
            Transform handPosition = gameObject.transform.Find("HandPoint");

            float distance = Vector3.Distance(hangPosition.position, handPosition.position);
            if (Input.GetButtonDown("Jump") && distance < 0.2f)
            {
                _animator.SetBool("isHang", true);
            }
        }
    }
}
