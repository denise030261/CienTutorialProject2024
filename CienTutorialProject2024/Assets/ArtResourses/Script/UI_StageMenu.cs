using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StageMenu : MonoBehaviour
{
    public int stage;
    public Canvas startUI;
    //�޴�ȭ�� ��ư����
    public List<GameObject> StageClick = new List<GameObject>();

    //�޴�ȭ��
    public void StageSelect(int num)
    {
        SceneManager.LoadScene("stage"+num.ToString());
        GameManager.Instance.stage = num;
    }
    public void StageDeselect(int num)
    {
        StageClick[num].SetActive(false);
    }

    public void MenuBack()
    {
        startUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
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
