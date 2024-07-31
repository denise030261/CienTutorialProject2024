using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float dist = 2f;
    [SerializeField] float speed = 2f;
    public Transform targetTransform;

    Vector3 targetPos;
    Vector3 targetDir;
    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        targetPos = targetTransform.position;
        targetPos.y=transform.position.y;
        targetDir = (targetPos - transform.position).normalized;
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
}
