using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallMonster : MonoBehaviour
{
    [SerializeField] float recallTime = 10f;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject recallEffect;
    [SerializeField] GameObject recallingEffect;
    [SerializeField] GameObject recallFields;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        recallingEffect.SetActive(false);
        Invoke("RecallingMonster", recallTime);
        Invoke("OnAnimationStart", recallTime - 1f);
    }

    void RecallingMonster()
    {
        if(recallFields != null)
        {
            for(int i=0;i< recallFields.transform.childCount;i++)
            {
                GameObject field = recallFields.transform.GetChild(i).gameObject;
                if(field.transform.childCount == 0)
                {
                    Debug.Log(field.transform.position);
                    RecallField(field.transform.position);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("필드를 선정하지 않았습니다");
        }
    }

    void RecallField(Vector3 recallPosition)
    {
        animator.SetBool("recallMonster", true);
        Invoke("OnAnimationEnd", 2.33f);
        Instantiate(enemies[Random.Range(0, 2)], recallPosition, Quaternion.identity);
        Instantiate(recallEffect, recallPosition, Quaternion.identity);
        Invoke("RecallingMonster", recallTime);
    }

    public void OnAnimationStart()
    {
        animator.SetBool("recallMonster", true);
        recallingEffect.SetActive(true);
    }

    public void OnAnimationEnd()
    {
        animator.SetBool("recallMonster", false);
        recallingEffect.SetActive(false);
    }

}
