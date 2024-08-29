using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageController : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject stair;
    [SerializeField] GameObject longAttackEnemies;
    [SerializeField] GameObject solveDefence;
    public int page = 1;
    public static BossStageController instance = null;

    void Awake()
    {
        instance = this;
        bridge.SetActive(false);
        stair.SetActive(false);
        longAttackEnemies.SetActive(false);
        solveDefence.SetActive(true);
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
        else if(page==3) 
        {
            solveDefence.SetActive(false);
        }
        else if(page == 4) 
        {
            stair.SetActive(true);
        }
    }

    //보스 애니메이션 컨트롤하기 
}
