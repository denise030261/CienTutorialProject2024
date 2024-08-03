using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecall : MonoBehaviour
{
    [SerializeField] float reloadTime = 5f;
    bool isReload = true;
    ParticleSystem useEffect;

    private List<Vector3> positionHistory; // 그림자오브젝트를 만들어 따라가게 만든다
    private float recordInterval = 0.1f; 
    private float rewindDelay = 3f; 
    private int maxHistoryLength;

    // Start is called before the first frame update
    void Start()
    {
        positionHistory = new List<Vector3>();
        maxHistoryLength = Mathf.CeilToInt(rewindDelay / recordInterval);
        StartCoroutine(RecordPosition());

        useEffect = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        useEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isReload)
        {

            RewindToPreviousPosition();
        }
    }

    void ReLoad()
    {
        isReload = true;
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
            Debug.Log("사용");
            useEffect.Play();
            Invoke("ReLoad", reloadTime);
            isReload = false;
            // 3초 전의 위치로 이동
            transform.parent.gameObject.transform.position = positionHistory[maxHistoryLength - 1];
            // 이동 후 기록 초기화 (원한다면)
            positionHistory.Clear();
        }
        else
        {
            Debug.Log("기록된 위치가 충분하지 않습니다.");
        }
    }
}
