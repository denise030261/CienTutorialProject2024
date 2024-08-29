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

    // Start is called before the first frame update
    void Start()
    {
        platformMaterial = GetComponent<MeshRenderer>().material;
        originColor = platformMaterial.color;
        if(shield == null)
        {
            Debug.LogError("쉴드 배치 바람");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(disappearing) 
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
                        platformMaterial.color = originColor;
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
        gameObject.SetActive(true);
        Transform shieldTransform = gameObject.transform;
        Vector3 shieldPosition = new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z);
        shieldTransform.position = shieldPosition;
        Instantiate(shield, shieldTransform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Disappear();
        }
    } // 플레이어 감지하면 사라지기 시작함
}
