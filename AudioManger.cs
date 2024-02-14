using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效相关
/// </summary>

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance {  get; private set; }

    //音源组件
    private AudioSource audioS;

    void Start()
    {
        instance= this;
        audioS = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
