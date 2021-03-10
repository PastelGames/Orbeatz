using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float percentage = 0;

    public float timeToMax = 1;

    public float aliveTime = 0;

    public float beat;

    public Conductor conductor;

    float secPerBeat;

    float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        secPerBeat = conductor.secPerBeat;


        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.15f, secPerBeat / 2).setEaseInOutSine().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime += Time.deltaTime;

        percentage = aliveTime / timeToMax;

    }
}
