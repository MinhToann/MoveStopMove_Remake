using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [field: SerializeField] public AudioSource audioSources { get; private set; }
    [field: SerializeField] public AudioClip audioClip { get; private set; }

    public void PlayStateFXSound(Transform tf, float volume, AudioClip clip)
    {
        AudioSource audio = Instantiate(audioSources, tf.position, Quaternion.identity);
        audio.clip = clip;
        audio.volume = volume;
        audio.Play();
        float clipLength = audio.clip.length;
        Destroy(audio.gameObject, clipLength);
        if(LevelManager.Ins.isChangeGameState)
        {
            StopFXSound(audio);
        }
    }
    public void StopFXSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    public void SetMinVolumn()
    {
        audioSources.volume = 0;
    }
    public void SetMaxVolumn()
    {
        audioSources.volume = 1;
    }
}
