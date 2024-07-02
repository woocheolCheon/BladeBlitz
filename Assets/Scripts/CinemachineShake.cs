using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Coroutine shakeCoroutine;
    

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float freqeuncy, float time)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(intensity, freqeuncy, time));
    }

    private IEnumerator ShakeCoroutine(float intensity, float freqeuncy, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float shakeTimer = time;
        float startingIntensity = intensity;

        while (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / time));

            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = freqeuncy;

            yield return null;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
