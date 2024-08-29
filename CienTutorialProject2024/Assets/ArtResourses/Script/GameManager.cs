using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float playTime;
    public bool isBattle;

    //UI�� ���� ���� �Ҵ�
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public PlayerAkane player;
    public Text playTimeTxt;
    
    //����ȭ�� ��ư����
    public List<GameObject> StartmenuClick = new List<GameObject>();
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

    

    //�ΰ���
    void Start()
    {
        previousTime = Time.time;
        wasInBattle = isBattle;
    }
    void Update()
    {
        PlayTimeCalculate();

    }
    void LateUpdate()
    {
        PlayTimeShow();
    }

    
    void PlayTimeCalculate()
    {
        //Time.deltaTime ���Ÿ�
        //if (isBattle)
        //  playTime += Time.deltaTime; �̷��Ը� �ϸ� ��

        if (isBattle)
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

                //playTIme�� ������Ʈ
                playTime += deltaTime;
            }
        }
        //���� ���� ���� ����
        wasInBattle = isBattle;
    }
    void PlayTimeShow()
    {
        //�÷���Ÿ��
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);
        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
    }

    //��������
    public void ExitGame()
    {
        Application.Quit();
    }

}
