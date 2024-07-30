using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    GameObject target;
    public EnemyProjectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LongAttack()
    {
        //projectile.targetTransform
        Instantiate(projectile);
    }
}
