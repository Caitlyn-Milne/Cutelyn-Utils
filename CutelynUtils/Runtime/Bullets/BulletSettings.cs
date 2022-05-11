using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletSettings
{
    public float BaseDmg;
    public float Speed;
    public Sprite Sprite;
    public float LifeTime;
    /// <summary>
    /// This is the layer in which the bullet is stored for hit detection IT IS NOT the layer the gameobject is on
    /// </summary>
    public int BulletLayer;
}
