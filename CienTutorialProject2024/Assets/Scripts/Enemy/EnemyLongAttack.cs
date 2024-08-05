using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject shoot;
    public GameObject projectileObject;
    private EnemyProjectile projectile;
    private EnemyAI ai;
    bool isFirstEnable = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if(isFirstEnable)
        {
            isFirstEnable = false;
            return;
        }
        StartCoroutine(LongAttack());
    }

    IEnumerator LongAttack()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(0.753f);
        Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);
        projectile=projectileObject.GetComponent<EnemyProjectile>();
        projectile.targetTransform = target.transform;
        StartCoroutine(LongAttack());
    } // rotation 방향대로 쏘게 만들기
}
