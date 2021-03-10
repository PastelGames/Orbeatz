using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public SongData jellyfish;
    public SongData sk8board;
    public SongData algorithm;

    God god;

    public TMP_Text volume;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        god = GameObject.Find("God").GetComponent<God>();
        audioSource.volume = god.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            god.currentSongData = jellyfish;
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            god.currentSongData = sk8board;
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            god.currentSongData = algorithm;
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            god.volume = (god.volume + .1f) % 1.01f;
            audioSource.volume = (audioSource.volume + .1f) % 1.01f;
        }

        volume.text = "(   ) Volume " + Mathf.FloorToInt(god.volume * 100) + "%";
    }
}
