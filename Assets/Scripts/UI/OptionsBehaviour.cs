using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsBehaviour : MonoBehaviour
{
    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";

    [SerializeField] private AudioMixer audioMixer;


    public void OnGeneralVolumeSliderValueChanged(float value)
    {
        audioMixer.SetFloat(MASTER_VOLUME, value);
    }

    public void OnMusicVolumeSliderValueChanged(float value)
    {
        audioMixer.SetFloat(MUSIC_VOLUME, value);
    }

    public void OnSFXVolumeSliderValueChanged(float value)
    {
        audioMixer.SetFloat(SFX_VOLUME, value);
    }

    void ChangeVolume(string name,  float linearValue)
    {
        float dbValue = 20.0f * Mathf.Log10(linearValue);
        audioMixer.SetFloat(name, dbValue);
    }
}
