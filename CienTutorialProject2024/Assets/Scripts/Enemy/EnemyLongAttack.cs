using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject shoot;
    public GameObject projectileObject;
    private EnemyProjectile projectile;
    public bool isShoot = false;
    public bool readyShoot = true;
    [Range(0f, 360f)] float ViewAngle;

    [SerializeField] float speed = 2f;
    [SerializeField] float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ViewAngle=transform.eulerAngles.y;
        if(isShoot && readyShoot)
        {
            isShoot = false;
            readyShoot = false;
            StartCoroutine(LongAttack());
        }
    }

    IEnumerator LongAttack()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject instance = Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);

        // �Ѿ��� ����� �ӵ� ����
        Vector3 targetDir = transform.forward;
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.velocity = targetDir.normalized * speed;

        // �Ѿ��� �߻�� ��ġ�� ���
        Vector3 originPos = instance.transform.position;

        // �Ѿ��� �̵��� �Ÿ��� ����ϰ�, �ִ� �Ÿ��� �����ϸ� �ı�
        while (true)
        {
            if (instance != null)
            {
                float traveledDistance = Vector3.Distance(originPos, instance.transform.position);

                if (traveledDistance >= maxDistance)
                {
                    Destroy(instance);
                    break;
                }
            }
            else
            {
                break;
            }

            yield return null; // �� �����Ӹ��� üũ
        }

        readyShoot = true;
    } // rotation ������ ��� �����
}
