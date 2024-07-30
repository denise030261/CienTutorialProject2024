using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] GameObject effectObject;
    [SerializeField] GameObject visualObject;

    bool isBomb;
    Animator animator;
    List<GameObject> Effects = new List<GameObject>();
  

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < effectObject.transform.childCount; i++) 
        {
            Effects.Add(effectObject.transform.GetChild(i).gameObject);
            Effects[i].SetActive(false);
        }
    }

    public void Bomb()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Effects[0].SetActive(true);

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
        Effects[0].SetActive(false);
        Effects[1].SetActive(true);
        visualObject.SetActive(false);
        StartCoroutine(BombAnimation());
    }


    IEnumerator BombAnimation()
    {
        yield return new WaitForSeconds(Effects[1].GetComponent<ParticleSystem>().main.duration+1f);
        Destroy(gameObject);

    }
}
