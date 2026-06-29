using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class mixerController : MonoBehaviour
{
    public static mixerController instance;

    public AudioMixer myMixer;


    public float volumeLevel = 0.7f;
    public float musicVolumeLevel = 0.7f;
    public float SFXVolumeLevel = 0.7f;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        myMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volumeLevel, 0.0001f)) * 20);
        myMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(musicVolumeLevel, 0.0001f)) * 20);
        myMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(SFXVolumeLevel, 0.0001f)) * 20);
        Debug.Log("Initial volume applied: " + volumeLevel);
    }

}
