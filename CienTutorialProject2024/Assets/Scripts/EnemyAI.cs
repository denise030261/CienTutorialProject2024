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
    GameObject target;
    Animator animator;
    float playerDist;
    float noMoveDist=1f;
    bool isTarget;
    bool isWall = false;
    EnemyBomb enemyBomb = null;

    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    LayerMask targetMask;
    public LayerMask obstacleMask;

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void Start()
    {
        // 레이어 이름으로 LayerMask 생성
        int layer = LayerMask.NameToLayer("Wall");
        if (layer == -1)
        {
            //Debug.LogError("Layer 'Wall' not found.");
            return;
        }

        // 씬의 모든 게임 오브젝트를 찾음
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> wallObjects = new List<GameObject>();

        // 레이어를 가진 오브젝트 필터링
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                wallObjects.Add(obj);
            }
        }

        // Wall 레이어를 가진 모든 오브젝트를 출력
        foreach (GameObject wallObj in wallObjects)
        {
            Debug.Log("Found Wall object: " + wallObj.name);
        }

        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyBomb = GetComponent<EnemyBomb>();
        target = GameObject.Find("Player");
        targetMask = LayerMask.GetMask("Player");
        //obstacleMask = LayerMask.GetMask("Wall"); // 벽 레이어 마스크 설정
    }

    private void Update()
    {
        isTarget = false;
        ViewOfField();
        ChaseTarget();

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

        if (Targets.Length == 0) return;
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
                Debug.Log("플레이어 발견!");
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
        else if(!isTarget)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("isWalk", false);
        }
    }

}
