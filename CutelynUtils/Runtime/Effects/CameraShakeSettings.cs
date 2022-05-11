
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CaitUtils/Camera Shake Setting")]
[System.Serializable]
public class CameraShakeSettings : ScriptableObject {
    [FormerlySerializedAs("duration")]
    public float Duration;
    [FormerlySerializedAs("magnitude")]
    public float Magnitude;
}