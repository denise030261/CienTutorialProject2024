using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecall : TimeSkill
{
    private List<Vector3> positionHistory; 
    private float recordInterval = 0.1f; 
    private float rewindDelay = 3f; 
    private int maxHistoryLength;
    Vector3 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        positionHistory = new List<Vector3>();
        maxHistoryLength = Mathf.CeilToInt(rewindDelay / recordInterval);
        StartCoroutine(RecordPosition());
        originPosition = transform.parent.gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G) && isReload)
        {
            RewindToPreviousPosition();
        }
    }

    private IEnumerator RecordPosition()
    {
        while (true)
        {
            positionHistory.Insert(0, transform.position);

            if (positionHistory.Count > maxHistoryLength)
            {
                positionHistory.RemoveAt(positionHistory.Count - 1);
            }

            yield return new WaitForSeconds(recordInterval);
        }
    }

    private void RewindToPreviousPosition()
    {
        if (positionHistory.Count >= maxHistoryLength)
        {
            SkillUse();

            transform.parent.gameObject.transform.position = positionHistory[maxHistoryLength - 1];
            Debug.Log(positionHistory[maxHistoryLength - 1]);
            positionHistory.Clear();
            originPosition = transform.parent.gameObject.transform.position;
        }
        else
        {
            SkillUse();

            transform.parent.gameObject.transform.position = originPosition;
            positionHistory.Clear();
            Debug.Log("기록된 위치가 충분하지 않습니다.");
        }
    }
}
