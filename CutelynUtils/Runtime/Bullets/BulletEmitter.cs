using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletEmitter : MonoBehaviour, IEmitter<Bullet>
{
    public BulletEmitterSettings EmitterSettings;

    BulletManager mBulletManager;

    float timeLastEmission;

    public UnityEvent OnEmit;

    private void Start() {
        mBulletManager = BulletManager.GetInstance();
    }

    public virtual Bullet Emit() {
        if (enabled == false) return null;

        if (Time.time - timeLastEmission < EmitterSettings.EmitionRate) return null; //too soon

        Bullet bullet = BulletManager.GetInstance().RequestBullet();

        if (bullet == null) return null; //no bullets in bullet manager

        timeLastEmission = Time.time;

        bullet.Initalize(EmitterSettings.BulletSettings, transform.position,transform.forward.normalized);
        OnEmit?.Invoke();
        return bullet;
    }
    public void EmissionEvent() {
        Emit();
    }
}
