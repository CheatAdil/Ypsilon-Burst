using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusContainer : MonoBehaviour
{
    private Transform player;
    private SpawnManager spawnManager;
    [SerializeField] private int hp;
    private int counter;
    private float x, z;
    [SerializeField] private GameObject[] parts;
    [SerializeField] private GameObject[] Bonuses; /// gold piece
    [SerializeField] private GameObject dust;
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioClip stoneNoise;
    private bool died;
    void Start()
    {
        hp = GameObject.Find("DifficultyManager").GetComponent<Difficulty>().bonusAsteroidHealth;
        player = GameObject.Find("Player").GetComponent<Transform>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        int a = Random.Range(-10, 11);
        int b = Random.Range(-10, 11);
        for (int i = 0; i<3; i++)   Instantiate(enemy, transform.position + new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
    }
    void FixedUpdate()
    {
        if (player != null)
        {
            counter++;

            if (counter >= 50)
            {
                counter = 0;
                if (Vector3.Distance(transform.position, player.position) >= 100f) Delete();
            }
        }
    }
    private void Delete()
    {
        Destroy(this.gameObject);
        spawnManager.SpawnAsteroid(1);
    }
    private void Killed()
    {
        GameObject.Find("Main Camera/StoneNoises").GetComponent<AudioSource>().PlayOneShot(stoneNoise);
        Instantiate(dust, transform.position, transform.rotation);
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] != null)
            {
                GameObject part = Instantiate(parts[i], transform.position, transform.rotation);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100000f * Time.deltaTime);
            }
        }

        int random = Random.Range(0, Bonuses.Length);
        if (!died)
        {
            GameObject gp = Instantiate(Bonuses[random], new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            gp.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * 1000 * Time.deltaTime);
            died = true;
        }
        Delete();
    }
    private void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0f) Killed();
    }
}
