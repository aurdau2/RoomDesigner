using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject furniturePanel;
    public GameObject inventoryPanel;
    public GameObject decorationsPanel;
    public GameObject colorPickerPanel;

    public GameObject pausePanel;
    public GameObject savePanel;
    public SaveLoad saveLoad;

    public StarterAssetsInputs player;


    // Start is called before the first frame update
    void Start()
    {
        player.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    
    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
                savePanel.SetActive(false);           
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                player.cursorInputForLook = true;
            }
            else
            {
                pausePanel.SetActive(true);
                Cursor.visible = true;
                player.cursorInputForLook = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (!pausePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inventoryPanel.SetActive(true);
                Cursor.visible = true;
                player.cursorInputForLook = false;
                Cursor.lockState = CursorLockMode.None;
                decorationsPanel.SetActive(false);
                furniturePanel.SetActive(false);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                inventoryPanel.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                decorationsPanel.SetActive(false);
                furniturePanel.SetActive(false);
                player.cursorInputForLook = true;
            }
        }

    }

    public void OnClickFurniture()
    {
        inventoryPanel.SetActive(false);
        furniturePanel.SetActive(true);
    }

    public void OnClickDecorations()
    {
        inventoryPanel.SetActive(false);
        decorationsPanel.SetActive(true);
    }

    public void OnClickSave()
    {
        if (savePanel.activeSelf)
            savePanel.SetActive(false);
        else
            savePanel.SetActive(true);
    }

    public void DisableAll()
    {
        decorationsPanel.SetActive(false);
        furniturePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        colorPickerPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.cursorInputForLook = true;
    }

    public void OnClickBack()
    {
        decorationsPanel.SetActive(false);
        furniturePanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void OnClickMain()
    {
    
        SceneManager.LoadScene(0);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickSaveSlot()
    {
        savePanel.SetActive(false);
    }
}
