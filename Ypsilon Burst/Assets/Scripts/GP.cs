using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP : MonoBehaviour
{
    private Transform player;
    private int counter;
    private float x, z;

    private ScoreManager SM;


    [SerializeField] private int ScoreReward;
    [SerializeField] private int EnergyReward;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        SM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    private void FixedUpdate()
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            GameObject.Find("Player").GetComponent<Weapons>().energy += EnergyReward; 
            other.gameObject.SendMessage("PlaySound");
            SM.GiveScore(ScoreReward);
            SM.EnergyChange();
            Delete();
        }
    }
    private void Delete()
    {
        Destroy(this.gameObject);
    }



}
