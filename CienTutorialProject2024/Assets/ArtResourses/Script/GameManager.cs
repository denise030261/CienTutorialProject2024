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
    public float[] stageRecord = new float[6];

    //UI�� ���� ���� �Ҵ�
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public Text playTimeTxt;
    public Text clearTimeTxt;
    public Image gun1Img;
    public Image gun2Img;
    //��ư ����
    public Image startselect;
    //����ȭ�� ��ư����
    public List<GameObject> StartmenuClick = new List<GameObject>();
    //�޴�ȭ�� ��ư����
    public List<GameObject> StageClick = new List<GameObject>();

    //�ΰ��Ӱ��ú���

    //�ΰ��� �÷���Ÿ��
    public float previousTime; //���� ������Ʈ �ð�
    bool wasInBattle; //���� �����ӿ����� ��������


    //���ӽ���ȭ��
    public void GameStart()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void StartbuttonOn (int num)
    {
        StartmenuClick[num].SetActive(true);
    }
    public void StartbuttonOff (int num)
    {
        StartmenuClick[num].SetActive(false);
    }

    //�޴�ȭ��
    public void Stageselect(int num)
    {

        StageClick[num].SetActive(true);
    }
    public void StageDeselect(int num)
    {
        StageClick[num].SetActive(false);
    }
    
    public void MenuBack()
    {
        menuPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void Clear()
    {
        Time.timeScale = 0;
        clearTimeTxt = playTimeTxt;
        stageRecord[stage] = playTime;
        playTime = 0;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameoverPanel.SetActive(true);
        playTime = 0;
    }

    public void PauseGame()
    {
        if (!isPause)
        {
            isPause = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPause = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }


    }

    //�ΰ���
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        else if(_instance != this)
        {
            Destroy(_instance);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isPause = false;
        previousTime = Time.time;
        wasInBattle = isBattle;
    }
    void Update()
    {
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


                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        //���� ���� ���� ����
        wasInBattle = isBattle;

    }
    void LateUpdate()
    {
        //�÷���Ÿ��
        int min = (int)(playTime/ 60);
        int second = (int)(playTime % 60);
        int milisecond = (int)((playTime - second) * 100);
        playTimeTxt.text = string.Format("{0:00}",min) + ":" + string.Format("{0:00}", second) + ":" + string.Format("{0:00}", milisecond);
    }


 
}
