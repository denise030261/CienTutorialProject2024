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

        // Bomb 상태가 활성화된 경우
        if (stateInfo.IsName("Bomb"))
        {
            isBomb = true;
        }
        if (isBomb)
        {
            // Bomb 상태에서 다른 상태로 전환된 경우
            isBomb = false;

            // Bomb 상태가 종료되었으므로 오브젝트 제거
            SelfDestructEnd();
        }
    }

    void SelfDestructEnd()
    {
        Debug.Log("Bomb");
        Destroy(gameObject);
    }
}
