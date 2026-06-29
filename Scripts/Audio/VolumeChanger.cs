using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private string mixerParameter;
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        if (mixerController.instance == null) return;

        float value = GetStoredValue();
        volumeSlider.value = value;
        ApplyVolume(value);
    }

    public void SetVolume(float sliderValue)
    {
        ApplyVolume(sliderValue);
        StoreValue(sliderValue);
    }

    void ApplyVolume(float value)
    {
        myAudioMixer.SetFloat(
            mixerParameter,
            Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20
        );
    }

    float GetStoredValue()
    {
        var mc = mixerController.instance;

        return mixerParameter switch
        {
            "MasterVolume" => mc.volumeLevel,
            "MusicVolume" => mc.musicVolumeLevel,
            "SFXVolume" => mc.SFXVolumeLevel,
            _ => 0.7f
        };
    }

    void StoreValue(float value)
    {
        var mc = mixerController.instance;

        switch (mixerParameter)
        {
            case "MasterVolume": mc.volumeLevel = value; break;
            case "MusicVolume": mc.musicVolumeLevel = value; break;
            case "SFXVolume": mc.SFXVolumeLevel = value; break;
        }
    }
}
