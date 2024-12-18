using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] List<GameAudio> listAudio = new List<GameAudio>();

    public void PlayLoseAudio()
    {
        listAudio[3].PlayStateFXSound(transform, 1f, listAudio[3].audioClip);
    }
    public void PlayCountdownAudio()
    {
        listAudio[4].PlayStateFXSound(transform, 1f, listAudio[4].audioClip);
    }
    public void PlayThrowWeaponAudio()
    {
        listAudio[0].PlayStateFXSound(transform, 1f, listAudio[0].audioClip);
    }
    public void PlayDeadAudio()
    {
        listAudio[1].PlayStateFXSound(transform, 1f, listAudio[1].audioClip);
    }
    public void PlayVictoryAudio()
    {
        listAudio[2].PlayStateFXSound(transform, 1f, listAudio[2].audioClip);
    }
    public void TurnOffAudio()
    {
        AudioListener.volume = 0;
    }
    public void TurnOnAudio()
    {
        AudioListener.volume = 1;
    }
}
