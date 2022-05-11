using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    static BulletManager _instance;
    public static BulletManager GetInstance() {
        return _instance;
    }

    public int NumPreloadedBullets = 100;

    Stack<Bullet> bulletsStack = new Stack<Bullet>();

    [FormerlySerializedAs("activeBullets")]
    public List<Bullet> ActiveBullets;


    [SerializeField]
    Bullet BulletPrefab;


    #region start

    private void Awake() {
        _instance = this;
    }
    public void Start() {
        PreloadBullets();
    }

    void PreloadBullets() {
        for (int i = 0; i < NumPreloadedBullets; i++) {
            Bullet newBullet = Instantiate<Bullet>(BulletPrefab, this.transform.position, Quaternion.identity, this.transform);
            bulletsStack.Push(newBullet);
        }
    }
    #endregion

    public Bullet RequestBullet() {
        if (bulletsStack.Count == 0) return null;
        Bullet bullet = bulletsStack.Pop();
        bullet.gameObject.SetActive(true);
        ActiveBullets.Add(bullet);
        return bullet;
    }

    public void ReturnBullet(Bullet _bullet) {
        if (_bullet == null) return;
        ActiveBullets.Remove(_bullet);
        _bullet.gameObject.SetActive(false);
        bulletsStack.Push(_bullet);
    }
}
