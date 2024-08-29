using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageMenu : MonoBehaviour
{
    public int stage;
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    //메뉴화면 버튼관련
    public List<GameObject> StageClick = new List<GameObject>();

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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
