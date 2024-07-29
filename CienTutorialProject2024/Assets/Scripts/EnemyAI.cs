using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    float playerDist;
    float noMoveDist=1f;
    bool isTarget;
    EnemyBomb enemyBomb = null;

    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    LayerMask targetMask;
    LayerMask obstacleMask;

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        enemyBomb=GetComponent<EnemyBomb>();
        target = GameObject.Find("Player");
        targetMask = LayerMask.GetMask("Player");
        obstacleMask = LayerMask.GetMask("Wall");
    }

    private void Update()
    {
        ViewOfField();
        ChaseTarget();

        DrawDebug();
    }

    void DrawDebug()
    {
        Vector3 viewAngleA = DirFromAngle(-ViewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(ViewAngle / 2, false);
        Debug.DrawLine(transform.position, transform.position + viewAngleA * ViewRadius, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + viewAngleB * ViewRadius, Color.yellow);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void ViewOfField()
    {
        float lookingAngle = transform.eulerAngles.y;  //캐릭터가 바라보는 방향의 각도
        Vector3 lookDir = AngleToDir(lookingAngle);

        Collider[] Targets = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);
        // 벽 처리

        if (Targets.Length == 0) return;
        foreach (Collider targetCollider in Targets)
        {
            Vector3 targetPos = targetCollider.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(transform.position, targetDir, ViewRadius, obstacleMask))
            {
                isTarget = true;
            }
        }
    }

    void ChaseTarget()
    {
        playerDist = Vector3.Distance(transform.position, target.transform.position);

        if (playerDist >= noMoveDist && isTarget)
        {
            nav.SetDestination(target.transform.position);
            animator.SetBool("isWalk", true);
        }
        else if (playerDist < noMoveDist && isTarget)
        {
            animator.SetBool("isAttack", true);
            animator.SetBool("isWalk", false);
            nav.SetDestination(transform.position);
            
            if(enemyBomb!=null)
            {
                enemyBomb.Bomb();
                isTarget = false;
            }
        }
    }

}
