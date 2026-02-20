using System.Collections.Generic;
using UnityEngine;

public class AudioSounds : MonoBehaviour
{
    public AudioSource speaker;

    public List<AudioClip> clips;

    public void PlayAudioClip(int clipNum)
    {
        speaker.PlayOneShot(clips[clipNum]);
    }
}
