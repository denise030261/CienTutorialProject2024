using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyParabolaAttack : MonoBehaviour
{
    [SerializeField]  private GameObject head;
    [SerializeField]  private GameObject waist;
    [SerializeField] float detectionRadius = 10f;
    public float reloadTime;

    private float reloadingTime=0f;
    private Transform startPoint; // �߻� ��ġ
    private GameObject target;
    private Transform targetPoint; // ��ǥ ��ġ
    public GameObject projectilePrefab; // �߻��� ��ü�� ������
    private float firingAngle = 30f;
    private float gravity = 9.8f;
    Animator animator;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetPoint = target.transform;
        startPoint = gameObject.transform;
        animator = GetComponent<Animator>();
    }


    GameObject FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child.gameObject;

            GameObject result = FindDeepChild(child, name);
            if (result != null)
                return result.gameObject;
        }
        return null;
    }

    void Update()
    {
        DetectPlayer();

        reloadingTime += Time.deltaTime;
        if (reloadingTime>=reloadTime)
        {
            reloadingTime = 0f;
            StartCoroutine(SimulateProjectile());
        }
        else if(reloadingTime >= (reloadTime-0.767f))
        {
            animator.SetBool("isAttack", true);
        }
    }

    public IEnumerator SimulateProjectile()
    {
        Vector3 shootPosition= new Vector3(startPoint.position.x + 0.5f, startPoint.position.y + 1.5f, startPoint.position.z);
        animator.SetBool("isAttack", false);
        // �߻��� ��ü ����
        GameObject projectile = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

        // �������� ��ǥ�� ������ �Ÿ� ���
        float target_Distance = Vector3.Distance(startPoint.position, targetPoint.position);

        // �ʱ� �ӵ� ���
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // XZ ��鿡���� �ӵ� ���
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // ���� �ð� ���
        float flightDuration = target_Distance / Vx;

        // �߻� ���� ����
        projectile.transform.rotation = Quaternion.LookRotation(targetPoint.position - startPoint.position);

        // ���� �ð� ���� �̵�
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            projectile.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }

        // ���𰡰� �ε����� �ı� + ȿ��
        Destroy(projectile);
    }

    void DetectPlayer()
    {
        Vector3 targetWaistDir = (target.transform.position - transform.position).normalized;

        Debug.DrawLine(transform.position, transform.position + targetWaistDir * detectionRadius, Color.yellow);
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                target = hit.gameObject;
                ViewOfField();
                break;
            }
        }
    }

    void ViewOfField()
    {
        Vector3 targetWaistDir = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(targetWaistDir);

        Debug.DrawLine(transform.position, target.transform.position, Color.blue);
    }
}
