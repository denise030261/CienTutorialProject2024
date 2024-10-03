using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    //변수할당
    public GameObject pausePanel;
    bool isPauseActive;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public PlayerAkane player;
    public Image gun1Img;
    public Image gun2Img;
    public GameObject gun1;
    public GameObject gun2;

    public float playTime;
    public Text playTimeTxt;
    public Text clearTimeTxt;
    string timeTxt;

    bool isGameOverActive;
    bool isClearStageActive;
    // Start is called before the first frame update
    void Start()
    {
        isPauseActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        playTime = GameManager.Instance.playTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetPauseMenu();
        }
        isGameOverActive = GameManager.Instance.isGameOver;
        isClearStageActive = GameManager.Instance.isClearStage;
    }

    void LateUpdate()
    {
        HasWeapon();
        //플레이타임
        int min = (int)(playTime / 60);
        int second = (int)(playTime % 60);
        int milisecond = (int)((playTime - (min*60)- second) * 100);
        timeTxt = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second) + ":" + string.Format("{0:00}", milisecond);
        playTimeTxt.text = timeTxt;
        if (isGameOverActive)
        {
            GetGameOverMenu();
        }
        if (isClearStageActive)
        {
            GetClearMenu();
        }
    }

    public void GetPauseMenu()
    {
        GameManager.Instance.PauseGame();
        if (!isPauseActive)
        {
            pausePanel.SetActive(true);
            isPauseActive = true;
        }
        else
        {
            pausePanel.SetActive(false);
            isPauseActive = false;
        }
    }

    public void GetGameOverMenu()
    {
        gameoverPanel.SetActive(true);
    }

    public void GetClearMenu()
    {
        gameclearPanel.SetActive(true);
        Debug.Log(timeTxt); 
        clearTimeTxt.text = timeTxt;
    }
    public void GotoMenu()
    {
        Debug.Log("Go To Menu");
        GameManager.Instance.stage = 0;
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlayBGM("Title");
    }

    public void ResetScene()
    {
        int curStage = GameManager.Instance.stage;
        GameManager.Instance.stage = 0;
        SceneManager.LoadScene("stage" + curStage.ToString());
        AudioManager.Instance.PlayBGM("stage" + curStage.ToString());
    }

    //무기획득 시 아이콘관련
    void HasWeapon()
    {
        //gun1Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        //gun2Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        if (player.hasWeapons[1] == true)
        {
            gun1.SetActive(true);
        }
        if (player.hasWeapons[2] == true)
        {
            gun2.SetActive(true);
        }
    }
}
