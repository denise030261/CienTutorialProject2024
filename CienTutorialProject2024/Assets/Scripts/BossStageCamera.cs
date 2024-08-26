using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageCamera : MonoBehaviour
{
    [SerializeField] Camera NellCamera;
    [SerializeField] Camera LCamera;
    [SerializeField] Camera AllSceneCamera;
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera MoveCamera;
    [SerializeField] float cameraMoveSpeed = 1f;

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
       if(Scene ==1)
        {
            if (LCamera.transform.position.z >= -13)
            {
                Scene++;
                LCamera.enabled = false;
                NellCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = LCamera.transform.position;
                currentPosition.z += Time.deltaTime* cameraMoveSpeed;
                LCamera.transform.position = currentPosition;
            }
        }
       else if(Scene==2)
        {
            if (NellCamera.transform.position.z <= 13)
            {
                Scene++;
                NellCamera.enabled = false;
                AllSceneCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = NellCamera.transform.position;
                currentPosition.z -= Time.deltaTime* cameraMoveSpeed;
                NellCamera.transform.position = currentPosition;
            }
        }
       else if(Scene ==3)
        {
            if (AllSceneCamera.transform.position.y >= 20)
            {
                Scene++;
                AllSceneCamera.enabled = false;
                NellCamera.enabled = true;
                LCamera.enabled = true;
            }
            else
            {
                Vector3 currentPosition = AllSceneCamera.transform.position;
                currentPosition.y += Time.deltaTime * cameraMoveSpeed*2;
                AllSceneCamera.transform.position = currentPosition;
            }
        }
       else if(Scene==4)
        {
            Vector3 currentPosition = NellCamera.transform.position;
            currentPosition.z = 14;
            NellCamera.transform.position = currentPosition;

             currentPosition = LCamera.transform.position;
            currentPosition.z = -14;
            LCamera.transform.position = currentPosition;

            LCamera.rect = new Rect(0, 0, 0.5f, 1);
            NellCamera.rect = new Rect(0.5f, 0, 0.5f, 1);

            Invoke("ChangeMainCamera", 5f);
        }
    }

    private void Init()
    {
        NellCamera.enabled = false;
        LCamera.enabled = false;
        AllSceneCamera.enabled = false;
        MainCamera.enabled = false;
        MoveCamera.enabled = false;
    }

    void ChangeMainCamera()
    {
        Scene++;
        NellCamera.enabled = false;
        LCamera.enabled = false;
        MainCamera.enabled = true;
    }
}
