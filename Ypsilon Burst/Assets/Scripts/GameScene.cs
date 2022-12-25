using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private GameObject[] models;
    [SerializeField] private Weapon[] weapons;
    private int i;
    private GameObject player;
    private Transform placement;
    void Awake()
    {
        //player = GameObject.Find("Player");
        //Destroy(player);
        player = Instantiate(models[PlayerPrefs.GetInt("skin")], new Vector3(0f,0f,0f), Quaternion.identity);
        player.GetComponent<Weapons>().weapon = weapons[PlayerPrefs.GetInt("weapon")];
        player.name = "Player";
        player.GetComponent<Movement>().Start();
        player.GetComponent<Weapons>().Start();
    }
}
