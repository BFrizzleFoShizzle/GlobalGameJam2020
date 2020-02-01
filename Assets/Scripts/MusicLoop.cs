using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    // BGM loop goes in the Audio Source. Tick the loop box!
    public AudioSource audioSource;
    public AudioClip bgmIntro;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(bgmIntro);
        audioSource.PlayScheduled(AudioSettings.dspTime + bgmIntro.length);
    }
}
