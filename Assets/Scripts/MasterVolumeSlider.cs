using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_masterMixer;

    private Slider m_slider;

    private const string VOLUME_KEY = "MasterVolumeLinear";
    private const float DEFAULT_VOLUME = 1f;

    void Awake()
    {
        m_slider = GetComponent<Slider>();

        float linearVolume = PlayerPrefs.GetFloat(VOLUME_KEY, DEFAULT_VOLUME);
        m_slider.value = linearVolume;

        // In case m_slider.value doesn't trigger OnValueChanged event
        HandleVolumeChanged(linearVolume);
    }

    public void HandleVolumeChanged(float volume)
    {
        float decibels = Mathf.Log10(volume) * 20f;

        m_masterMixer.SetFloat("MasterVolume", decibels);

        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
}
