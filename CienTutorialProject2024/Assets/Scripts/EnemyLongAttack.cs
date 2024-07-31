using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
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
        Vector3 shootPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.4f, transform.localPosition.z);
        Debug.Log("start");
        yield return new WaitForSeconds(0.833f);
        Instantiate(projectileObject, shootPosition, Quaternion.identity);
        projectile=projectileObject.GetComponent<EnemyProjectile>();
        projectile.targetTransform = target.transform;
    }
}
