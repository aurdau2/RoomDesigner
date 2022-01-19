using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField]
    public List<RoomObject> roomObjects;
    static bool created;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else Destroy(this);
    }



}
