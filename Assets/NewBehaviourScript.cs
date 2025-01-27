using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public float volumeThreshold = 0.1f;
    public Sprite defaultSprite;
    public Sprite loudSprite;
    public SpriteRenderer targetImage;

    private string microphoneDevice;
    private AudioClip microphoneClip;
    private const int sampleSize = 64;
    private float[] samples = new float[sampleSize];

    private bool UP = false;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0];
            Debug.Log($"使用麦克风设备：{microphoneDevice}");
            microphoneClip = Microphone.Start(microphoneDevice, true, 1, 44100);
        }
        else
        {
            Debug.LogError("未找到可用的麦克风设备！");
        }

        // 启动音量检测的Coroutine
        StartCoroutine(VolumeCheckRoutine());
    }

    public float SleepTime = 1;
    float timmer = 0;

    private void Update()
    {
        if (UP)
        {
            timmer = SleepTime+Random.Range(0,SleepTime/2f);
        }
        if (timmer > 0)
        {
            targetImage.sprite = loudSprite;
            timmer -= Time.deltaTime;
        }
        else
        {
            targetImage.sprite = defaultSprite;
        }
    }

    IEnumerator VolumeCheckRoutine()
    {
        while (true)
        {
            if (Microphone.IsRecording(microphoneDevice))
            {
                int position = Microphone.GetPosition(microphoneDevice);
                if (position > 0 && microphoneClip != null)
                {
                    microphoneClip.GetData(samples, position - samples.Length);
                    float volume = GetAverageVolume(samples);

                    UP = volume > volumeThreshold;
                }
            }

            yield return new WaitForSeconds(0.05f); // 50ms检测一次
        }
    }

    float GetAverageVolume(float[] audioSamples)
    {
        float sum = 0f;
        foreach (float sample in audioSamples)
        {
            sum += Mathf.Abs(sample);
        }
        return sum / audioSamples.Length;
    }
}
