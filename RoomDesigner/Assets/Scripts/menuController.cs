using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject loadPanel;
    public GameObject infoPanel;
    public GameData gameData;
    public SaveLoad saveLoad;

    public TMP_Text room1;
    public TMP_Text room2;
    public TMP_Text room3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameData == null)
        {
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
        }  
    }

    public void OnClickLoad(bool state)
    {
        loadPanel.SetActive(state);
        if (saveLoad.CheckRoom(1))
            room1.SetText("Room Available");
        else
            room1.SetText("No Room Saved");

        if (saveLoad.CheckRoom(2))
            room2.SetText("Room Available");
        else
            room2.SetText("No Room Saved");

        if (saveLoad.CheckRoom(3))
            room3.SetText("Room Available");
        else
            room3.SetText("No Room Saved");

        
    }

    public void OnClickNew()
    {
        gameData.roomObjects.Clear();
        SceneManager.LoadScene(1);
    }
    public void OnClickInfo(bool state)
    {
        infoPanel.SetActive(state);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

}
