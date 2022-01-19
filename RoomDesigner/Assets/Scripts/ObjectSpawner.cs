using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject table;
    public GameObject table2;
    public GameObject bed;
    public GameObject closet;
    public GameObject chair;

    public GameObject monitor;
    public GameObject pc;
    public GameObject cellingLight;
    public GameObject wallLight;
    public GameObject colorPickerPanel;

    public GameObject player;
    public GameObject activeObject;
    Rigidbody activeObjectRig;
    Vector3 objectTransform;
    Vector3 objectRotation;
    public bool spawned = false;
    public PossitionController possitionController;
    public SaveLoad saveLoad;
    public GameObject colorPicker;
    int counter = 0;
    bool loaded;

    public StarterAssetsInputs playerScript;
    // Start is called before the first frame update
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded)
        {
            if (saveLoad.gameData != null)
            {
                if (saveLoad.gameData.roomObjects.Count > 0)
                {
                    saveLoad.SpawnObjects();
                }
                loaded = true;
            }
        }
        if (activeObject!=null || spawned)
        {
           
            if(Input.mouseScrollDelta.y != 0)
            {
                float newPosition = Input.mouseScrollDelta.y * 0.1f;
                objectTransform.z += newPosition;
                if(possitionController!=null)
                    possitionController.startPosition = objectTransform;
                activeObject.transform.localPosition = objectTransform;
            }
            if (Input.GetKey(KeyCode.X))
            {
                if(possitionController==null)
                     possitionController = activeObject.GetComponent<PossitionController>();

                playerScript.cursorInputForLook = false;
                activeObject.transform.Rotate(0, (Input.GetAxis("Mouse X") * -600 * Time.deltaTime), 0, Space.World);
                possitionController.objectRotation = activeObject.transform.rotation.eulerAngles;
            }
            else if (Input.GetKeyUp(KeyCode.X)){
                playerScript.cursorInputForLook = true;
                saveLoad.AddObject(activeObject, possitionController.colorCode);
            }
            objectTransform = activeObject.transform.localPosition;
           
            if (Input.GetMouseButtonDown(0) && activeObject.transform.parent!=null)
            {
               
                activeObject.transform.parent = null;
                saveLoad.AddObject(activeObject, possitionController.colorCode);
                if(activeObjectRig!=null)
                    activeObjectRig.constraints = RigidbodyConstraints.FreezeAll;
                spawned = false;
                activeObject = null;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                saveLoad.RemoveObject(activeObject);
                Destroy(activeObject);
                activeObject = null;
                spawned = false;
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (possitionController == null)
                    possitionController = activeObject.GetComponent<PossitionController>();
                activeObject.transform.parent = player.transform;
                possitionController.startPosition = activeObject.transform.localPosition;               
                spawned = true;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                
                if (colorPickerPanel.activeSelf)
                {
                    colorPickerPanel.SetActive(false);
                    playerScript.cursorInputForLook = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    colorPickerPanel.SetActive(true);
                    playerScript.cursorInputForLook = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                
            }


        }
    }
    public void SpawnObject(string name)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 10f;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        if (name.Equals("Table")){
            activeObject = Instantiate(table, spawnPos, playerRotation);              
            objectRotation = new Vector3(0, 0, 0);      
        }
        if (name.Equals("Table2"))
        {
            activeObject = Instantiate(table2, spawnPos, playerRotation);
            objectRotation = new Vector3(0, 0, 0);
        }
        if (name.Equals("Chair"))
        {
            activeObject = Instantiate(chair, spawnPos, playerRotation);
            objectRotation = new Vector3(0, 0, 0);
        }
        if (name.Equals("Closet"))
        {
            activeObject = Instantiate(closet, spawnPos, playerRotation);
            objectRotation = new Vector3(0, 0, 0);
        }
        if (name.Equals("Bed"))
        {
            activeObject = Instantiate(bed, spawnPos, playerRotation);
            objectRotation = new Vector3(0, 0, 0);
        }
        if (name.Equals("WallLight"))
        {
            activeObject = Instantiate(wallLight, spawnPos, playerRotation);
        }
        if (name.Equals("CellingLight"))
        {
            activeObject = Instantiate(cellingLight, spawnPos, playerRotation);
        }
        if (name.Equals("Monitor"))
        {
            activeObject = Instantiate(monitor, spawnPos, playerRotation);
        }
        if (name.Equals("PC"))
        {
            activeObject = Instantiate(pc, spawnPos, playerRotation);
        }
        activeObject.name = string.Format("{0}{1}", activeObject.name, counter.ToString());
        activeObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        possitionController = activeObject.GetComponent<PossitionController>();
        activeObject.transform.parent = player.transform;
        activeObjectRig = activeObject.GetComponent<Rigidbody>();
        spawned = true;
        counter++;
    }

    public void changeColor(GameObject button)
    {

        GameObject child;
        if (possitionController == null)
            possitionController = activeObject.GetComponent<PossitionController>();

        for (int i = 0; i < activeObject.transform.childCount; i++)
        {
            child = activeObject.transform.GetChild(i).gameObject;
            if (child.layer == LayerMask.NameToLayer("Paintable"))
            {
                child.GetComponent<Renderer>().material.color = button.GetComponent<Image>().color;
            }
            if (child.layer == LayerMask.NameToLayer("Light"))
            {
                child.GetComponent<Light>().color = button.GetComponent<Image>().color;
            }
        }
       
        possitionController.colorCode = int.Parse(button.name);
        saveLoad.AddObject(activeObject, possitionController.colorCode);
       
    }

    public void changeColorLoad(GameObject gameObject, int color)
    {
        GameObject button = colorPicker.transform.Find(color.ToString()).gameObject;
        GameObject child;
        if (possitionController == null)
            possitionController = gameObject.GetComponent<PossitionController>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            child = gameObject.transform.GetChild(i).gameObject;
            if (child.layer == LayerMask.NameToLayer("Paintable"))
            {
                child.GetComponent<Renderer>().material.color = button.GetComponent<Image>().color;
            }
            if (child.layer == LayerMask.NameToLayer("Light"))
            {
                child.GetComponent<Light>().color = button.GetComponent<Image>().color;
            }
        }

        possitionController.colorCode = int.Parse(button.name);
        possitionController = null;

    }

}
