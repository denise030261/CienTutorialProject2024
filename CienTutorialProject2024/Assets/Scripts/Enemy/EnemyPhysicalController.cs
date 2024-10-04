using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPhysicalController : MonoBehaviour
{
    GameObject floor = null;
    Rigidbody rb;
    NavMeshAgent nav;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = transform.parent.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (floor != null) 
        {
            if (!floor.activeSelf)
            {
                rb.isKinematic = false;
                nav.enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            floor = other.gameObject;
        }
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
