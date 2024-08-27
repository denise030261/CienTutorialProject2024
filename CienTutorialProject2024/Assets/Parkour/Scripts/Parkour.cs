using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkour : MonoBehaviour
{
    Animator anim;

    float rollTrigger;
    bool isFalling;
    // Start is called before the first frame update

    void Awake()
    {
        isFalling = false;
        rollTrigger = 2.0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Hang();
        Climb();

    }

    public void Hang()
    {
        if (anim.GetBool("isJump"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isHang", true);
                anim.SetBool("isJump", false);
            }
        }
    }

    public void Climb()
    {
        if (anim.GetBool("isHang"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("doClimb");
                anim.SetTrigger("doClimb");
            }
        }
    }

}
