using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallMonster : MonoBehaviour
{
    [SerializeField] float recallTime = 10f;
    [SerializeField] GameObject Enemy;
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
            bool isCreate = false;
            foreach(Transform fields in recallFields.transform)
            {
                if(!isCreate)
                {
                    bool isNeed = true;
                    foreach (Transform field in fields)
                    {
                        if (field.childCount >= 1)
                        {
                            isNeed = false;
                            break;
                        }
                    }

                    if(isNeed)
                    {
                        isCreate = true;
                        Transform recallField = fields.transform.GetChild(Random.RandomRange(0, fields.transform.childCount));
                        RecallField(recallField.position, recallField.gameObject);
                    }
                }
            }
        }
        else
        {
            Debug.Log("필드를 선정하지 않았습니다");
        }
    }

    void RecallField(Vector3 recallPosition, GameObject recallObject)
    {
        animator.SetBool("recallMonster", true);
        Invoke("OnAnimationEnd", 2.33f);
        GameObject instance = Instantiate(Enemy, recallPosition, Quaternion.identity);
        instance.transform.parent = recallObject.transform;
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
        Destroy(recallingEffect);
    }

}
