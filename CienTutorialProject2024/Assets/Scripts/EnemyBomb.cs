using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBomb : MonoBehaviour
{
    bool isBomb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Bomb()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Bomb 상태가 활성화된 경우
        if (stateInfo.IsName("Bomb"))
        {
            isBomb = true;
        }
        if (isBomb)
        {
            // Bomb 상태에서 다른 상태로 전환된 경우
            isBomb = false;

            // Bomb 상태가 종료되었으므로 오브젝트 제거
            SelfDestructEnd();
        }
    }

    void SelfDestructEnd()
    {
        Debug.Log("Bomb");
        Destroy(gameObject);
    }

}
