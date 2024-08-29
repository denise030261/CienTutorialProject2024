using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallback : MonoBehaviour
{
    public bool endRollAnim = false;
    public bool startHurdle = false;
    public bool endHurdle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartHurdle()
    {
        startHurdle=true;
    }

    void EndHurdle()
    {
        endHurdle=true;
    }
    

    void EndRoll()
    {
        endRollAnim = true;
    }
}
