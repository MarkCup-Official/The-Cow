using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != null)
        {
            float targetTime = audioSource.clip.length * 0.5f; // 计算总长度的50%
            audioSource.time = targetTime; // 设置播放时间
        }
    }
}
