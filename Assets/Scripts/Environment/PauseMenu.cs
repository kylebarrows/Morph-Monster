using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    [SerializeField] public GameObject pauseMenuPrefab;
    [SerializeField] public GameObject healthPrefab;
    public GameObject healthUI;

    private void Awake()
    {
        //pauseMenuUI = GameObject.Find("PauseMenu");
        healthUI = GameObject.Find("UICanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape key was pressed");
            if (gameIsPaused)
                Resume();
            else 
                Pause();

        }

    }

    public void Resume()
    {
        if(pauseMenuUI == null)
        {
            pauseMenuUI = GameObject.Instantiate(pauseMenuPrefab);
        }

        if(healthUI == null)
        {
            healthUI = GameObject.Instantiate(healthPrefab);
        }

        pauseMenuUI.SetActive(false);
        if (healthUI != null)
            healthUI.SetActive(true);
        Time.timeScale = 1f; 
        gameIsPaused = false;

    }

    public void Pause()
    {
        if (pauseMenuUI == null)
        {
            pauseMenuUI = GameObject.Instantiate(pauseMenuPrefab);
        }

        if (healthUI == null)
        {
            healthUI = GameObject.Instantiate(healthPrefab);
        }

        pauseMenuUI.SetActive(true);
        if (healthUI != null)
            healthUI.SetActive(false);
        Time.timeScale = 0f; 
        gameIsPaused = true;


    }

    public void GoToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);

    }
}
