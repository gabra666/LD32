using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
    public AudioSource[] loops;

	// Use this for initialization
	void Start () {
        loops[(int)Random.Range(0, 1)].Play();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
