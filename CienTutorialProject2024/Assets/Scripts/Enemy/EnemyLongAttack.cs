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
        yield return new WaitForSeconds(0.753f);
        GameObject instance = Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);

        // 총알의 방향과 속도 설정
        Vector3 targetDir = transform.forward;
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.velocity = targetDir.normalized * speed;

        // 총알이 발사된 위치를 기록
        Vector3 originPos = instance.transform.position;

        // 총알이 이동한 거리를 계산하고, 최대 거리에 도달하면 파괴
        while (true)
        {
            // traveledDistance는 총알이 발사된 이후 현재까지 이동한 거리
            float traveledDistance = Vector3.Distance(originPos, instance.transform.position);

            // 만약 총알이 최대 거리 이상 이동하면 파괴
            if (traveledDistance >= maxDistance)
            {
                Destroy(instance);
                break;
            }

            yield return null; // 매 프레임마다 체크
        }

        readyShoot = true;
    } // rotation 방향대로 쏘게 만들기
}
