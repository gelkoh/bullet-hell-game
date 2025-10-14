using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_masterMixer;

    public void HandleVolumeChange(float volume)
    {
        float decibels = Mathf.Log10(volume) * 20f;
        m_masterMixer.SetFloat("MasterVolume", decibels);
    }
}
