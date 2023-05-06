using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenDebugMessages : MonoBehaviour
{
    public string output = "";
    public string stack = "";
    Queue logQueue = new Queue();
    
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            output = logString;
            stack = stackTrace;
        }
    }
}
