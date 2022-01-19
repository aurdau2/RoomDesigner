using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixLoad : MonoBehaviour
{
    public SaveLoad saveLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (saveLoad.gameData == null)
                saveLoad.gameData = GameObject.Find("GameData").GetComponent<GameData>();
        }
    }
}
