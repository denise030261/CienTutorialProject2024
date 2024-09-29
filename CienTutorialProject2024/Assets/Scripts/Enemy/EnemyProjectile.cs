using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
    }
}
