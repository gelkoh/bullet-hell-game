using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource m_gunSource;

    [SerializeField]
    private AudioClip m_laserShot;

    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayLaserShot()
    {
        m_gunSource.PlayOneShot(m_laserShot);
    }
}
