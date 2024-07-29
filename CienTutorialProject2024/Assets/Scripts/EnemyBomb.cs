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

        // Bomb ���°� Ȱ��ȭ�� ���
        if (stateInfo.IsName("Bomb"))
        {
            isBomb = true;
        }
        if (isBomb)
        {
            // Bomb ���¿��� �ٸ� ���·� ��ȯ�� ���
            isBomb = false;

            // Bomb ���°� ����Ǿ����Ƿ� ������Ʈ ����
            SelfDestructEnd();
        }
    }

    void SelfDestructEnd()
    {
        Debug.Log("Bomb");
        Destroy(gameObject);
    }

}
