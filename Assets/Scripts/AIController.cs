	using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	private MovementController movementController;
	private CombatController combatController;
	private Life life;

	private GameObject player;
	private Vector2 movement;

	private float randomCareBehave;
	private bool selectRandomCareBehave = true;

	public float closeEnoughDistance = 0.5f;
	public float maxDistance = 1.5f;
	public float timeBetweenAttacks = 2f;
	public float blockProbability = 0.25f;
	public float chargeAttackProbability = 0.4f;


	private float timeBetweenAttacksLeft = 0;
	private bool charging = false;


	private Animator playerAnimator; 
	private int playerAttackState;
	private int playerReleaseAttackState;




	
	// Use this for initialization
	void Start () {

		movementController = gameObject.GetComponent<MovementController>();
		combatController = gameObject.GetComponent<CombatController>();
		life = gameObject.GetComponent<Life>();
		player = GameObject.FindGameObjectWithTag("Player");
		movement = new Vector2(0,0);
		timeBetweenAttacksLeft = timeBetweenAttacks;

		playerAnimator = player.GetComponent<Animator>();
		playerAttackState = Animator.StringToHash("Base.Punch");
		playerReleaseAttackState = Animator.StringToHash("Base.ReleaseAttack");

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = new Vector2(0,0);
		if(life.actualLife >= life.maximunLife*0.75){
			//BeAgressive;
			BeAgressive();
		}
		else if(life.actualLife < life.maximunLife*0.75 && life.actualLife >= life.maximunLife*0f){
			//BeCare;
			blockProbability = 0.5f;
			chargeAttackProbability = 0.45f;
			BeCare();
		}
		else{
			//No se usa
			blockProbability = 0.75f;
			chargeAttackProbability = 0.15f;
			//BePassive;
			BePassive();
		}

	}


	private void BeAgressive(){
		//MoveToPlayer And Attack when in range every X seconds 
		if(DistanceToPlayer() > closeEnoughDistance){
			MoveTowardsPlayer();
		}else{
			if(Random.Range(0f,1f)<0.5){
				Attack();
			}else{
				TryToBlock();
			}
		}

	}

	private void BeCare(){
		Debug.Log("Care");
		//Defend for a while but attack when possible 
		if(selectRandomCareBehave){
			Debug.Log("Selecting behave");
			randomCareBehave = Random.Range(0f,1f);
			selectRandomCareBehave = false;
		}
		Debug.Log(randomCareBehave);
		if(randomCareBehave < 0.8){
			if(DistanceToPlayer() > closeEnoughDistance){
				MoveTowardsPlayer();
			}else{
				Attack();
			}
		}else{
			if(DistanceToPlayer() < maxDistance){
				MoveAwayFromPlayer();
			}else{
				TryToBlock();
			}
		}

	}

	private void BePassive(){
		Debug.Log("Passive");
		MoveAwayFromPlayer();
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

	private void MoveAwayFromPlayer(){
		Debug.Log("Away");
		if(movementController.isFacingRight()){
			movement.x = -1;
		}else{
			movement.x = 1;
		}
		movementController.setMovementVector(movement);
	}

	private void Attack(){

		timeBetweenAttacksLeft -= Time.deltaTime;
		if(timeBetweenAttacksLeft<0 && !charging){

			float randomValue = Random.Range(0.0f,1.0f);

			if(randomValue < chargeAttackProbability){
				charging = true;
				StartCoroutine(DoChargeAttack());
			}else{
				DoNormalAttack();
			}
		}
	}

	private void DoNormalAttack ()
	{
		combatController.chargeAttack();
		combatController.releaseAttack();
		timeBetweenAttacksLeft = timeBetweenAttacks;

		selectRandomCareBehave = true;
	}

	private IEnumerator DoChargeAttack(){
		Debug.Log("Chargiiiiing");
		combatController.chargeAttack();
		yield return new WaitForSeconds(1.3f);
		combatController.releaseAttack();
		timeBetweenAttacksLeft = timeBetweenAttacks;
		charging = false;
		selectRandomCareBehave = true;
	}

	private void TryToBlock(){
		if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Punch") || 
		   playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReleaseAttack")){
			float randomBlockValue = Random.Range(0f,1f);
			if(randomBlockValue < blockProbability){
				Block();
			}
		}
		selectRandomCareBehave = true;
	}

	private void Block(){
		if(!combatController.isBlocking()){
			combatController.block(true);
			Invoke("StopBlock",1f);
		}
	}

	private void StopBlock(){
		if(combatController.isBlocking()){
			combatController.block(false);
		}
	}





}
