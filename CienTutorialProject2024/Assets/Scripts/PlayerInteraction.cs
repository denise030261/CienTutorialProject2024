using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    bool getGlass = false;
    bool putGlass = false;
    public bool doneGlass;

    GameObject glass;
    GameObject glassEffect;
    GameObject glassPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(getGlass && Input.GetKeyDown(KeyCode.E))
        {
            glass.transform.parent = transform;
            glassEffect.transform.parent = transform;
            glass.SetActive(false);
            getGlass=false;
        }
        else if(putGlass && Input.GetKeyDown(KeyCode.E))
        {
            glassEffect.transform.parent = glassPlatform.transform;
            glass.transform.parent = glassPlatform.transform;
            glass.SetActive(true);
            glass.transform.localPosition = new Vector3(0, 2, 0);
            glassEffect.transform.localPosition = new Vector3(0, 0, 0);

            glassEffect = null;
            glass = null;
            putGlass = false;
            if (BossStageController.instance != null) 
            {
                BossStageController.instance.page++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Glass") 
        {
            getGlass = true;
            glass = other.gameObject;
            glassEffect = glass.transform.GetChild(0).gameObject;
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        if(glass !=null && other.gameObject.tag != "Glass")
        {
            glassPlatform = other.transform.parent.gameObject;
            if (glassPlatform.name.Substring(0, (glassPlatform.name.Length - 4)) == glass.name.Substring(0, (glass.name.Length - 5)))
            {
                putGlass = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Glass")
        {
            getGlass = false;
            glass = null;
        }
        if (glass != null)
        {
            glassPlatform = other.transform.parent.gameObject;
            if (glassPlatform.name.Substring(0, (glassPlatform.name.Length - 4)) == glass.name.Substring(0, (glass.name.Length - 5)))
            {
                putGlass = false;
            }
        }
    }
}
