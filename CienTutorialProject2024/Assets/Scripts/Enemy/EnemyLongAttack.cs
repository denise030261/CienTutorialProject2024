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
        Debug.Log(ViewAngle);
        yield return new WaitForSeconds(0.753f);
        Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);
        projectile=projectileObject.GetComponent<EnemyProjectile>();
        projectile.targetDir = transform.forward;
        readyShoot = true;
    } // rotation 방향대로 쏘게 만들기
}
