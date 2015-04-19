using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject fightFinishedGUI;
    public GameObject player1Portrait;
    public GameObject player2Portrait;
    public Sprite character1Portrait;
    public Sprite character2Portrait;
    public Transform player1Spawn;
    public Transform player2Spawn;
    public GameObject Bobby;
    public GameObject Rober;

    private  GameObject player1;
    private  GameObject player2;

    void Awake()
    {
        player1 = GameObject.Instantiate((StorageManager.Instance.Player1CharacterName == "Rober") ? Rober : Bobby, player1Spawn.position, player1Spawn.rotation) as GameObject;
        player2 = GameObject.Instantiate((StorageManager.Instance.Player2CharacterName == "Rober") ? Rober : Bobby, player2Spawn.position, player2Spawn.rotation) as GameObject;

        player1.transform.position = player1Spawn.position;
        player2.transform.position = player2Spawn.position;

        player1.tag = "Player";
        player2.tag = "Player2";

        player1.SetActive(true);
        player2.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        
        player1Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player1CharacterName == "Rober") ? character1Portrait : character2Portrait);
        player2Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player2CharacterName == "Rober") ? character1Portrait : character2Portrait);

        player1.AddComponent<InputController>();

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
