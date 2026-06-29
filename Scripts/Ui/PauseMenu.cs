using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public GameObject pauseMenuUi;
    public GameObject settingsUi_box;

    private bool settingsOpen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OpenPauseMenu()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeButton()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SettingsButton()
    {
        if (!settingsOpen)
        {
            settingsOpen = true;
            settingsUi_box.SetActive(true);
        }
        else
        {
            settingsOpen = false;
            settingsUi_box.SetActive(false);
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }
}
