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
        Debug.Log("Bomb");
        Effects[0].SetActive(false);
        Effects[1].SetActive(true);
        
        visualObject.SetActive(false);
        isDamage = true;
        StartCoroutine(BombAnimation());
    }


    IEnumerator BombAnimation()
    {
        yield return new WaitForSeconds(Effects[1].GetComponent<ParticleSystem>().main.duration+1f);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(isDamage)
        {
            if(other.CompareTag("Player"))
            {
                foreach (Transform child in other.gameObject.transform)
                {
                    if (child.gameObject.tag == "Shield")
                    {
                        Destroy(child.gameObject);
                        isDamage = false;
                        Debug.Log(other.name);
                        return;
                    }
                }

                other.gameObject.SetActive(false);
                GameManager.Instance.GameOver();
                isDamage = false;
            }
        }
    }
}
