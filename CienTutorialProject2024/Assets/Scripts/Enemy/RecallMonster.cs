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
    int recallCount = 1;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        recallingEffect.SetActive(false);
        Invoke("RecallingMonster", recallTime);
        Invoke("OnAnimationStart", recallTime - 1f);
    }

    private void Update()
    {
        if (BossStageController.instance != null)
        {
            if (BossStageController.instance.page == 3)
            {
                recallCount = 2;
            }
        }
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
                        if (field.childCount >= recallCount)
                        {
                            isNeed = false;
                            break;
                        }
                    }

                    if(isNeed)
                    {
                        Debug.Log("소환");
                        isCreate = true;
                        Transform recallField = fields.transform.GetChild(Random.RandomRange(0, fields.transform.childCount));
                        RecallField(recallField.position, recallField.gameObject);
                    }
                }
            }

            if(!isCreate)
            {
                Invoke("RecallingMonster", recallTime);
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
