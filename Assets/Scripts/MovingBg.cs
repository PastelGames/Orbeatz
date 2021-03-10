using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBg : MonoBehaviour
{
    public GameObject stripe;
    public float spawnRate;
    float timeElapsed = 0;

    public GameObject leftBound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnRate)
        {
            Instantiate(stripe, stripe.transform.position, Quaternion.identity);
            timeElapsed = 0;
        }
    }
}
