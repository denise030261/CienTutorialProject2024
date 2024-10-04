using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPhysicalController : MonoBehaviour
{
    GameObject floor = null;
    Rigidbody rb;
    NavMeshAgent nav;
    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = transform.parent.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.up * -1, out hit))
        {
            if(!(hit.collider.gameObject.tag == "Platform"))
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
