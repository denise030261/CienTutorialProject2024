using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private readonly EnemyAI ai;

    public ChaseState(EnemyAI enemyAI) => ai = enemyAI;

    public void OnEnter()
    {
        ai.Animator.SetBool("isWalk", true);
    }

    public void Tick()
    {
        // 플레이어를 놓쳤는가
        if (!ai.CanSeePlayer())
        {
            ai.ChangeState(ai.IdleState);
            return;
        }

        float distanceToPlayer = Vector3.Distance(ai.transform.position, ai.Target.position);

        // 공격 범위에 들어왔는가
        if (distanceToPlayer <= ai.AttackRange)
        {
            ai.ChangeState(ai.AttackState);
            return;
        }

        // 플레이어를 향해 이동
        ai.NavAgent.SetDestination(ai.Target.position);
    }

    public void OnExit()
    {
        ai.Animator.SetBool("isWalk", false);
        if (ai.NavAgent.hasPath)
        {
            ai.NavAgent.ResetPath();
        }
    }
}