using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int playerFormID;
    
    public GameData()
    {
        playerPosition = Vector3.zero;
        playerFormID = 0; // Default monster form
    }
}
