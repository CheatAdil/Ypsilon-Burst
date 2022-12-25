using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBot : MonoBehaviour
{
    private Transform player;
    private Transform self;
    public Transform ShootingSpot;
    [SerializeField] private GameObject enemy;
    private Vector3 target;
    private int counter;
    private float distanceToPlayer;
    private bool toHeal;

    [SerializeField] private float speed;
    private float actSpeed;
    [SerializeField] private int HP;
    [SerializeField] private float Damage;
    [SerializeField] private float RateOfFire;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private GameObject bullet;

    [SerializeField] private GameObject[] parts1;
    [SerializeField] private GameObject[] parts2;
    [SerializeField] private GameObject explosion;
    private AudioSource Sound;
    [SerializeField] private AudioClip laserSound;

    private bool attacking;
    private float SCycle;
    private void Start()
    {
        Sound = GetComponent<AudioSource>();
        actSpeed = speed;
        player = GameObject.Find("Player").GetComponent<Transform>();
        self = GetComponent<Transform>();
    }
    private void Update()
    {
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(self.position, player.position);

            self.position = Vector3.MoveTowards(self.position, player.position, actSpeed * Time.deltaTime);
            
            if (distanceToPlayer < 2f) actSpeed = 0;
            else
            {
                if (distanceToPlayer <= 5f) actSpeed = speed / 3f;
                else actSpeed = speed;
            }
        }
        else player = GameObject.Find("Player").GetComponent<Transform>();
        if (enemy != null)
        {
            target = enemy.transform.position;
            self.LookAt(target);
            RaycastHit hit;
            if (Physics.Raycast(ShootingSpot.position, self.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Enemy")
                {
                    attacking = true;
                }
                else attacking = false;
            }
            if (attacking)
            {
                SCycle += Time.deltaTime;
                if (SCycle >= RateOfFire)
                {
                    Shoot();
                    SCycle = 0;
                }
            }
        }
        if (toHeal)
        {
            float timer = 0;
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                HP += 10;
                timer = 0;
                if (HP >= 1000)
                {
                    HP = 1000;
                    toHeal = false;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && enemy == null)
            enemy = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (enemy = other.gameObject) enemy = null;
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            counter++;

            if (counter >= 50)
            {
                counter = 0;
                if (Vector3.Distance(self.position, player.position) >= 100f) Destroy(this.gameObject);
            }
        }
    }

    private void Shoot()
    {
        PlaySound(laserSound);
        GameObject instbullet = Instantiate(bullet, ShootingSpot.transform.position, ShootingSpot.transform.rotation) as GameObject;
        Rigidbody instRB = instbullet.GetComponent<Rigidbody>();
        instRB.AddRelativeForce(new Vector3(0, 0, 1f + actSpeed) * BulletSpeed * Time.deltaTime);
        instRB.gameObject.SendMessage("SetDamage", Damage);
    }
    private void Damaged(int damage)
    {
        HP -= damage;
        if (HP <= 0f) Killed();
        Invoke("Heal", 30f);
    }

    private void Killed()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (Random.Range(1, 3) == 1) //// max is exclusive for int !!!!
        {
            for (int i = 0; i < parts1.Length; i++)
            {
                GameObject part = Instantiate(parts1[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100f * Time.deltaTime);
            }
        }
        else
        {
            for (int i = 0; i < parts2.Length; i++)
            {
                GameObject part = Instantiate(parts2[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100f * Time.deltaTime);
            }
        }
        Destroy(this.gameObject);
    }
    private void PlaySound(AudioClip sound)
    {
        if (!Sound.isPlaying)
        {
            Sound.volume = Random.Range(0.6f, 0.7f);
            Sound.pitch = Random.Range(0.7f, 0.8f);
            Sound.PlayOneShot(sound);
        }
    }
    private void Heal()
    {
        toHeal = true;
    }
}
