using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject shoot;
    public GameObject projectileObject;
    public bool isShoot = false;
    public bool readyShoot = true;

    [SerializeField] float speed = 2f;
    [SerializeField] float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(isShoot && readyShoot)
        {
            isShoot = false;
            readyShoot = false;
            StartCoroutine(LongAttack());
        }
    }

    IEnumerator LongAttack()
    {
        yield return new WaitForSeconds(0.733f);
        GameObject instance = Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);

        // �Ѿ��� ����� �ӵ� ����
        Vector3 targetDir = target.transform.position - shoot.transform.position;
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.velocity = targetDir.normalized * speed;

        // �Ѿ��� �߻�� ��ġ�� ���
        Vector3 originPos = instance.transform.position;

        Invoke("Reload", 1f);

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

        //readyShoot = true;
    } // rotation ������ ��� �����

    void Reload()
    {
        readyShoot = true;
    }
}
