using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Feedback : MonoBehaviour
{
    God god;

    public TMP_Text totalNotesHit;
    public TMP_Text averageAccuracy;
    public TMP_Text longestStreak;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        god = GameObject.Find("God").GetComponent<God>();

        totalNotesHit.text = god.totalNotesHit + "/" + (god.totalNotesHit + god.totalNotesMissed);
        averageAccuracy.text = god.averageHitAccuracy + "%";
        longestStreak.text = god.longestStreak.ToString();

        audioSource.volume = god.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
