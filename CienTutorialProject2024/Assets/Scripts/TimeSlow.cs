using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    ParticleSystem useEffect;
    // Start is called before the first frame update
    void Start()
    {
        useEffect = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        useEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            useEffect.Play();
        }

        if(Input.GetKey(KeyCode.F))
        {
            Time.timeScale = 0.5f;

        }
        else if(Input.GetKeyUp(KeyCode.F)) 
        {
            Time.timeScale = 1f;
            useEffect.Stop();
        }
    }
}
