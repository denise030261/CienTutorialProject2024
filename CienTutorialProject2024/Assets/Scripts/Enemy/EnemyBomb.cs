using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] GameObject effectObject;
    [SerializeField] GameObject visualObject;
    [SerializeField] float bombTime = 1.0f;

    bool isDamage = false;
    List<GameObject> Effects = new List<GameObject>();
  

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < effectObject.transform.childCount; i++) 
        {
            Effects.Add(effectObject.transform.GetChild(i).gameObject);
            Effects[i].SetActive(false);
        }
    }

    public void Bomb()
    {
        Effects[0].SetActive(true);
        Invoke("SelfDestructEnd", bombTime);
    }

    void SelfDestructEnd()
    {
        isDamage = true;
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

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if((other.CompareTag("Player") || other.CompareTag("Shield")) && isDamage)
        {
            Destroy(other.gameObject);
        }
    }
}
