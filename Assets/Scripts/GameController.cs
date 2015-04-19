using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    GameObject player1;
    GameObject player2;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        player1.name = StorageManager.Instance.Player1CharacterName;
        player2.name = StorageManager.Instance.Player2CharacterName;

        if (StorageManager.Instance.NumberOfPlayers == 2)
            player2.AddComponent<InputController>();
        else
            player2.AddComponent<AIController>();
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
