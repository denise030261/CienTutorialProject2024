using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //정보를 가지고 있는 변수할당
    //public GameObject menuCam;
    //public GameObject gameCam;
    //public (플레이어스크립트) player;
    public int stage;
    public float playTime;
    public bool isBattle;

    //UI를 위한 변수 할당
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public Text playTimeTxt;
    public Image gun1Img;
    public Image gun2Img;
    //버튼 관련
    public Image startselect;
    //시작화면 버튼관련
    public GameObject startSelect;
    public GameObject continueSelect;
    public GameObject optionsSelect;
    public GameObject exitSelect;
    public List<GameObject> B = new List<GameObject>();
    //메뉴화면 버튼관련
    public GameObject stage1click;
    public GameObject stage2click;
    public GameObject stage3click;
    public GameObject stage4click;
    public GameObject stagebclick;
    public List<GameObject> A = new List<GameObject>();

    //게임시작화면
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

    //메뉴화면
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
