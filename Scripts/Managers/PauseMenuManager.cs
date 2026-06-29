using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Paneelit")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Asetukset")]
    public string mainMenuSceneName = "MenuScene";

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    void Update()
    {
        
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // Jos settings on auki, Esc sulkee sen
                if (settingsPanel != null && settingsPanel.activeSelf)
                {
                    CloseSettings();
                }
                // Muuten käännetään pausen tilaa (päälle/pois)
                else
                {
                    if (isPaused)
                    {
                        ResumeGame();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
            }
        }
    }

    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void OpenSettings()
    {
        Debug.Log("Settings-nappia painettu! Odotetaan prefabia...");
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
