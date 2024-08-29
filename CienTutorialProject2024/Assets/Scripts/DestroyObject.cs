using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int maxHp = 5;
    public int curHp;
    [SerializeField] GameObject destroiedObject;

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
                destroiedObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
