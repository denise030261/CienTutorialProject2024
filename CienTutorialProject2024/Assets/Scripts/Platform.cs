using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float disappearSpeed=5f;
    bool disappearing = false; // 알파값
    Material platformMaterial;

    [SerializeField] bool haveShield = false; // 다시 생성용 또는 사라지는 용
    [SerializeField] GameObject shield;
    [SerializeField] float createSpeed = 3f;

    Color originColor;
    Transform originTransform;
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        platformMaterial = GetComponent<MeshRenderer>().material;
        collider = GetComponent<Collider>();
        originColor = platformMaterial.color;
        originTransform = gameObject.transform;
        if (shield == null)
        {
            Debug.LogError("쉴드 배치 바람");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disappearing) 
        {
            if (platformMaterial != null)
            {
                Color curColor = platformMaterial.color;
                platformMaterial.color = 
                    new Color(curColor.r, curColor.g, curColor.b, curColor.a-Time.deltaTime*disappearSpeed);

                if(platformMaterial.color.a<=0f)
                {
                    if(!haveShield)
                     {
                         Destroy(gameObject);
                     }
                     else 
                     {
                        gameObject.SetActive(false);
                         Invoke("HaveShield", createSpeed);
                     }
                }
            }
        }
    }

    public void Disappear()
    {
        disappearing = true;
    }

    void HaveShield()
    {
        if (ShieldProbability())
        {
            Instantiate(shield, transform);
        }
        platformMaterial.color = originColor;
        gameObject.SetActive(true);
        disappearing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Disappear();
        }
    }

    bool ShieldProbability()
    {
        int num = Random.Range(1, 101);
        if(num<=5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
