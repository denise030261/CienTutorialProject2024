using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    //변수할당
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    public PlayerAkane player;
    //public Image gun1Img;
    //public Image gun2Img;
    public GameObject gun1;
    public GameObject gun2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        HasWeapon();
    }

    //무기획득 시 아이콘관련
    void HasWeapon()
    {
        //gun1Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        //gun2Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        if (player.hasWeapons[1] = true)
        {
            gun1.SetActive(true);
        }
        if (player.hasWeapons[2] = true)
        {
            gun2.SetActive(true);
        }
    }
}
