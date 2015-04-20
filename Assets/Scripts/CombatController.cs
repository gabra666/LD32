﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CombatController : MonoBehaviour {
    public int maximunTimeCharging;
    public int chargedAttackMultiplier;
    public float attackRange;

    private bool charging = false;
    private bool attacking = false;
    private bool blocking = false;
    private float timeTillAttackButtonPressed = 0;

	public AudioSource chargin_snd;
	public AudioSource attack_snd;
	public AudioSource chargedAttack_snd;
	public AudioSource grito_snd;
	public AudioSource bloqueado_snd;
    private Attack currentAttack;
    private AudioSource currentAttackTSound;

    private Animator animator;
    private MovementController movementController;
	// Use this for initialization
	void Start () {
	    animator = gameObject.GetComponent<Animator>();
        movementController = gameObject.GetComponent<MovementController>();

	}
	
	// Update is called once per frame
	void Update () {
        if (attacking)
        {
            //timeTillAttackButtonPressed += Time.deltaTime;
            if (!charging)
            {
                if (timeTillAttackButtonPressed >= 1.25)
                {
                    animator.SetBool("charging", true);
                    charging = true;
                }
            }
            else
                if (maximunTimeCharging < timeTillAttackButtonPressed)
                    chargeFailed();
        }
	}

    public void chargeAttack()
    {
        if (!attacking)
        {
            attacking = true;
            movementController.blockMovement(attacking);

            animator.SetBool("punching", true);
            currentAttack = new Attack(0, 10);
            currentAttackTSound = attack_snd;
            currentAttackTSound.Play();
        }
    }

    private void chargeFailed()
    {
		chargin_snd.Stop();
		grito_snd.Play ();
        charging = false;
        attacking = false;
        animator.SetTrigger("chargeFailed");
        timeTillAttackButtonPressed = 0;
    }

    public void releaseAttack()
    {
        return;
		chargin_snd.Stop();

        if (attacking)
        {
            if (!charging)
            {
                animator.SetBool("punching", true);
                currentAttack = new Attack(0, 10);
                currentAttackTSound = attack_snd;
            }
            else
            {
                animator.SetBool("charging", false);
                currentAttack = new Attack(1, 25);
				currentAttackTSound = chargedAttack_snd;
            }

            timeTillAttackButtonPressed = 0;
            attacking = false;
            charging = false;
            movementController.blockMovement(false);
        }
    }

    public void makeDamegeIfEnemyHasBeenBeaten()
    {
        int rayDirection = movementController.isFacingRight() ? 1 : -1;
        Vector3 rayOrigin = gameObject.transform.position;


        RaycastHit2D[] objectBeaten = Physics2D.RaycastAll(rayOrigin, new Vector2(rayDirection, 0));
        foreach (RaycastHit2D raycast in objectBeaten)
            if (raycast.collider.gameObject != gameObject && Mathf.Abs(gameObject.transform.position.x - raycast.collider.gameObject.transform.position.x) <= attackRange)
                raycast.collider.gameObject.SendMessage("ReceiveDamage", currentAttack, SendMessageOptions.DontRequireReceiver);
    }

    public void block(bool block)
    {
        blocking = block;
        animator.SetBool("blocking", blocking);
        movementController.blockMovement(blocking);
    }

    public void hitStopped()
    {
        attacking = false;
        movementController.blockMovement(attacking);
        makeDamegeIfEnemyHasBeenBeaten();
    }

    public bool isBlocking()
    {
        return blocking;
    }

    public void breakDefense()
    {
        blocking = false;
        animator.SetTrigger("defenseBroken");
        //movementController.blockMovement(blocking);
    }

    public void damaged()
    {
        animator.SetTrigger("damageReceived");
		grito_snd.Play ();
    }
}
