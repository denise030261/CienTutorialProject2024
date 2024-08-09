using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Animator _animator;
    public Camera _camera;
    public Camera _aimCamera;
    CharacterController _controller;

    public float speed = 8f;
    public float gravity = -20f;
    public float jumpPower = 0.1f;
    float yVelocity = 0;
    public bool toggleCameraRotation;

    Vector3 dir;
    Vector3 characterRotation;

    public float smoothness = 10f;
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


    }

    private void LateUpdate()
    {
        if (toggleCameraRotation)
        {
            Vector3 playerRotate = Vector3.Scale(_aimCamera.transform.forward, new Vector3(1, 0, 1));//카메라 방향으로 캐릭터 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
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
        if (_controller.isGrounded)
        {
            Debug.Log("is Grounded!");
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            dir = new Vector3(h, 0, v);
            dir = dir.normalized;
            dir = _camera.transform.TransformDirection(dir);

            characterRotation = Vector3.Scale(dir, new Vector3(1, 0, 1));

            if (dir.magnitude != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(characterRotation), Time.deltaTime * smoothness);

            }

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
            }
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        _controller.Move(dir * speed * Time.deltaTime);
        float percent = 0.5f * characterRotation.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

    }
}
