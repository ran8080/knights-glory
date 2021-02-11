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
        while(true) { 
            for(int i = 0; i < audioClips.Length; i++) {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
