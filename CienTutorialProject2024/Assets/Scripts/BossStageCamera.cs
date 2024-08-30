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
    [SerializeField] Animator bossAnimator;

    Vector3 startPosition = new Vector3(8.5f, 5f, 0f);
    Quaternion startRotation= Quaternion.Euler(20f, -90f, 0f);
    Vector3 targetPosition = new Vector3(1.5f, 5.5f, 17f);
    Quaternion targetRotation = Quaternion.Euler(-10f, -30f, 0f);
    float elapsedTime = 0f;
    bool isMove = false;
    int prePage = 1;
    int curPage = 1;

    int Scene = 1;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        LCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        FirstScene();
        curPage = BossStageController.instance.page;

        if(prePage!=curPage)
        {
            if(!isMove && !MoveCamera.enabled)
            {
                StartCoroutine(PlayerMoveCamera());
            }
            else if(isMove && MoveCamera.enabled)
            {
                elapsedTime += Time.deltaTime;

                // 위치와 회전 갱신
                MoveCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (cameraMoveSpeed*2f));
                MoveCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / (cameraMoveSpeed * 2f));

                // 목표에 도달했는지 확인
                if (elapsedTime >= (cameraMoveSpeed * 2f))
                {
                    MoveCamera.transform.position = targetPosition; // 정확한 목표 위치로 이동
                    MoveCamera.transform.rotation = targetRotation; // 정확한 목표 회전으로 회전

                    StartCoroutine(BossMoveCamera());
                }
            }
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

    IEnumerator BossMoveCamera()
    {
        if(BossStageController.instance.page<=3)
        {
            bossAnimator.SetBool("getDamage", true);
            bossAnimator.SetBool("recallMonster", false);
        }
        else
        {
            bossAnimator.SetBool("getDamage", false);
            bossAnimator.SetBool("recallMonster", false);
            bossAnimator.SetBool("isDown", true);
        }
        elapsedTime = 0;
        prePage = curPage;
        isMove = false;
        yield return new WaitForSeconds(3f);
        bossAnimator.SetBool("getDamage", false);
        MoveCamera.enabled = false;
        AllSceneCamera.enabled = true;
        yield return new WaitForSeconds(3f);
        ChangeMainCamera();
    }

    IEnumerator PlayerMoveCamera()
    {
        Init();
        MoveCamera.enabled = true;
        MoveCamera.transform.position = startPosition;
        MoveCamera.transform.rotation = startRotation;
        yield return new WaitForSeconds(1f);
        isMove = true;
    }

    void ChangeMainCamera()
    {
        NellCamera.enabled = false;
        LCamera.enabled = false;
        AllSceneCamera.enabled = false;
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

            Scene++;
            Invoke("ChangeMainCamera", 5f);
        }
    }
}
