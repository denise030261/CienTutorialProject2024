using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;
using Debug = UnityEngine.Debug;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;
    Animator animator;
    LayerMask targetMask;
    float playerDist;
    bool isTarget;
    bool isWall = false;
    EnemyBomb enemyBomb = null;
    EnemyLongAttack enemyLongAttack = null;

    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] float noMoveDist = 1f;
    public GameObject target;

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyBomb = GetComponent<EnemyBomb>();
        enemyLongAttack = GetComponent<EnemyLongAttack>();
        target = GameObject.Find("Player");
        targetMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        isTarget = false;
        ViewOfField();

        ViewOfFieldDebug();
    }

    void ViewOfFieldDebug()
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
        float lookingAngle = transform.eulerAngles.y;  // 캐릭터가 바라보는 방향의 각도
        Vector3 lookDir = AngleToDir(lookingAngle);

        Collider[] Targets = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);

        if (Targets.Length == 0)
        {
            animator.SetBool("isWalk", false);
            return;
        }
        foreach (Collider targetCollider in Targets)
        {
            Vector3 targetPos = targetCollider.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetDist = Vector3.Distance(transform.position, targetCollider.gameObject.transform.position);
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

            RaycastHit hit;
            if(Physics.Raycast(transform.position, targetDir, out hit,targetDist) && hit.collider.tag == "Wall")
            {
                isWall = true;
            }
            else
            {
                isWall = false;
            }

            if (targetAngle <= ViewAngle * 0.5f && !isWall)
            {
                isTarget = true;
                ChaseTarget();
            }
            else if(targetAngle> ViewAngle * 0.5f)
            {
                nav.SetDestination(transform.position);
                animator.SetBool("isWalk", false);
            }
            else if(enemyLongAttack != null)
            {
                if(enemyLongAttack.enabled == true)
                {
                    enemyLongAttack.enabled = false;
                }
            }
        }
    }

    void ChaseTarget()
    {
        playerDist = Vector3.Distance(transform.position, target.transform.position);

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (playerDist >= noMoveDist && isTarget)
        {
            nav.SetDestination(target.transform.position);
            animator.SetBool("isWalk", true);
            nav.autoBraking = false; 
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
            else if(enemyLongAttack!=null) 
            {
                isTarget = false;
                if(enemyLongAttack.enabled==false)
                {
                    enemyLongAttack.enabled = true;
                }
            }
        }
        else if(!isTarget)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("isWalk", false);
            nav.autoBraking = true;
        }
    }
}
