using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Life : MonoBehaviour {
    public int maximunLife;
    public float actualLife;

    private CombatController combatController;

	// Use this for initialization
	void Start () {
        actualLife = maximunLife;
        combatController = gameObject.GetComponent<CombatController>();
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
        }
        else if (combatController.isBlocking() && attack.attackType == 1)
        {
            combatController.breakDefense();
            actualLife -= (float)(0.2 * attack.damage);
        }
        checkLife();
    }

    private void checkLife()
    {
        if (actualLife <= 0)
            GameObject.FindGameObjectWithTag("GameController").SendMessage("FightFinished", gameObject);
    }
}
