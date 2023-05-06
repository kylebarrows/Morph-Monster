using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoad;
    public string sLevelToLoad;

    public bool useStringToLoad;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if(mc)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if(useStringToLoad)
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
        else
        {
            SceneManager.UnloadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(levelToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
