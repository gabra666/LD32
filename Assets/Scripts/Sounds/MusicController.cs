using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
    public AudioSource[] loops;

	// Use this for initialization
	void Start () {
        int index = (int)(Random.Range(0, 10) % 2);
        loops[index].Play();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
