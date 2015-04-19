using UnityEngine;
using System.Collections;

public class FightFinishedController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FightFinished(GameObject loser)
    {
        
        loser.GetComponent<Animator>().SetTrigger("lose");
        GameObject.FindGameObjectWithTag((loser.tag == "Player") ? "Player2" : "Player").GetComponent<Animator>().SetTrigger("win");
        
    }
}
