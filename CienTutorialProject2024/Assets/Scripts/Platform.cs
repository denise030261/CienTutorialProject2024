using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float disappearTime=5f;
    ParticleSystem disappearParticleSystem;
    bool disappearing = false; // 알파값

    // Start is called before the first frame update
    void Start()
    {
        disappearParticleSystem = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        disappearParticleSystem.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(!disappearParticleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
        if(disappearing) 
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = 0f;  // 완전히 투명하게 설정
                renderer.material.color = color;
            }

            // 오브젝트를 서서히 나타나게 하려면
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = 1f;  // 완전히 불투명하게 설정
                renderer.material.color = color;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("StartDestroy", disappearTime);
            disappearing = true;
        }
    }

    void StartDestroy()
    {
        disappearParticleSystem.Play();
    }
}
