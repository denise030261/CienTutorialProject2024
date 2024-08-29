using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float followSpeed = 0.1f;
    public float sensitivity = 0.1f;
    public float clampAngle = 70f;

    float mx = 0;
    float my = 0;

    public Transform mainCamera;
    public Transform aimCamera;
    public Transform activatedCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public Vector3 localPosition;
    public float minDistance;
    float maxDistance;
    public float finalDistance;
    public float smoothness = 0.1f;

    private void Start()
    {
        mx = transform.localRotation.eulerAngles.x;
        my = transform.localRotation.eulerAngles.y;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            dirNormalized = aimCamera.localPosition.normalized;
            finalDistance = aimCamera.localPosition.magnitude;
            activatedCamera = aimCamera;
            maxDistance = 1f;
        }
        else
        {
            dirNormalized = mainCamera.localPosition.normalized;
            finalDistance = mainCamera.localPosition.magnitude;
            activatedCamera = mainCamera;
            maxDistance = 2f;
        }

        mx += -(Input.GetAxis("Mouse Y")) * sensitivity * 0.05f;
        my += Input.GetAxis("Mouse X") * sensitivity * 0.05f;

        mx = Mathf.Clamp(mx, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(mx, my, 0);
        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, followSpeed * 0.05f);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }

        // if문 또는 아예 참조
        activatedCamera.localPosition = Vector3.Lerp(activatedCamera.localPosition, dirNormalized * finalDistance, 0.05f * smoothness);
    }
}
