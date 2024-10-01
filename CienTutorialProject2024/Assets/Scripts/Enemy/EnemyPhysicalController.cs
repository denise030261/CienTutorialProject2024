using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPhysicalController : MonoBehaviour
{
    bool isPlatform = true;
    bool isCheck = false;
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
        if(!isPlatform && isCheck)
        {
            rb.isKinematic = false;
            nav.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isPlatform = false;
        isCheck = false;
        if (other.gameObject.tag=="Platform")
        {
            isPlatform = true;
        }
        isCheck = true;
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
