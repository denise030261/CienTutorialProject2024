using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageController : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject longAttackEnemies;
    public int page = 1;
    public static BossStageController instance;   

    void Awake()
    {
        BossStageController.instance = this;
        bridge.SetActive(false);
        longAttackEnemies.SetActive(false);
    } 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(page==2)
        {
            bridge.SetActive(true);
            longAttackEnemies.SetActive(true);
        }
    }

 
}
