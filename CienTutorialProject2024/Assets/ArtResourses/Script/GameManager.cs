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
    public List<GameObject> StartmenuClick = new List<GameObject>();
    //메뉴화면 버튼관련
    public List<GameObject> StageClick = new List<GameObject>();
    //인게임관련변수

    //인게임 플레이타임
    public float previousTime; //이전 업데이트 시간
    bool wasInBattle; //이전 프레임에서의 전투상태


    //게임시작화면
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

    //메뉴화면
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

    //인게임
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
        //Time.deltaTime 쓸거면
        //if (isBattle)
        //  playTime += Time.deltaTime; 이렇게만 하면 됨

        if (isBattle)
        {
            if (!wasInBattle)
            {
                //전투 상태가 이전에는 비활성화되어 있었고 현재는 활성화 된 경우
                //이전 시간 업데이트
                previousTime = Time.time;
            }
            else
            {
                //현재 시간과 이전 시간의 차이를 계산
                float currentTime = Time.time;
                float deltaTime = currentTime - previousTime;
                previousTime = currentTime;

                //playTIme을 업데이트
                playTime += deltaTime;
            }
        }
        //현재 전투 상태 저장
        wasInBattle = isBattle;
    }
    void PlayTimeShow()
    {
        //플레이타임
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);
        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
    }

    //게임종료
    public void ExitGame()
    {
        Application.Quit();
    }

}
