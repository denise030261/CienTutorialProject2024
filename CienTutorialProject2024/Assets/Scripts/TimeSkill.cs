using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkill : MonoBehaviour
{
    [SerializeField] protected float reloadTime = 5f;
    [SerializeField] protected ParticleSystem useEffect;
    public bool isReload = true;

    private void Awake()
    {
        useEffect.Stop();
    }

    protected void ReLoad()
    {
        isReload = true;
    }

    protected void SkillUse()
    {
        useEffect.Play();
        Invoke("ReLoad", reloadTime);
        isReload = false;
    }

    /*protected void SkillCharge()
    {
        if (isReload)
        {
            timeRecallcharge.SetActive(true);
        }
        else
        {
            timeRecallcharge.SetActive(false);
        }
    }*/
}
