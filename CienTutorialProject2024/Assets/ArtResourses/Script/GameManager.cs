using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if( _instance == null)
                {
                    Debug.Log("No Singleton obj");
                }
            }
            return _instance;
        }
    }

    //������ ������ �ִ� �����Ҵ�
    //public GameObject menuCam;
    //public GameObject gameCam;
    //public (�÷��̾ũ��Ʈ) player;
    public int stage;
    public float playTime;
    public bool isBattle;
    bool isPause;
    public float[] stageRecord = new float[7];

    //�ΰ��Ӱ��ú���
    public bool isGameOver;
    public bool isClearStage;
    //�ΰ��� �÷���Ÿ��
    public float previousTime; //���� ������Ʈ �ð�
    bool wasInBattle; //���� �����ӿ����� ��������

    public void Clear()
    {
        isClearStage = true;
        Time.timeScale = 0;
        stageRecord[stage] = playTime;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        playTime = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void PauseGame()
    {
        if (!isPause)
        {
            Debug.Log("PauseGame");
            isPause = true;
            Time.timeScale = 0; 
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
        }
    }

    //�ΰ���
    private void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        isClearStage = false;
        isGameOver = false;
        isPause = false;
        previousTime = Time.time;
        wasInBattle = isBattle;
    }
    void Update()
    {
        isBattle = stage != 0;
        //Time.deltaTime ���Ÿ�
        //if (isBattle)
        //  playTime += Time.deltaTime; �̷��Ը� �ϸ� ��
        if (isBattle)
        {
            if (!isPause)
            {
                if (!wasInBattle)
                {
                    //���� ���°� �������� ��Ȱ��ȭ�Ǿ� �־��� ����� Ȱ��ȭ �� ���
                    //���� �ð� ������Ʈ
                    previousTime = Time.time;
                }
                else
                {

                    //���� �ð��� ���� �ð��� ���̸� ���
                    float currentTime = Time.time;
                    float deltaTime = currentTime - previousTime;
                    previousTime = currentTime;

                    //playTime�� ������Ʈ
                    playTime += deltaTime;

                    Debug.Log(currentTime); 
                }
            }
        }
        else
        {
            //�������� ���Խ� �ð� �ʱ�ȭ
            Time.timeScale = 1;
            playTime = 0;
            previousTime = 0;
            isPause = false;
            wasInBattle = isBattle;

            //���ӿ���, Ŭ���� ���� �ʱ�ȭ
            isClearStage = false;
            isGameOver = false;
        }
        //���� ���� ���� ����
        wasInBattle = isBattle;

    }
    void LateUpdate()
    {

    }


 
}
