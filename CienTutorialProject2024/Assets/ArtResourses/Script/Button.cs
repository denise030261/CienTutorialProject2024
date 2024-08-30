using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] gate;
    bool isInteract;
    void Start()
    {
        isInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInteract)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, -transform.localScale.y *2f, 0);
            for(int i = 0; i < gate.Length; i++)
            {
                gate[i].SetActive(false);
            }
            Debug.Log("button Pressed");
            isInteract = true;
        }

    }
}
