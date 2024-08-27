using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 20f; 
    [SerializeField] float maxDistance = 50f; 
    public Vector3 targetDir;  

    private Rigidbody rb;
    private Vector3 originPos;  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originPos = transform.position;
        rb.velocity = targetDir.normalized * speed;
    }

    void Update()
    {
        float traveledDistance = Vector3.Distance(originPos, transform.position);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("명중");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // 발사체가 화면 밖으로 나갔을 때 제거
        Destroy(gameObject);
    }
}
