using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private int damage;
    [SerializeField] private GameObject explosion;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
        collision.gameObject.SendMessage("Damaged", damage);
    }

    // Update is called once per frame
    private void SetDamage(int Damage)
    {
        damage = Damage;
    }
}
