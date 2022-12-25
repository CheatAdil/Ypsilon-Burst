using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int SpawnAmount;
    [SerializeField] private int EnemiesAmount;
    private float x, z, a, b;
    private float speed;
    [SerializeField] private float MiniRaduis, MaxRadius;
    private float miniRaduis, maxRadius;
    private GameObject player;
    [SerializeField] private GameObject[] asteroid;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject gunBot;
    [SerializeField] private GameObject BonusHolder;
    private void Start()
    {
        player = GameObject.Find("Player");

        x = player.transform.position.x;
        z = player.transform.position.z;
        miniRaduis = MiniRaduis;
        maxRadius = MaxRadius;
        SpawnAsteroid(SpawnAmount);
        SpawnEnemy(EnemiesAmount);
    }
    // Update is called once per frame
    private void Update()
    {if (player != null)
        {
            x = player.transform.position.x;
            z = player.transform.position.z;
            speed = player.GetComponent<Rigidbody>().velocity.magnitude;
            miniRaduis = MiniRaduis + speed;
            maxRadius = MaxRadius + speed;
        }
    else player = GameObject.Find("Player");
    }

    public void SpawnAsteroid(int spawnAmount)
    {
        int i = 0;
        while (i < spawnAmount)
        {
            a = Random.Range(-maxRadius + x, maxRadius + x);
            b = Random.Range(-maxRadius + z, maxRadius + z);
            
            if ((System.Math.Pow(miniRaduis, 2) <= System.Math.Pow((a - x), 2) + System.Math.Pow((b - z), 2)) & (System.Math.Pow(maxRadius, 2) >= System.Math.Pow((x - a), 2) + System.Math.Pow((z - b), 2)))
            {
                if (Random.Range(0, 1000) == 999) Instantiate(BonusHolder, new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
                else Instantiate(asteroid[Random.Range(0, asteroid.Length)], new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
                i++;
            }

        }
    }
    public void SpawnEnemy(int spawnAmount)
    {
        int i = 0;
        while (i < spawnAmount)
        {
            a = Random.Range(-maxRadius + x, maxRadius + x);
            b = Random.Range(-maxRadius + z, maxRadius + z);

            if ((System.Math.Pow(miniRaduis, 2) <= System.Math.Pow((a - x), 2) + System.Math.Pow((b - z), 2)) & (System.Math.Pow(maxRadius, 2) >= System.Math.Pow((x - a), 2) + System.Math.Pow((z - b), 2)))
            {
                if (Random.Range(0, 1000) == 999) Instantiate(BonusHolder, new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
                else Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
                i++;
            }

        }
    }
    public void SpawnGunBot()
    {
            switch (Random.Range(0, 3))
        {
            case 0:
                a = x;
                break;
            case 1:
                a = x - miniRaduis;
                break;
            case 2:
                a = x + miniRaduis;
                break;
        }
        switch (Random.Range(0, 3))
        {
            case 0:
                if (a != x) b = z;
                break;
            case 1:
                b = z - miniRaduis;
                break;
            case 2:
                b = z + miniRaduis;
                break;
        }

        Instantiate(gunBot, new Vector3(a, 0f, b), Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));

    }
    private void NoEnergy()
    {
        miniRaduis = 20f;
        maxRadius = 21f;
        SpawnEnemy(50);
    }
}
