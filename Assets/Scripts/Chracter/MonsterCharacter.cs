using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacter : MonoBehaviour, ISerializable
{
    public MonsterInput MInput;

    public MonsterController Controller;

    public FormSwapHandler SwapHandler;

    [SerializeField] public FormInfo BaseForm;

    public FormInfo ActiveForm;

    public int ActiveFormId;

    public FormAbilities Abilities;

    public bool isTransformed;

    private static MonsterCharacter _instance;

    public static MonsterCharacter instance
    {
        get
        {
            MonsterCharacter silentInstance = SilentInstance;
            if (!silentInstance)
            {
                Debug.LogError("Couldn't find a Hero, make sure one exists!");
            }
            return silentInstance;
        }
    }

    public static MonsterCharacter SilentInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType<MonsterCharacter>();
                if ((bool)_instance && Application.isPlaying)
                {
                    UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            return;
        }

        BaseForm = new MonsterFormInfo();
        ActiveForm = BaseForm;
        ActiveFormId = 0;
        Controller = GetComponent<MonsterController>();
        MInput = GetComponent<MonsterInput>();

        SetReferenceToCharacter(base.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject startGO = GameObject.FindWithTag("StartPosition");
        if(startGO)
        {
            transform.position = startGO.transform.position;
        }
    }

    public void LoadData(GameData gameData)
    {
        ChangeForm(gameData.playerFormID, true);
        Health health = GetComponent<Health>();
        health.Respawn();
    }

    public void SaveData(ref GameData data)
    {

    }

    public void ChangeForm(int formID, bool load)
    {
        ActiveFormId = formID;
        SwapHandler.ChangeForm(formID, load);
    }

    public void SetReferenceToCharacter(GameObject obj)
    {
        obj.BroadcastMessage("SetMonsterReference", this, SendMessageOptions.RequireReceiver);
    }
}
