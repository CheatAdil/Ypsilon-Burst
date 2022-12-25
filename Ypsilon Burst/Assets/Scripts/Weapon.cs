using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Weapon : ScriptableObject
{
    public int iD;
    public GameObject Bullet;
    public int Damage;
    public float BulletSpeed;
    public float RateOfFire;
    public int Energy;
    public AudioClip weaponSound;
}
