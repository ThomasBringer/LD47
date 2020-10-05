using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource source1;
    //[SerializeField] AudioSource source2;

    public void Play1() { source1.pitch = Random.Range(.75f, 1.25f); source1.Play(); }
    public void Play2() { source1.pitch = Random.Range(.5f, .75f); source1.Play(); }

    public static SoundManager sm;

    void Awake() { sm = this; }
}