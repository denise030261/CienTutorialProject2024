using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallMonster : MonoBehaviour
{
    [SerializeField] float recallTime = 10f;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
