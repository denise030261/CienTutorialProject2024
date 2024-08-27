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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            curHp -= bullet.damage;
            if (curHp <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

}
