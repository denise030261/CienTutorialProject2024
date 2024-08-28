using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float disappearSpeed=5f;
    bool disappearing = false; // 알파값
    Material platformMaterial;

    // Start is called before the first frame update
    void Start()
    {
        platformMaterial = GetComponent<MeshRenderer>().material;
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
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Disappear()
    {
        disappearing = true;
    }
}
