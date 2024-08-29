using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSheld : MonoBehaviour
{
    [SerializeField] ParticleSystem shieldEffect;

    // Start is called before the first frame update
    void Start()
    {
        shieldEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        Invoke("StopEffect", 1.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 발사체 없애기
    }

    void StopEffect()
    {
        Debug.Log("방어");
        shieldEffect.Pause();
    }
}
