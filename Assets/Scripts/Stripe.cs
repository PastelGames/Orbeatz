using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stripe : MonoBehaviour
{
    GameObject rightBound;

    // Start is called before the first frame update
    void Start()
    {
        rightBound = GameObject.Find("RightBound");

        Conductor c = GameObject.Find("Conductor").GetComponent<Conductor>();

        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.3f, c.secPerBeat / 2).setEaseInOutSine().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(.05f, 0, 0);
        if (transform.position.x > rightBound.transform.position.x) Destroy(this.gameObject);
    }
}
