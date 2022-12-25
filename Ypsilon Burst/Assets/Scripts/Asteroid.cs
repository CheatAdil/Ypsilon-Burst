using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Transform player;
    private SpawnManager spawnManager;
    [SerializeField] private int hp;
    private int counter;
    private float x, z;
    [SerializeField] private int asteroidIndex;
    [SerializeField] private GameObject[] parts1;
    [SerializeField] private GameObject[] parts2;
    [SerializeField] private GameObject dust;

    [SerializeField] private AudioClip stoneNoise;
    void Start()
    {
        if (GameObject.Find("DifficultyManager") != null)
        {
            switch (asteroidIndex)
            {
                case 0:
                    hp = GameObject.Find("DifficultyManager").GetComponent<Difficulty>().smallAsteroidHealth;
                    break;
                case 1:
                    hp = GameObject.Find("DifficultyManager").GetComponent<Difficulty>().bigAsteroidHealth;
                    break;
            }
        }

        if (GameObject.Find("Player") != null) player = GameObject.Find("Player").GetComponent<Transform>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
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
        Delete();
        Instantiate(dust, transform.position, transform.rotation);
        if (Random.Range(1, 3) == 1) //// max is exclusive for int !!!!
        {
            for (int i = 0; i < parts1.Length; i++)
            {
                GameObject part = Instantiate(parts1[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100000f * Time.deltaTime);
            }
        }
        else
        {
            for (int i = 0; i < parts2.Length; i++)
            {
                GameObject part = Instantiate(parts2[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100000f * Time.deltaTime);
            }
        }
        
    }
    private void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0f) Killed();
    }
}
