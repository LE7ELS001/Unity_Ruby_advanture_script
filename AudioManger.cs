using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ч���
/// </summary>

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance {  get; private set; }

    //��Դ���
    private AudioSource audioS;

    void Start()
    {
        instance= this;
        audioS = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
