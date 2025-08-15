using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private readonly EnemyAI ai;

    public IdleState(EnemyAI enemyAI) => ai = enemyAI;

    public void OnEnter()
    {
        ai.Animator.SetBool("isWalk", false);
        if (ai.NavAgent.hasPath)
        {
            ai.NavAgent.ResetPath();
        }
    }

    public void Tick()
    {
        // 상태 전환 조건: 플레이어를 볼 수 있는가?
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(ai.ChaseState);
        }
    }

    public void OnExit() { }
}
