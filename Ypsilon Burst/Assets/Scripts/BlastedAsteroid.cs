using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastedAsteroid : MonoBehaviour
{
    private Transform player;
    private int counter;
    private float x, z;
    private void Start()
    {
        if (GameObject.Find("Player")!= null) player = GameObject.Find("Player").GetComponent<Transform>();
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
    private void Delete()
    {
        Destroy(this.gameObject);
    }

    private void SetMaterial(Material mat) 
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void Damaged()
    {

    }
}
