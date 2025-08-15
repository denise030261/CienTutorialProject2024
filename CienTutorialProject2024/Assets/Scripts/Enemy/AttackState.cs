using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private readonly EnemyAI ai;
    private float attackCooldownTimer;
    private const float attackCoolTime = 1.5f;

    public AttackState(EnemyAI enemyAI) => ai = enemyAI;

    public void OnEnter()
    {
        ai.Animator.SetBool("isWalk", false);
        ai.Animator.SetBool("isAttack", true);

        if (ai.NavAgent.hasPath)
        {
            ai.NavAgent.ResetPath();
        }

        ai.RotateTowardsTarget();

        // 공격 유형에 따른 처리
        if (ai.BombComponent != null)
        {
            ai.BombComponent.Bomb();
            ai.enabled = false; 
        }
        else if (ai.LongAttackComponent != null)
        {
            if (ai.LongAttackComponent.readyShoot)
            {
                ai.LongAttackComponent.isShoot = true;
            }
            else
            {
                // 아직 쏠 준비가 안됐으면 바로 추적 상태로 복귀
                ai.ChangeState(ai.ChaseState);
            }
        }

        attackCooldownTimer = attackCoolTime;
    }

    public void Tick()
    {
        if (!ai.enabled) return;

        ai.RotateTowardsTarget();

        attackCooldownTimer -= Time.deltaTime;

        // 공격 쿨타임이 다 되었는가?
        if (attackCooldownTimer <= 0)
        {
            // 공격 후 다시 플레이어와의 거리를 체크하여 다음 상태 결정
            float distanceToPlayer = Vector3.Distance(ai.transform.position, ai.Target.position);
            if (distanceToPlayer <= ai.AttackRange && ai.CanSeePlayer())
            {
                // 아직 공격 범위 내에 있다면 다시 공격 (혹은 잠시 대기 후 공격)
                ai.ChangeState(ai.AttackState);
            }
            else
            {
                // 공격 범위를 벗어났다면 추적 상태로
                ai.ChangeState(ai.ChaseState);
            }
        }
    }

    public void OnExit()
    {
        ai.Animator.SetBool("isAttack", false);
        if (ai.LongAttackComponent != null)
        {
            ai.LongAttackComponent.isShoot = false;
        }
    }
}