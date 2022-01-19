using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public GameObject tips;
    public ObjectSpawner spawner;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawner.spawned)
        {
            RaycastHit hitInfo = new RaycastHit();
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(screenCenter), out hitInfo);
            if (hit)
            {
                if (CheckTags(hitInfo))
                {
                    tips.SetActive(true);
                    spawner.activeObject = hitInfo.transform.gameObject;
                  
                }
                else
                {
                    tips.SetActive(false);
                    spawner.activeObject = null;
                    spawner.possitionController = null;
                }

            }
            else
            {
                tips.SetActive(false);
                spawner.activeObject = null;
                spawner.possitionController = null;
            }
        }
    }

    bool CheckTags(RaycastHit hit)
    {
        if (!hit.transform.gameObject.tag.Equals("Wall") && !hit.transform.gameObject.tag.Equals("Floor")
            && !hit.transform.gameObject.tag.Equals("Celling") && !hit.transform.gameObject.tag.Equals("Player")
            && !hit.transform.gameObject.tag.Equals("MainCamera"))
            return true;
        else
            return false;
    }
}
