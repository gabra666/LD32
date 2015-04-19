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
        loser.GetComponent<InputController>().enabled = false;

        GameObject winner = GameObject.FindGameObjectWithTag((loser.tag == "Player") ? "Player2" : "Player");
        winner.GetComponent<Animator>().SetTrigger("win");
        winner.GetComponent<InputController>().enabled = false;

        showFightFinsishedMessage(winner.name);
    }

    private void showFightFinsishedMessage(string winnerName)
    {
    }
}
