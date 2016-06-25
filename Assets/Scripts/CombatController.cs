using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CombatController : MonoBehaviour {
   public float coolDownAttack;
   public float cooldDownBlock;
   public int maximunTimeCharging;
   public int chargedAttackMultiplier;
   public float attackRange;
   public float maximunBlockTimeUntillValidParry;
   public float parryExtraDamageFactor;

   private bool charging = false;
   private bool attacking = false;
   private bool blocking = false;
   private float timeTillAttackButtonPressed = 0;

   public AudioSource chargin_snd;
   public AudioSource attack_snd;
   public AudioSource chargedAttack_snd;
   public AudioSource grito_snd;
   //public AudioSource bloqueado_snd;
   private Attack currentAttack;
   private AudioSource currentAttackTSound;

   private Animator animator;
   private MovementController movementController;
   private float lastAttackTime = 0;
   private float lastBlockTime = 0;
   [HideInInspector]
   public bool BlockDisabled = false;

   // Use this for initialization
   void Start () {
      animator = gameObject.GetComponent<Animator>();
      movementController = gameObject.GetComponent<MovementController>();

   }

   // Update is called once per frame
   void Update () {
      if (attacking) {
         timeTillAttackButtonPressed += Time.deltaTime;
         if (!charging) {
            if (timeTillAttackButtonPressed >= 0.3) {
               animator.SetBool("charging", true);
               charging = true;
            }
         }
         /*
     else
         if (maximunTimeCharging < timeTillAttackButtonPressed)
             chargeFailed();
          * */
      } else
         animator.SetBool("punching", false);
   }

   public void chargeAttack () {
      if (!attacking && Time.time - lastAttackTime >= coolDownAttack) {
         lastAttackTime = Time.time;
         attacking = true;
         movementController.blockMovement(attacking);
      }
   }

   private void chargeFailed () {
      chargin_snd.Stop();
      grito_snd.Play();
      charging = false;
      attacking = false;
      animator.SetTrigger("chargeFailed");
      timeTillAttackButtonPressed = 0;
   }

   public void releaseAttack () {
      chargin_snd.Stop();

      if (attacking) {
         attack(parryExtraDamageFactor);
      }
   }

   private void attack (float damageFactor, bool parry = false) {
      if (!charging) {
         animator.SetBool(parry ? "parry" : "punching", true);
         currentAttack = new Attack(0, (int) (10 * damageFactor));
         currentAttackTSound = attack_snd;
      } else {
         animator.SetBool("charging", false);
         currentAttack = new Attack(1, 25);
         currentAttackTSound = chargedAttack_snd;
      }
      currentAttackTSound.Play();
      timeTillAttackButtonPressed = 0;
      attacking = false;
      charging = false;
      movementController.blockMovement(false);
   }

   public void makeDamageIfEnemyHasBeenBeaten () {
      int rayDirection = movementController.isFacingRight() ? 1 : -1;
      Vector3 rayOrigin = gameObject.transform.position;


      RaycastHit2D[] objectBeaten = Physics2D.RaycastAll(rayOrigin, new Vector2(rayDirection, 0));
      foreach (RaycastHit2D raycast in objectBeaten)
         if (raycast.collider.gameObject != gameObject &&
             Mathf.Abs(gameObject.transform.position.x - raycast.collider.gameObject.transform.position.x) <= attackRange)
            raycast.collider.gameObject.SendMessage("ReceiveDamage", currentAttack, SendMessageOptions.DontRequireReceiver);
   }

   public void block (bool block) {
      if (block && (Time.time - lastBlockTime < cooldDownBlock || BlockDisabled)) {
         return;
      } else if (block) {
         lastBlockTime = Time.time;
      }

      blocking = block;
      animator.SetBool("blocking", blocking);
      movementController.blockMovement(blocking);
   }

   public void hitStopped () {
      attacking = false;
      movementController.blockMovement(attacking);
      makeDamageIfEnemyHasBeenBeaten();
      animator.SetBool("punching", false);
   }

   public void hitBlocked () {
      Debug.Log(" golpe bloqueado");
      //deberia ver como averiguo cual es el sonido del golpe en concreto que se bloqueo para pararlo
      //attack_snd.Stop ();
   }

   public bool isBlocking () {
      return blocking;
   }

   public void breakDefense () {
      blocking = false;
      animator.SetTrigger("defenseBroken");
      //movementController.blockMovement(blocking);
   }

   public void damaged () {
      animator.SetTrigger("damageReceived");
      grito_snd.Play();
   }

   public void defenseBroken () {
      grito_snd.Play();
   }

   public bool IsParry {
      get { return Time.time - lastBlockTime <= maximunBlockTimeUntillValidParry; }
   }

   public void MakeParry (GameObject enemy) {
      enemy.GetComponent<CombatController>().BlockDisabled = true;
      StartCoroutine(parry(enemy));
   }

   private IEnumerator parry (GameObject enemy) {
      block(false);
      yield return new WaitForSeconds(0.2f);
      attack(parryExtraDamageFactor);
      lastAttackTime = Time.time;
      yield return new WaitForSeconds(0.4f);
      enemy.GetComponent<CombatController>().BlockDisabled = false;
   }
}
