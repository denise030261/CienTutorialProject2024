using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam: MonoBehaviour
{
    public Transform target;
    [SerializeField] public float rotateSpeed = 10f;
    [SerializeField] float mx = 0;
    [SerializeField] float my = 0;
    // Start is called before the first frame update
    void Start()
    {
        mx = target.localRotation.eulerAngles.x;
        my = target.localRotation.eulerAngles.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            float mouse_X = Input.GetAxis("Mouse X");
            float mouse_Y = Input.GetAxis("Mouse Y");

            mx += -mouse_Y * rotateSpeed + Time.deltaTime;
            my += mouse_X * rotateSpeed + Time.deltaTime;

            mx = Mathf.Clamp(mx, -90f, 90f);
            Quaternion q = Quaternion.Euler(mx, my, 0);
            this.gameObject.transform.rotation = q;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            this.gameObject.transform.rotation = target.localRotation;
        }
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.target.position, this.rotateSpeed*Time.deltaTime);
    }
}
