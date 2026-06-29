using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Valikoiden paneelit")]
    public GameObject settingsPanel;
    public GameObject crewPanel;
    
    [Header("Asetukset (Skene johon siirrytään)")]
    public string gameSceneName = "Submarine"; // Vaihda tähän oikea skenen nimi, esim. "SampleScene"

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Piilotetaan overlayt pelin alussa
        CloseAllPanels();
    }

    public void PlayGame()
    {
        // Ladataan itse peli
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        // Suljetaan muut ja avataan Settings
        CloseAllPanels();
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void OpenCrew()
    {
        // Suljetaan muut ja avataan Crew
        CloseAllPanels();
        if (crewPanel != null)
        {
            crewPanel.SetActive(true);
        }
    }

    public void CloseAllPanels()
    {
        // Piilotetaan molemmat overlayt
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        if (crewPanel != null)
        {
            crewPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        // Suljetaan peli (toimii vain oikeassa buildissa, ei editorissa)
        Debug.Log("Peli suljetaan!");
        Application.Quit();
    }
}