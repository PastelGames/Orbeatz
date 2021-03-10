using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Overhead : MonoBehaviour
{
    public int streak;
    public TMP_Text streakText;

    public GameObject innerTrack;
    public GameObject middleTrack;
    public GameObject outerTrack;

    public int totalNotesHit;
    public int totalNotesMissed;

    public float totalHitAccuracy;

    public int longestStreak;

    // Start is called before the first frame update
    void Start()
    {
        streak = 0;

        //Set the tracks' beat patterns.
        SongData sd = GameObject.Find("God").GetComponent<God>().currentSongData;

        innerTrack.GetComponent<Track>().noteTimes = sd.track1beats.ToArray();
        middleTrack.GetComponent<Track>().noteTimes = sd.track2beats.ToArray();
        outerTrack.GetComponent<Track>().noteTimes = sd.track3beats.ToArray();

        GameObject.Find("God").GetComponent<God>().SetVolumeInScene();

        totalHitAccuracy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        streakText.text = "Streak: " + streak.ToString();

        totalNotesHit = innerTrack.GetComponent<Track>().correct + middleTrack.GetComponent<Track>().correct + outerTrack.GetComponent<Track>().correct;
        totalNotesMissed = innerTrack.GetComponent<Track>().misses + middleTrack.GetComponent<Track>().misses + outerTrack.GetComponent<Track>().misses;

        if (streak > longestStreak)
        {
            longestStreak = streak;
        }
    }
}
