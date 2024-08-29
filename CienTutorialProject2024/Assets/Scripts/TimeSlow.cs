using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : TimeSkill
{
    [SerializeField] float slowTime = 0.5f;
    [SerializeField] float slowStateTime = 5f;
    TimeRecall timeRecall;
    public GameObject timeSlowcharge;

    private void Start()
    {
        timeRecall=GetComponent<TimeRecall>();
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
        Debug.Log("시간 느리게");
        Time.timeScale = slowTime;
    }

    void OriginTime()
    {
        timeRecall.isReload = true;
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
