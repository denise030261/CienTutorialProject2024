using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StageMenu : MonoBehaviour
{
    public int stage;
    public Canvas startUI;
    //메뉴화면 버튼관련
    public List<Text> recordText;
    float[] stageRecord;
    //메뉴화면
    public void StageSelect(int num)
    {
        SceneManager.LoadScene("stage"+num.ToString());
        AudioManager.Instance.PlayBGM("stage" + num.ToString());
        GameManager.Instance.stage = num;
    }

    public void MenuBack()
    {
        startUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        stageRecord = GameManager.Instance.stageRecord;
        for(int i =0; i<stageRecord.Length;i++)
        {
            if (stageRecord[i] != 0)
            {
                int min = (int)(stageRecord[i] / 60);
                int second = (int)(stageRecord[i] % 60);
                int milisecond = (int)((stageRecord[i] - second) * 100);
                recordText[i].text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second) + ":" + string.Format("{0:00}", milisecond);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
