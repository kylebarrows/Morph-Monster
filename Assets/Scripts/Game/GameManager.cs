using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private GameData gameData;

    private List<ISerializable> serializableObjects;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one GameManager");
        }
        instance = this;
    }

    private void Start()
    {
        this.serializableObjects = FindAllSerializableObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if(this.gameData == null)
        {
            Debug.Log("No data was found");
            NewGame();
        }

        foreach (ISerializable serializable in serializableObjects)
        {
            serializable.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (ISerializable serializable in serializableObjects)
        {
            serializable.SaveData(ref gameData);
        }
    }

    private List<ISerializable> FindAllSerializableObjects()
    {
        IEnumerable<ISerializable> serializables = FindObjectsOfType<MonoBehaviour>().OfType<ISerializable>();

        return new List<ISerializable>(serializables);
    }
}
