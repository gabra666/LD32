using UnityEngine;
using System.Collections;

public class FightFinishedController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (StorageManager.Instance.NumberOfPlayers == 2)
            GameObject.FindGameObjectWithTag("Player2").AddComponent<InputController>();
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
