using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public int maxHp = 50;
    public int curHp;

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        curHp = maxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            curHp -= bullet.damage;
            if (curHp<=0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
