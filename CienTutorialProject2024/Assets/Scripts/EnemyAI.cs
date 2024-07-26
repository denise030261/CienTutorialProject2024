using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    float playerDist;
    bool isBomb;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        playerDist=Vector3.Distance(transform.position,target.transform.position);

        if (playerDist>=1)
        {
            nav.SetDestination(target.transform.position);
            animator.SetBool("isWalk", true);
        }
        else 
        {
            animator.SetBool("isAttack", true);
            animator.SetBool("isWalk", false);
            nav.SetDestination(transform.position);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Bomb ���°� Ȱ��ȭ�� ���
        if (stateInfo.IsName("Bomb"))
        {
            isBomb = true;
        }
        if (isBomb)
        {
            // Bomb ���¿��� �ٸ� ���·� ��ȯ�� ���
            isBomb = false;

            // Bomb ���°� ����Ǿ����Ƿ� ������Ʈ ����
            SelfDestructEnd();
        }
    }

    void SelfDestructEnd()
    {
        Debug.Log("Bomb");
        Destroy(gameObject);
    }
}
