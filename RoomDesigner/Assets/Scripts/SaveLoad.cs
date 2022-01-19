using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public GameData gameData;
    public GameObject colorPanel;
    public ObjectSpawner spawner;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Save(int saveSlot)
    {

        string fileName = "room" + saveSlot + ".dat";
        if (!File.Exists(fileName))
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, gameData.roomObjects);
            fs.Close();
        }
        else
        {
            File.Delete(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, gameData.roomObjects);
            fs.Close();
        }

    }

    public void Load(int saveSlot)
    {
        if (gameData == null)
            gameData = GameObject.Find("GameData").GetComponent<GameData>();

        if (gameData == null)
            gameData = new GameData();
        if (CheckRoom(saveSlot))
        {
            string fileName = "room" + saveSlot + ".dat";
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                var bformatter = new BinaryFormatter();

                gameData.roomObjects = (List<RoomObject>)bformatter.Deserialize(stream);
            }

            SceneManager.LoadScene(1);
        }
    }

    public void SpawnObjects()
    {
        foreach (var gameObject in gameData.roomObjects)
        {
            Vector3 position = new Vector3();
            Vector3 rotation = new Vector3();
            int color = gameObject.color;
            string name = gameObject.Name;
            GameObject objectType = getObject(gameObject.Name);
            position.x = gameObject.posX;
            position.y = gameObject.posY;
            position.z = gameObject.posZ;

            rotation.x = gameObject.rotX;
            rotation.y = gameObject.rotY;
            rotation.z = gameObject.rotZ;
            GameObject newObject = Instantiate(objectType, position, Quaternion.Euler(rotation));
            newObject.name = name;
            spawner.changeColorLoad(newObject, color);

        }
    }


    GameObject getObject(string name)
    {
        GameObject gameObject = null;

        if (name.Contains("Table"))
            gameObject = spawner.table;
        if (name.Contains("AngleTable"))
            gameObject = spawner.table2;
        if (name.Contains("Closet"))
            gameObject = spawner.closet;
        if (name.Contains("WallLight"))
            gameObject = spawner.wallLight;
        if (name.Contains("Chair"))
            gameObject = spawner.chair;
        if (name.Contains("Bed"))
            gameObject = spawner.bed;
        if (name.Contains("CellingLight"))
            gameObject = spawner.cellingLight;
        if (name.Contains("Monitor"))
            gameObject = spawner.monitor;
        if (name.Contains("PC"))
            gameObject = spawner.pc;


        return gameObject;
    }

    public bool CheckRoom(int saveSlot) {
        string fileName = "room" + saveSlot + ".dat";
        if (File.Exists(fileName))
            return true;
        else
            return false;

    }
    public void AddObject(GameObject Object, int color)
    {
        bool replaced = false;
        RoomObject roomObject = new RoomObject();

        roomObject.Name = Object.name;
        roomObject.color = color;
        roomObject.posX = Object.transform.position.x;
        roomObject.posY = Object.transform.position.y;
        roomObject.posZ = Object.transform.position.z;

        roomObject.rotX = Object.transform.rotation.eulerAngles.x;
        roomObject.rotY = Object.transform.rotation.eulerAngles.y;
        roomObject.rotZ = Object.transform.rotation.eulerAngles.z;
        
        for (int i = 0; i < gameData.roomObjects.Count; i++)
        {
           
            if (gameData.roomObjects[i].Name.Equals(Object.name))
            {
                gameData.roomObjects[i] = roomObject;
                replaced = true;
            }
        }
        if (!replaced)
        {
            gameData.roomObjects.Add(roomObject);
        }
    }
    public void RemoveObject(GameObject Object)
    {
        for (int i = 0; i < gameData.roomObjects.Count; i++)
        {
            
            if (gameData.roomObjects[i].Name.Equals(Object.name))
                gameData.roomObjects.RemoveAt(i);
        }
    }
}

