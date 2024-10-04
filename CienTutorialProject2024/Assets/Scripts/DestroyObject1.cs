using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }
        Destroy(other.gameObject);
    }
}
