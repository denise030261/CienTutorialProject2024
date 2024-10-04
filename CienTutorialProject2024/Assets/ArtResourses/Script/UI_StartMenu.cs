using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartMenu : MonoBehaviour
{
    public Canvas stageUI;
    public GameObject audioUI;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Title");
    }

    public void NewGame()
    {
        stageUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
        GameManager.Instance.stageRecord = new float[7];
    }

    public void Continue()
    {
        stageUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GetAudioSetting(bool on)
    {
        audioUI.SetActive(on);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
