using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isMute = false;
    public AudioClip xiaochu;
    public AudioClip jieshu;
    public AudioClip pojilu1;
    public AudioClip buttonclick;
    public AudioClip putdown;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Playxiaochu()
    {
        PlayAudio(xiaochu);
    }
    public void Playjieshu()
    {
        PlayAudio(jieshu);
    }
    public void Playpojilu()
    {
        PlayAudio(pojilu1);
    }
    public void Playbuttonclick()
    {
        PlayAudio(buttonclick);
    }
    public void Playputdown()
    {
        PlayAudio(putdown);
    }

   private void PlayAudio(AudioClip clip)
    {
        if (isMute) return;
        audioSource.clip = clip;
        audioSource.Play();
    }


}
