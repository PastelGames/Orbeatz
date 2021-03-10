using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SongData", menuName = "SongData")]
public class SongData : ScriptableObject
{
    public AudioClip song;
    public string name;
    public int BPM;
    public List<float> track1beats = new List<float>();
    public List<float> track2beats = new List<float>();
    public List<float> track3beats = new List<float>();
}
