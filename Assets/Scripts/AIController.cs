﻿using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
   private MovementController movementController;
   private CombatController combatController;
   private Life life;

   private GameObject player;
   private Vector2 movement;

   private float randomCareBehave;
   private bool selectRandomCareBehave = true;

   public float closeEnoughDistance = 10f;
   public float maxDistance = 3f;
   public float timeBetweenAttacks = 2f;
   public float blockProbability = 25f;
   public float chargeAttackProbability = 15f;


   private float timeBetweenAttacksLeft = 0;
   private bool charging = false;


   private Animator playerAnimator;
   private int playerAttackState;
   private int playerReleaseAttackState;

   void Start () {
      movementController = gameObject.GetComponent<MovementController>();
      combatController = gameObject.GetComponent<CombatController>();
      life = gameObject.GetComponent<Life>();
      player = GameObject.FindGameObjectWithTag("Player");
      movement = new Vector2(0, 0);
      timeBetweenAttacksLeft = timeBetweenAttacks;

      playerAnimator = player.GetComponent<Animator>();
   }

   void Update () {
      movement = new Vector2(0, 0);
      if (life.currentLife >= life.maximunLife * 0.75) {
         //BeAgressive;
         BeAgressive();
      } else if (life.currentLife < life.maximunLife * 0.75 && life.currentLife >= life.maximunLife * 0f) {
         //BeCare;
         blockProbability = 50f;
         chargeAttackProbability = 45f;
         BeCare();
      } else {
         //No se usa
         blockProbability = 50f;
         chargeAttackProbability = 15f;
         //BePassive;
         BePassive();
      }
      movementController.setMovementVector(movement);
   }


   private void BeAgressive () {
      //MoveToPlayer And Attack when in range every X seconds 
      if (DistanceToPlayer() > closeEnoughDistance) {
         MoveTowardsPlayer();
      } else {
         if (Random.Range(0f, 1f) < 0.7) {
            Attack();
         } else {
            TryToBlock();
         }
      }

   }

   private void BeCare () {
      Debug.Log("Care");
      //Defend for a while but attack when possible 
      if (selectRandomCareBehave) {
         Debug.Log("Selecting behave");
         randomCareBehave = Random.Range(0f, 1f);
         selectRandomCareBehave = false;
      }
      Debug.Log(randomCareBehave);
      if (randomCareBehave < 0.8) {
         if (DistanceToPlayer() > closeEnoughDistance) {
            MoveTowardsPlayer();
         } else {
            Attack();
         }
      } else {
         if (DistanceToPlayer() < maxDistance) {
            MoveAwayFromPlayer();
         } else {
            TryToBlock();
         }
      }

   }

   private void BePassive () {
      Debug.Log("Passive");
      MoveAwayFromPlayer();
      //Move Away from player jump forward when reaching a collider 
   }

   private float DistanceToPlayer () {
      return Vector3.Distance(gameObject.transform.position, player.transform.position);
   }

   private void MoveTowardsPlayer () {
      if (movementController.isFacingRight()) {
         movement.x = 1;
      } else {
         movement.x = -1;
      }
   }

   private void MoveAwayFromPlayer () {
      Debug.Log("Away");
      if (movementController.isFacingRight()) {
         movement.x = -1;
      } else {
         movement.x = 1;
      }
   }

   private void Attack () {

      timeBetweenAttacksLeft -= Time.deltaTime;
      if (timeBetweenAttacksLeft < 0 && !charging) {

         float randomValue = Random.Range(0.0f, 100.0f);

         if (randomValue < chargeAttackProbability) {
            charging = true;
            StartCoroutine(DoChargeAttack());
            DoNormalAttack();
         } else {
            DoNormalAttack();
         }
      }
   }

   private void DoNormalAttack () {
      combatController.chargeAttack();
      combatController.releaseAttack();
      timeBetweenAttacksLeft = timeBetweenAttacks;

      selectRandomCareBehave = true;
   }

   private IEnumerator DoChargeAttack () {
      Debug.Log("Chargiiiiing");
      combatController.chargeAttack();
      yield return new WaitForSeconds(1.3f);
      combatController.releaseAttack();
      timeBetweenAttacksLeft = timeBetweenAttacks;
      charging = false;
      selectRandomCareBehave = true;
   }

   private void TryToBlock () {
      if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Punch") ||
         playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReleaseAttack")) {
         float randomBlockValue = Random.Range(0f, 100f);
         if (randomBlockValue < blockProbability) {
            Block();
         }
      }
      selectRandomCareBehave = true;
   }

   private void Block () {
      if (!combatController.isBlocking()) {
         combatController.block(true);
         Invoke("StopBlock", 1f);
      }
   }

   private void StopBlock () {
      if (combatController.isBlocking()) {
         combatController.block(false);
      }
   }
}
