using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip AudioClipMusic;
    public AudioClip AudioClipAttack1;
    public AudioClip AudioClipAttack2;
    public AudioClip AudioClipAttack3;
    public AudioClip AudioClipJump1;
    public AudioClip AudioClipJump2;

    public static SoundManager instance = null;
    private AudioSource _AudioSourceOneShot;
    private AudioSource _AudioSourceMusic;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        _AudioSourceMusic = GetComponents<AudioSource>()[0];
        _AudioSourceOneShot = GetComponents<AudioSource>()[1];

        _AudioSourceMusic.clip = AudioClipMusic;
        _AudioSourceMusic.Play();
    }


    public void PlaySound(AudioClip iClip)
    {
        _AudioSourceOneShot.Stop();
        _AudioSourceOneShot.PlayOneShot(iClip);
    }
}
