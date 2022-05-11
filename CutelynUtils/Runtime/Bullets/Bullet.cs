using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour {
    public BulletSettings BulletSettings;

    public Vector3 Velocity;
    float remainingLifeTime;

    SpriteRenderer spriteRenderer;
    

    public static Dictionary<int, List<Bullet>> LayerVsActiveBulletsDictionary = new Dictionary<int, List<Bullet>>();

    [FormerlySerializedAs("computeShader")]
    public ComputeShader ComputeShader;

    // public bool IsRunning;

    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public virtual void Initalize(BulletSettings _bulletSettings, Vector3 _startingLocation,Vector3 _direction) {
        this.BulletSettings = _bulletSettings;

        transform.position = _startingLocation;
        remainingLifeTime = BulletSettings.LifeTime;
        Velocity = _direction * BulletSettings.Speed;
        spriteRenderer.sprite = BulletSettings.Sprite;

        if (LayerVsActiveBulletsDictionary.ContainsKey(BulletSettings.BulletLayer)) {
            LayerVsActiveBulletsDictionary[BulletSettings.BulletLayer].Add(this);
        }
        else {
            LayerVsActiveBulletsDictionary[BulletSettings.BulletLayer] = new List<Bullet>();
        }
    }

    protected virtual void Update() {
        //if (!IsRunning) return;
        remainingLifeTime -= Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;

        if (remainingLifeTime < 0) {
            ReturnToManager();
        }
    }

    public void ReturnToManager() {
        // IsRunning = false;
        if (LayerVsActiveBulletsDictionary.ContainsKey(BulletSettings.BulletLayer)) {
            LayerVsActiveBulletsDictionary[BulletSettings.BulletLayer].Remove(this);
        }
        BulletManager.GetInstance().ReturnBullet(this);
    }
    
    private void OnDestroy() {
        ReturnToManager();
    }


    private void OnDisable() {
        if (LayerVsActiveBulletsDictionary.ContainsKey(BulletSettings.BulletLayer)) {
            LayerVsActiveBulletsDictionary[BulletSettings.BulletLayer].Remove(this);
        }
    }
}
