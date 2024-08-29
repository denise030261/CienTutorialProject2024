using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    [SerializeField] GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if (shield != null)
            {
                // ���� �ִ��� Ȯ��
                GameObject instance = Instantiate(shield, other.transform);
                instance.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                instance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("���带 ��ġ���ּ���");
            }
        }
    }
}
