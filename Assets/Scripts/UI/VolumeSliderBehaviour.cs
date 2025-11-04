using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderBehaviour : MonoBehaviour
{
    [SerializeField] private string paramName;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    void Start()
    {
        audioMixer.GetFloat(paramName, out float dbValue);
        // Logarithmic to linear conversion
        slider.value = Mathf.Pow(10.0f, dbValue / 20.0f);
    }

    public void OnValueChange(float value)
    {
        // Change the volume param to the new value
        ChangeVolume(paramName, value);
    }

    public void ChangeVolume(string name, float linearValue)
    {
        // Linear to logarithmic conversion
        float dbValue = 20.0f * Mathf.Log10(linearValue);
        audioMixer.SetFloat(name, dbValue);
    }
}
