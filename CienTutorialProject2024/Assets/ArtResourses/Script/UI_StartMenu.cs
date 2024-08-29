using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartMenu : MonoBehaviour
{
    public Canvas stageUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }



    public void NewGame()
    {
        stageUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
        GameManager.Instance.stageRecord = new float[6];
    }

    public void Continue()
    {
        stageUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
