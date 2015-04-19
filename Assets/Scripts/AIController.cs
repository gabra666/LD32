using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	MovementController movementController;
	CombatController combatController;
	Life life;

	public float closeEnoughDistance;
	// Use this for initialization
	void Start () {
		movementController = gameObject.GetComponent<MovementController>();
		combatController = gameObject.GetComponent<CombatController>();
		life = gameObject.GetComponent<Life>();
	}
	
	// Update is called once per frame
	void Update () {
		if(life.actualLife > life.maximunLife*0.75){
			//GoAgressive;
		}
		else if(life.actualLife< life.maximunLife*0.75 && life.actualLife > life.maximunLife*0.5){
			//BeCare;
		}
		else{
			//GoPassive;
		}

	}


	private void BeAgressive(){
		//MoveToPlayer And Attack when in range every 2 seconds 



	}

	private void BeCare(){
		//Defend for a while but attack when possible 
	}

	private void BePassive(){
		//Move Away from player jump forward when reaching a collider 
	}

	private V



}
