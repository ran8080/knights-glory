using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
        StartCoroutine(PlayAudioClipsSequentionally());
    }

    private IEnumerator PlayAudioClipsSequentionally() { 
        while (true) {
            ShuffleAudioClips();
            for (int i = 0; i < audioClips.Length; i++) {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                while (audioSource.isPlaying)
                { 
                    yield return null;
                }
            }
        }
    }

    private void ShuffleAudioClips()
    {
        for (int i = 0; i < audioClips.Length - 1; i++)
        {
            int rnd = Random.Range(i, audioClips.Length);
            var tempAudioClip = audioClips[rnd];
            audioClips[rnd] = audioClips[i];
            audioClips[i] = tempAudioClip;
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
