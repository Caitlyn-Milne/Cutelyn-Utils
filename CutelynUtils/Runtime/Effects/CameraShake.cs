using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraShake : MonoBehaviour
{
    public float DefaultDuration = 0.2f;

    public float DefaultMagnitude = 1;


    Vector3 originalPos;
    private void Start() {
        originalPos = transform.localPosition;
    }
    public void DefaultShake() {
        Shake(DefaultDuration, DefaultMagnitude);
    }
    public void StartShake(CameraShakeSettings _cameraShakeSettings) {
        Shake(_cameraShakeSettings.Duration, _cameraShakeSettings.Magnitude);
    }

    public async Task Shake(float _duration, float _magnitude) {
        var elapsedTime = 0f;

        var x = 0f;
        var z = 0f;
        
        while (elapsedTime < _duration) {
            x = Random.Range(-1f, 1f) * _magnitude * Time.deltaTime;
            z = Random.Range(-1f, 1f) * _magnitude * Time.deltaTime;

            transform.localPosition = originalPos +  new Vector3(x, 0, z);

            elapsedTime += Time.deltaTime;

            await Task.Yield();
        }
        transform.localPosition -= new Vector3(x, 0, z);
    }
}