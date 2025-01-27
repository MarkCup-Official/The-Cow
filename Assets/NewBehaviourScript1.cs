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
            float targetTime = audioSource.clip.length * 0.5f; // �����ܳ��ȵ�50%
            audioSource.time = targetTime; // ���ò���ʱ��
        }
    }
}
