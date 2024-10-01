using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public ParticleSystem shieldEffect;
    bool isStop = false;
    float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shieldEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        currentTime = 0f;
        shieldEffect.Play();
    }

    private void Update()
    {
        currentTime += Time.fixedDeltaTime;
        if(currentTime >= 0.7f && !isStop)
        {
            isStop = true;
            shieldEffect.Pause();
        }
    }
}
