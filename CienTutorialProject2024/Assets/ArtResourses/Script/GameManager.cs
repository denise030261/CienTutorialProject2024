using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //������ ������ �ִ� �����Ҵ�
    //public GameObject menuCam;
    //public GameObject gameCam;
    //public (�÷��̾ũ��Ʈ) player;
    public int stage;
    public float playTime;
    public bool isBattle;

    //UI�� ���� ���� �Ҵ�
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public Text playTimeTxt;
    public Image gun1Img;
    public Image gun2Img;
    //��ư ����
    public Image startselect;
    //����ȭ�� ��ư����
    public GameObject startSelect;
    public GameObject continueSelect;
    public GameObject optionsSelect;
    public GameObject exitSelect;
    public List<GameObject> B = new List<GameObject>();
    //�޴�ȭ�� ��ư����
    public GameObject stage1click;
    public GameObject stage2click;
    public GameObject stage3click;
    public GameObject stage4click;
    public GameObject stagebclick;
    public List<GameObject> A = new List<GameObject>();

    //���ӽ���ȭ��
    public void GameStart()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(true);

    }

    public void StartbuttonOn (int num)
    {
        B[num].SetActive(true);
    }
    public void StartbuttonOff (int num)
    {
        B[num].SetActive(false);
    }

    //�޴�ȭ��
    public void stageselect(int num)
    {
        
        A[num].SetActive(true);
    }
    public void stageDeselect(int num)
    {
        A[num].SetActive(false);
    }
    
    public void menuBack()
    {
        menuPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
