using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public ParticleSystem shieldEffect;

    // Start is called before the first frame update
    void Start()
    {
        shieldEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        Invoke("StopEffect", 1.0f);
    }

    protected void StopEffect()
    {
        Debug.Log("¹æ¾î");
        shieldEffect.Pause();
    }
}
