using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public Animator Animator;
    public EnemyBomb BombComponent { get; private set; }
    public EnemyLongAttack LongAttackComponent { get; private set; }

    [SerializeField] private LayerMask targetMask;
    [SerializeField, Range(0, 360)] private float viewAngle = 90f;
    [SerializeField] private float viewRadius = 5f;
    [SerializeField] private float attackRange = 2f;
    public Transform Target { get; private set; }

    public float ViewAngle => viewAngle;
    public float ViewRadius => viewRadius;
    public float AttackRange => attackRange;


    private IState currentState;
    public IdleState IdleState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public AttackState AttackState { get; private set; }

    private float initialSpeed;
    private float doubleSpeed;
    private float tripleSpeed;


    private void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        BombComponent = GetComponent<EnemyBomb>();
        LongAttackComponent = GetComponent<EnemyLongAttack>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        initialSpeed = NavAgent.speed;
        doubleSpeed = initialSpeed * 2;
        tripleSpeed = initialSpeed * 3;

        IdleState = new IdleState(this);
        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentEnemy();
        currentState?.Tick();
    }

    // 상태를 전환하는 핵심 메서드
    public void ChangeState(IState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    private void CurrentEnemy()
    {
        // Boss Stage에 따른 변화
        if (BossStageController.instance != null)
        {
            switch (BossStageController.instance.page)
            {
                case 2:
                    viewRadius = 7f;
                    NavAgent.speed = doubleSpeed;
                    break;
                case 3:
                    viewRadius = 9f;
                    break;
                case 4:
                    viewRadius = 15f;
                    NavAgent.speed = tripleSpeed;
                    break;
                default:
                    viewRadius = 5f;
                    NavAgent.speed = initialSpeed;
                    break;
            }
        }

        if (GameManager.Instance != null && GameManager.Instance.isSlow)
        {
            NavAgent.speed = initialSpeed / 4f;
        }
    }

    // 플레이어가 시야 내에 있고, 장애물에 가려지지 않았는지 확인
    public bool CanSeePlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length > 0)
        {
            Transform playerTransform = targetsInViewRadius[0].transform;
            Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;

            // 시야각 안에 있는지 확인
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                // 플레이어 사이에 벽(Wall)이 없는지 
                if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, LayerMask.GetMask("Wall")))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RotateTowardsTarget()
    {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}