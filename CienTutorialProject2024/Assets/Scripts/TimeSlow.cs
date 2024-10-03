using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : TimeSkill
{
    [SerializeField] float slowTime = 0.5f;
    [SerializeField] float slowStateTime = 5f;
    TimeRecall timeRecall;
    PlayerMovement playerMovement;
    public GameObject timeSlowcharge;

    private void Start()
    {
        playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement>();
        timeRecall =GetComponent<TimeRecall>();
        reloadTime += slowStateTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeslowCharge();

        if (Input.GetKeyDown(KeyCode.F) && isReload)
        {
            timeRecall.isReload = false;
            SlowState();
            SkillUse();
            Invoke("OriginTime", slowStateTime);
        }
    }

    void SlowState()
    {
        playerMovement.speed *= 2;
        playerMovement.jumpPower /= 1.2f;
    }

    void OriginTime()
    {
        timeRecall.isReload = true;
        playerMovement.speed /= 2;
        playerMovement.jumpPower *= 1.2f;
        Time.timeScale = 1f;
        useEffect.Stop();
    }

    void TimeslowCharge()
    {
        if (isReload)
        {
            timeSlowcharge.SetActive(true);
        }
        else
        {
            timeSlowcharge.SetActive(false);
        }
    }
}
