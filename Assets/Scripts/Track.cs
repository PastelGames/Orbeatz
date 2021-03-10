using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using TMPro;

public class Track : MonoBehaviour
{
    public float radius = 1;

    public List<GameObject> notes;

    public float[] noteTimes = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

    Conductor conductor;

    public GameObject note;

    int nextIndex = 0;

    float beatsShownInAdvance = 6f;

    public int correct = 0;

    public KeyCode input = KeyCode.LeftArrow;

    public int misses = 0;

    public bool miss = false;

    public float hitWindow = .25f;

    public GameObject targetIndicator;

    Vector3 targetIndicatorOriginalScale;

    public List<GameObject> notesHit; //The notes hit in a single frame.

    public GameObject hitBurst;

    public GameObject missBurst;

    LineRenderer lr;

    public Shaker shaker;
    public ShakePreset shakePreset;

    public GameObject overhead;

    public ProgressBar pb;

    God god;

    // Start is called before the first frame update
    void Start()
    {
        //Find the conductor.
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();

        notesHit = new List<GameObject>();

        notes = new List<GameObject>();

        god = GameObject.Find("God").GetComponent<God>();

        targetIndicator.transform.position = Vector3.up * radius + Vector3.forward * 5;

        lr = GetComponent<LineRenderer>();

        //Draw a circle indicating the track.
        DrawCircle();

        targetIndicatorOriginalScale = targetIndicator.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextIndex < noteTimes.Length && noteTimes[nextIndex] < conductor.songPositionInBeats + beatsShownInAdvance)
        {
            GameObject newNote = Instantiate(note);

            newNote.GetComponent<Note>().timeToMax = beatsShownInAdvance * conductor.secPerBeat;

            newNote.GetComponent<Note>().beat = noteTimes[nextIndex];

            notes.Add(newNote);

            nextIndex++;
        }

        notesHit.Clear();

        //Move all of the notes along the track.
        foreach (GameObject note in notes)
        {
            if (note)
            {
                MoveAlongTrack(note.GetComponent<Note>());
            }
        }

        //Hit the track's designated key.
        if (Input.GetKeyDown(input))
        {
            LeanTween.scale(targetIndicator, targetIndicator.transform.localScale * 1.2f, .05f).setOnComplete(() =>
            {
                LeanTween.scale(targetIndicator, targetIndicatorOriginalScale, 0.1f);
            });

            foreach (GameObject note in notes)
            {
                if (note && conductor.songPositionInBeats > note.GetComponent<Note>().beat - hitWindow / 2 && conductor.songPositionInBeats < note.GetComponent<Note>().beat + hitWindow / 2)
                {
                    notesHit.Add(note);
                }
            }

            if (notesHit.Count == 0)
            {
                Instantiate(missBurst, new Vector3(0, radius, 2), Quaternion.identity);
                missBurst.GetComponent<AudioSource>().volume = god.volume;
                Miss();
                shaker.Shake(shakePreset);
            }
            else
            {
                GameObject oldestNote = notesHit[0];
                foreach (GameObject note in notesHit)
                {
                    if (note.GetComponent<Note>().beat < oldestNote.GetComponent<Note>().beat)
                    {
                        oldestNote = note;
                    }
                }
                correct++;

                float hitAccuracy = Mathf.Floor(100 - ((Mathf.Abs(conductor.songPositionInBeats - oldestNote.GetComponent<Note>().beat) / (hitWindow / 2)) * 100));

                overhead.GetComponent<Overhead>().totalHitAccuracy += hitAccuracy;

                //Update the accuracy bar.
                pb.BarValue = hitAccuracy;
                //Create that burst effect before destroying.
                Instantiate(hitBurst, oldestNote.transform.position, Quaternion.identity);
                Destroy(oldestNote);
                overhead.GetComponent<Overhead>().streak++;
            }
        }
    }

    void DrawCircle()
    {
        float theta_scale = 0.01f; //Circle resolution.
        lr.positionCount = (int)((1 / theta_scale) + 2);

        int i = 0;
        for (float theta = 0; theta < 2 * Mathf.PI; theta += 2 * Mathf.PI * theta_scale)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 10);
            lr.SetPosition(i, pos);
            i += 1;
        }
        lr.SetPosition(lr.positionCount - 2, new Vector3(radius, 0, 10));
        lr.SetPosition(lr.positionCount - 1, new Vector3(radius * Mathf.Cos(2 * Mathf.PI * theta_scale), radius * Mathf.Sin(2 * Mathf.PI * theta_scale), 10));
    }

    void MoveAlongTrack(Note note)
    {
        //If the player doesn't hit the note.
        if (note.isActiveAndEnabled && note.percentage >= 1.2 )
        {
            Instantiate(missBurst, note.transform.position, Quaternion.identity);
            missBurst.GetComponent<AudioSource>().volume = god.volume;
            Destroy(note.gameObject);
            shaker.Shake(shakePreset);
            misses++;
            Miss();
        }
        note.transform.position = new Vector3(Mathf.Cos(note.percentage * 1.5f * -Mathf.PI), Mathf.Sin(note.percentage * 1.5f * -Mathf.PI), 0) * radius + new Vector3(0, 0, 3);
    }

    void Miss()
    {
        overhead.GetComponent<Overhead>().streak = 0;
        pb.BarValue = 0;
    }

}