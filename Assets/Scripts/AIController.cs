using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	private MovementController movementController;
	private CombatController combatController;
	private Life life;

	private GameObject player;
	private Vector2 movement;

	public float closeEnoughDistance = 0.5f;
	// Use this for initialization
	void Start () {

		movementController = gameObject.GetComponent<MovementController>();
		combatController = gameObject.GetComponent<CombatController>();
		life = gameObject.GetComponent<Life>();
		player = GameObject.FindGameObjectWithTag("Player");
		movement = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = new Vector2(0,0);
		if(life.actualLife > life.maximunLife*0.75){
			//BeAgressive;
			BeAgressive();
		}
		else if(life.actualLife< life.maximunLife*0.75 && life.actualLife > life.maximunLife*0.5){
			//BeCare;
		}
		else{
			//BePassive;
		}

	}


	private void BeAgressive(){
		Debug.Log("Agressive");
		//MoveToPlayer And Attack when in range every 2 seconds 
		if(DistanceToPlayer() > closeEnoughDistance){
			MoveTowardsPlayer();
		}else{
			StartCoroutine(DoChargeAttack());
		}
	}

	private void BeCare(){
		//Defend for a while but attack when possible 
	}

	private void BePassive(){
		//Move Away from player jump forward when reaching a collider 
	}

	private float DistanceToPlayer(){
		return Vector3.Distance(gameObject.transform.position ,player.transform.position);
	}

	private void MoveTowardsPlayer(){
		Debug.Log("Move");
		if(movementController.isFacingRight()){
			movement.x = 1;
		}else{
			movement.x = -1;
		}
		movementController.setMovementVector(movement);
	}

	private void Attack(){

		combatController.chargeAttack();
		combatController.releaseAttack();
	}

	private IEnumerator DoChargeAttack(){
		Debug.Log("Charging");
		combatController.chargeAttack();
		yield return new WaitForSeconds(1f);
		combatController.releaseAttack();

	}



}
