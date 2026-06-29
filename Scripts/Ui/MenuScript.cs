using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject settingsUi_box;
    public GameObject crewUi_box;

    private bool isSettingsOpen;
    private bool isCrewOpen;

    private void Start()
    {
        if (settingsUi_box  == null)
        {
            Debug.LogWarning("No settings Ui reference set");
        }
        if (crewUi_box == null)
        {
            Debug.LogWarning("No crew Ui reference set");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("IntroSubmarine");
        Time.timeScale = 1;
    }

    public void SettingsButton()
    {
        if (!isSettingsOpen)
        {
            settingsUi_box.SetActive(true);
            isSettingsOpen = true;
        }
        else
        {
            isSettingsOpen = false;
            settingsUi_box.SetActive(false);
        }
    }

    public void CrewButton()
    {
        if (!isCrewOpen)
        {
            isCrewOpen = true;
            crewUi_box.SetActive(true);
        }
        else
        {
            isCrewOpen = false;
            crewUi_box.SetActive(false);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
