using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnTime : MonoBehaviour
{
    private float counter;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 5f)
            Destroy(this.gameObject);
    }
}
