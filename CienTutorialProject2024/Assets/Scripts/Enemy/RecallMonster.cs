using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallMonster : MonoBehaviour
{
    [SerializeField] float recallTime = 10f;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject recallEffect;
    [SerializeField] GameObject recallingEffect;
    [SerializeField] GameObject recallField;
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
        if(recallField!=null)
        {
            Vector3 recallPosition = new Vector3(recallField.transform.position.x + Random.Range(-recallField.transform.localScale.x / 2, recallField.transform.localScale.x / 2),
                recallField.transform.position.y + recallField.transform.localScale.y,
                recallField.transform.position.z + Random.Range(-recallField.transform.localScale.z / 2, recallField.transform.localScale.z / 2));
            animator.SetBool("recallMonster", true);
            Invoke("OnAnimationEnd", 2.33f);
            Instantiate(enemies[Random.Range(0, 2)], recallPosition, Quaternion.identity);
            Instantiate(recallEffect, recallPosition, Quaternion.identity);
            Invoke("RecallingMonster", recallTime);
        }
        else
        {
            Debug.Log("필드를 선정하지 않았습니다");
        }
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
