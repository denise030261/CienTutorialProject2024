using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float dist = 2f;
    [SerializeField] float speed = 2f;

    public Vector3 targetDir;
    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, originPos + targetDir * dist, speed * Time.deltaTime);

        if(transform.position== originPos + targetDir * dist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 체력 깎는 코드
            Debug.Log("명중");
            Destroy(gameObject);
        }
    }
}
