using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class God : MonoBehaviour
{
    public SongData currentSongData;
    public float volume = .51f;

    public int totalNotesHit;
    public int totalNotesMissed;
    public int longestStreak;
    public float averageHitAccuracy;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SetVolumeInScene()
    {
        GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>().volume = volume;
    }
}
