using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Conductor : MonoBehaviour
{
    public float songBpm;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public AudioSource musicSource;

    private bool endSongSequenceBegun;

    int countDownTime = 3;

    public TMP_Text countdownTMP;

    public ProgressBar songProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Get the song data from God.
        SongData sd = GameObject.Find("God").GetComponent<God>().currentSongData;

        musicSource.clip = sd.song;

        songBpm = sd.BPM;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource.isPlaying)
        {
            //Determine how many seconds have passed since the song started.
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            //Determind how many beats since the song started.
            songPositionInBeats = songPosition / secPerBeat;
        }
        else if (songPosition >= musicSource.clip.length - 1)
        {
            //if the end of the song has been reached.
            if (!endSongSequenceBegun)
            {
                Overhead overhead = GameObject.Find("Overhead").GetComponent<Overhead>();

                God god = GameObject.Find("God").GetComponent<God>();

                god.totalNotesHit = overhead.totalNotesHit;
                god.totalNotesMissed = overhead.totalNotesMissed;
                god.longestStreak = overhead.longestStreak;
                god.averageHitAccuracy = overhead.totalHitAccuracy / (float)overhead.totalNotesHit;

                SceneManager.LoadScene(3);
            }
        }

        //Show how far along in the song you are.
        songProgressBar.BarValue = (songPosition / musicSource.clip.length) * 100;
    }
    
    IEnumerator Countdown()
    {
        while (countDownTime > 0)
        {
            countdownTMP.text = countDownTime.ToString();

            yield return new WaitForSecondsRealtime(1f);

            countDownTime--;
        }


        countdownTMP.text = "GO!";

        yield return new WaitForSeconds(1f);

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();

        countdownTMP.gameObject.SetActive(false);
    }
    
}
