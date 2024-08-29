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
    [SerializeField] GameObject projectilePrefab; // �߻��� ��ü�� ������
    [SerializeField] float gravity = 9.8f;
    [SerializeField] GameObject particleObject;
    public float reloadTime;

    private float reloadingTime=0f;
    bool readyShoot = false; // �߻� �غ�
    private Transform startPoint; // �߻� ��ġ
    private GameObject target;
    private Transform targetPoint; // ��ǥ ��ġ
    private float firingAngle = 30f;
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
        reloadingTime += Time.deltaTime;
        if (reloadingTime>=reloadTime && readyShoot)
        {
            reloadingTime = 0f;
            StartCoroutine(SimulateProjectile());
            readyShoot = false;
        }
        else if(reloadingTime >= (reloadTime-0.767f) && DetectPlayer())
        {
            animator.SetBool("isAttack", true);
            readyShoot = true;
        }
    }

    private void LateUpdate()
    {
        if(!readyShoot)
        {
            ViewOfField();
        }
    }

    public IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.767f);
        Vector3 shootPosition= new Vector3(startPoint.position.x + 0.5f, startPoint.position.y + 1.5f, startPoint.position.z);
        animator.SetBool("isAttack", false);
        // �߻��� ��ü ����
        GameObject projectile = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

        // �������� ��ǥ�� ������ �Ÿ� ���
        float target_Distance = Vector3.Distance(startPoint.position, targetPoint.position);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = (target_Distance*2) / Vx;

        projectile.transform.rotation = Quaternion.LookRotation(targetPoint.position - startPoint.position);

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

    bool DetectPlayer()
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
                return true;
            }
        }

        return false;
    }

    void ViewOfField()
    {
        Vector3 targetWaistDir = (target.transform.position - transform.position).normalized;
        waist.transform.rotation = Quaternion.LookRotation(targetWaistDir);

        Vector3 targetHeadDir = (target.transform.position - transform.position).normalized;
        head.transform.rotation = Quaternion.LookRotation(targetHeadDir);

        Debug.DrawLine(transform.position, target.transform.position, Color.blue);
    }
}
