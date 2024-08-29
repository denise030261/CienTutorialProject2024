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

                // ��ġ�� ȸ�� ����
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(targetRotation), elapsedTime / duration);

                // ��ǥ�� �����ߴ��� Ȯ��
                if (elapsedTime >= duration)
                {
                    transform.position = targetPosition; // ��Ȯ�� ��ǥ ��ġ�� �̵�
                    transform.rotation = Quaternion.Euler(targetRotation); // ��Ȯ�� ��ǥ ȸ������ ȸ��

                    OnReachedTarget(); // ��ǥ�� �������� �� ������ �޼��� ȣ��
                }
            }
            //�����ϸ�
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
