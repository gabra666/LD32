using UnityEngine;
using System.Collections;

public class VoicesController : MonoBehaviour {
    public AudioSource startingLine;

    public AudioSource[] audioHits;

	// Use this for initialization
	void Start () {
        if (tag == "Player")
            startingLine.Play();
        else
            startingLine.PlayDelayed(1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Hit()
    {
        audioHits[Random.Range(0, audioHits.Length - 1)].Play();
    }
}
