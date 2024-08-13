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
    [Range(0f, 360f)] float ViewAngle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LongAttack());
    }

    private void Update()
    {
        ViewAngle=transform.eulerAngles.y;
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
        Debug.Log("발사");
        yield return new WaitForSeconds(0.753f);
        Instantiate(projectileObject, shoot.transform.position, Quaternion.identity);
        projectile=projectileObject.GetComponent<EnemyProjectile>();
        projectile.targetDir = transform.forward;
        this.enabled = false;
    } // rotation 방향대로 쏘게 만들기
}
