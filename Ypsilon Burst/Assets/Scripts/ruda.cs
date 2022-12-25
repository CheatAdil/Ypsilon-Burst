using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ruda : MonoBehaviour
{
    private Transform player;
    private SpawnManager spawnManager;
    [SerializeField] private int hp;
    private int counter;
    private float x, z;
    [SerializeField] private GameObject[] parts;
    [SerializeField] private GameObject GP; /// gold piece
    [SerializeField] private GameObject dust;
    [SerializeField] private AudioClip stoneNoise;
    void Start()
    {
        hp = GameObject.Find("DifficultyManager").GetComponent<Difficulty>().goldAsteroidHealth;
        if (GameObject.Find("Player") != null) player = GameObject.Find("Player").GetComponent<Transform>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
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
        for (int i = 0; i < Random.Range(4, 6); i++) 
        {
            if (GP != null) 
            {
                GameObject gp = Instantiate(GP, new Vector3( transform.position.x, 0f , transform.position.z), transform.rotation);
                gp.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * 1000 * Time.deltaTime);
            }
        }
        Delete();
    }
    private void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0f) Killed();
    }
}
