using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool isPaused;

    [SerializeField]
    private GameObject pausePanel;
    
    [SerializeField]
    private GameObject hudPanel;

    public void Pause()
    {
        pausePanel.SetActive(true);
        hudPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Unity msgs

    void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            isPaused = !isPaused;
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
