using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShow : MonoBehaviour
{
    private Transform shootingPoint;
    private GameObject Bullet;
    private float BulletSpeed;
    private float RateOfFire;
    [SerializeField]private float SCycle;

    public Weapon weapon;
    private AudioSource weaponSound;

    private AudioClip laserSound;
    public void Start()
    {
        shootingPoint = GameObject.Find("ShootingPoint").GetComponent<Transform>();
        weaponSound = shootingPoint.GetComponent<AudioSource>();
        Bullet = weapon.Bullet;
        BulletSpeed = weapon.BulletSpeed;
        RateOfFire = weapon.RateOfFire;
        laserSound = weapon.weaponSound;
        SCycle = 0;
    }
    private void Update()
    {
        if (weaponSound == null) Start();
        SCycle += Time.deltaTime;
        if (SCycle >= RateOfFire)
        {
            Shoot();
            SCycle = 0;
        }
    }
    private void Shoot()
    {
        PlayWeaponSound(laserSound);
        GameObject instbullet = Instantiate(Bullet, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;
        Rigidbody instRB = instbullet.GetComponent<Rigidbody>();
        instRB.AddRelativeForce(new Vector3(0, 0, 1f) * BulletSpeed * Time.deltaTime);
        instRB.gameObject.SendMessage("WeaponShow");
    }
    private void PlayWeaponSound(AudioClip sound)
    {
        if (!weaponSound.isPlaying)
        {
            weaponSound.volume = Random.Range(0.6f, 0.7f);
            weaponSound.pitch = Random.Range(0.7f, 0.8f);
            weaponSound.PlayOneShot(sound);
        }
    }
}
