using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject fightFinishedGUI;
    public GameObject player1Portrait;
    public GameObject player2Portrait;
    public Sprite character1Portrait;
    public Sprite character2Portrait;
    public Sprite blackChest;
    public Sprite blackBack;

    private  GameObject player1;
    private  GameObject player2;

	// Use this for initialization
	void Start () {
        //
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        player1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(StorageManager.Instance.Player1CharacterName) as RuntimeAnimatorController;
        player2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(StorageManager.Instance.Player2CharacterName) as RuntimeAnimatorController;

        if (StorageManager.Instance.Player1CharacterName != "Rober")
        {
            GameObject.Find("WhiteHip1").SetActive(false);
            GameObject.Find("WhiteChest1").SetActive(false);
        }
        else
        {
            GameObject.Find("BlackHip1").SetActive(false);
            GameObject.Find("BlackChest1").SetActive(false);
        }

        if (StorageManager.Instance.Player2CharacterName != "Rober")
        {
            GameObject.Find("WhiteHip2").SetActive(false);
            GameObject.Find("WhiteChest2").SetActive(false);
        }
        else
        {
            GameObject.Find("BlackHip2").SetActive(false);
            GameObject.Find("BlackChest2").SetActive(false);
        }

        player1Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player1CharacterName == "Rober") ? character1Portrait : character2Portrait);
        player2Portrait.SendMessage("SetPortrait", (StorageManager.Instance.Player2CharacterName == "Rober") ? character1Portrait : character2Portrait);

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
