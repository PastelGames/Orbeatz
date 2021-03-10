using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bop : MonoBehaviour
{
    public float expandRatio = 1.2f;
    public float BPM = 140f;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, gameObject.transform.localScale * expandRatio, (60f / BPM)).setEaseInOutSine().setLoopPingPong();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
