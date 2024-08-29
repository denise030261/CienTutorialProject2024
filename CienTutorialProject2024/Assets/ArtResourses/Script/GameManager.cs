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

    //정보를 가지고 있는 변수할당
    //public GameObject menuCam;
    //public GameObject gameCam;
    //public (플레이어스크립트) player;
    public int stage;
    public float playTime;
    public bool isBattle;
    bool isPause;
    public float[] stageRecord = new float[7];

    //인게임관련변수
    public bool isGameOver;
    public bool isClearStage;
    //인게임 플레이타임
    public float previousTime; //이전 업데이트 시간
    bool wasInBattle; //이전 프레임에서의 전투상태

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

    //인게임
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
        //Time.deltaTime 쓸거면
        //if (isBattle)
        //  playTime += Time.deltaTime; 이렇게만 하면 됨
        if (isBattle)
        {
            if (!isPause)
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

                    //playTime을 업데이트
                    playTime += deltaTime;

                    Debug.Log(currentTime); 
                }
            }
        }
        else
        {
            //스테이지 진입시 시간 초기화
            Time.timeScale = 1;
            playTime = 0;
            previousTime = 0;
            isPause = false;
            wasInBattle = isBattle;

            //게임오버, 클리어 변수 초기화
            isClearStage = false;
            isGameOver = false;
        }
        //현재 전투 상태 저장
        wasInBattle = isBattle;

    }
    void LateUpdate()
    {

    }


 
}
