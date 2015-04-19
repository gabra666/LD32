using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject fightFinishedGUI;
    public GameObject player1Portrait;
    public GameObject player2Portrait;
    public Sprite character1Portrait;
    public Sprite character2Portrait;

    private  GameObject player1;
    private  GameObject player2;

	// Use this for initialization
	void Start () {
        //StorageManager.Instance.Player1CharacterName
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        player1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Rober") as RuntimeAnimatorController;
        player2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Rober") as RuntimeAnimatorController;

        player1.name = StorageManager.Instance.Player1CharacterName;
        player2.name = StorageManager.Instance.Player2CharacterName;

        player1Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player2CharacterName == "Character1") ? character1Portrait : character2Portrait);
        player2Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player2CharacterName == "Character1") ? character1Portrait : character2Portrait);

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
        if (loser == player2 && StorageManager.Instance.NumberOfPlayers == 2)
            loser.GetComponent<InputController>().enabled = false;
        else if (loser == player2)
            loser.GetComponent<AIController>().enabled = false;

        GameObject winner = GameObject.FindGameObjectWithTag((loser == player1) ? "Player2" : "Player");
        winner.GetComponent<Animator>().SetTrigger("win");
        winner.GetComponent<InputController>().enabled = false;

        fightFinishedGUI.SendMessage("FightFinished", winner.name);
    }
}
