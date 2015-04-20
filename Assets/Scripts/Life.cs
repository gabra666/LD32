using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Life : MonoBehaviour {
    public int maximunLife;
    public float actualLife;
    public AudioSource bloqueado_snd;
    public GameObject enemy;

    private CombatController combatController;

	// Use this for initialization
	void Start () {
        actualLife = maximunLife;
        combatController = gameObject.GetComponent<CombatController>();
        enemy = GameObject.FindGameObjectWithTag((tag == "Player") ? "Player2" : "Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ReceiveDamage(Attack attack)
    {
        if (!combatController.isBlocking())
        {
            combatController.damaged();
            actualLife -= attack.damage;
            gameObject.SendMessage("Hit");
            GameObject.Find("PunchMessagesController").SendMessage("Show", enemy);
        }
        else if (combatController.isBlocking() && attack.attackType == 1)
        {
            combatController.breakDefense();
            actualLife -= (float)(0.2 * attack.damage);
        }
        else
            bloqueado_snd.Play();
        checkLife();
    }

    private void checkLife()
    {
        if (actualLife <= 0)
            GameObject.FindGameObjectWithTag("GameController").SendMessage("FightFinished", gameObject);
    }
}
