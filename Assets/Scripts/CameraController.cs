using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;


    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();    //Se obtiene la componente de la camara virtual
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shake(float duration = 0.1f, float amplitude = 1.5f, float frequency = 20.0f)   //Se le ponen valores por defecto por si no se le quieren asignar valores diferentes
    {
        StopAllCoroutines();
        StartCoroutine(ApplyNoiseRoutine(duration, amplitude, frequency));
    }

    IEnumerator ApplyNoiseRoutine(float duration, float amplitude, float frequency)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
