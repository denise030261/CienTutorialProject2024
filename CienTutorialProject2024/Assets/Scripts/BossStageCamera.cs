using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossStageCamera : MonoBehaviour
{
    [SerializeField] Camera NellCamera;
    [SerializeField] Camera LCamera;
    [SerializeField] Camera AllSceneCamera;
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera MoveCamera;
    [SerializeField] Camera AimCamera;

    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] float cameraMoveSpeed = 1f;

    bool isMove = false;
    int prePage = 1;
    int curPage = 1;

    int Scene = 1;
    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        LCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        FirstScene();
        curPage = BossStageController.instance.page;

        if(prePage!=curPage)
        {
            /*if(!isMove)
            {
                Init();
                MoveCamera.enabled = true;
                MoveCamera.transform.position = new Vector3(-12f, 7.5f, 4.5f);
                MoveCamera.transform.rotation = Quaternion.Euler(-10f, -30f, 0f);
                isMove = true;
            }
            else
            {
                elapsedTime += Time.deltaTime;

                // 위치와 회전 갱신
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(targetRotation), elapsedTime / duration);

                // 목표에 도달했는지 확인
                if (elapsedTime >= duration)
                {
                    transform.position = targetPosition; // 정확한 목표 위치로 이동
                    transform.rotation = Quaternion.Euler(targetRotation); // 정확한 목표 회전으로 회전

                    OnReachedTarget(); // 목표에 도달했을 때 실행할 메서드 호출
                }
            }
            //도착하면
            prePage = curPage;*/
        }
    }

    private void Init()
    {
        NellCamera.enabled = false;
        LCamera.enabled = false;
        AllSceneCamera.enabled = false;
        MainCamera.enabled = false;
        MoveCamera.enabled = false;
        AimCamera.enabled = false;
        cameraFollow.enabled = false;
        playerMovement.enabled = false;
    }

    void ChangeMainCamera()
    {
        Scene++;
        NellCamera.enabled = false;
        LCamera.enabled = false;
        MainCamera.enabled = true;
        cameraFollow.enabled = true;
        playerMovement.enabled = true;
    }

    void FirstScene()
    {
        if (Scene == 1)
        {
            if (LCamera.transform.position.z >= -19)
            {
                Scene++;
                LCamera.enabled = false;
                NellCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = LCamera.transform.position;
                currentPosition.z += Time.deltaTime * cameraMoveSpeed;
                LCamera.transform.position = currentPosition;
            }
        }
        else if (Scene == 2)
        {
            if (NellCamera.transform.position.z <= 19)
            {
                Scene++;
                NellCamera.enabled = false;
                AllSceneCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = NellCamera.transform.position;
                currentPosition.z -= Time.deltaTime * cameraMoveSpeed;
                NellCamera.transform.position = currentPosition;
            }
        }
        else if (Scene == 3)
        {
            if (AllSceneCamera.transform.position.y >= 27)
            {
                Scene++;
                AllSceneCamera.enabled = false;
                NellCamera.enabled = true;
                LCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = AllSceneCamera.transform.position;
                currentPosition.y += Time.deltaTime * cameraMoveSpeed * 5;
                AllSceneCamera.transform.position = currentPosition;
            }
        }
        else if (Scene == 4)
        {
            Vector3 currentPosition = NellCamera.transform.position;
            currentPosition.z = 20;
            NellCamera.transform.position = currentPosition;

            currentPosition = LCamera.transform.position;
            currentPosition.z = -20;
            LCamera.transform.position = currentPosition;

            LCamera.rect = new Rect(0, 0, 0.5f, 1);
            NellCamera.rect = new Rect(0.5f, 0, 0.5f, 1);

            Invoke("ChangeMainCamera", 5f);
        }
    }
}
