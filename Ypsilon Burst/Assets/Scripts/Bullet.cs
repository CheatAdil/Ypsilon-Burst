using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private float LifeTime = 10f;
    private int damage;
    private float LifeCounter;
    private int explosionIndex;
    private int drill = 1;
    private void Update()
    {
        LifeCounter += Time.deltaTime;
        if (LifeCounter >= LifeTime) Despawn();
    }

    private void Despawn() 
    {
        LifeCounter = 0;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explosionIndex > 0)
        {
            GameObject explosionSphere = Instantiate(explosion, transform.position, transform.rotation);
            explosionSphere.SendMessage("SetDamage", explosionIndex * 5);
        }
        Destroy(this.gameObject);
        if(collision.collider.tag == "Asteroid") collision.gameObject.SendMessage("Damaged", damage * drill);
        collision.gameObject.SendMessage("Damaged", damage);
    }
    private void SetDamage(int Damage)
    {
        damage = Damage;
    }
    private void ExplosiveAmmo(int index)
    {
        explosionIndex++;
    }
    private void Drill(int index)
    {
        drill++;
    }
    private void WeaponShow()
    {
        Invoke("Kill", 1f);
    }
    private void Kill()
    {
        Destroy(this.gameObject);
    }

}
