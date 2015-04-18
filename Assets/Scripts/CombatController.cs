using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CombatController : MonoBehaviour {
    public int maximunTimeCharging;
    public int chargedAttackMultiplier;

    private bool charging = false;
    private bool attacking = false;
    private bool blocking = false;
    private float timeTillAttackButtonPressed = 0;

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
            timeTillAttackButtonPressed += Time.deltaTime;
            if (!charging)
            {
                if (timeTillAttackButtonPressed >= 0.25)
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
        attacking = true;
        movementController.blockMovement(attacking);
    }

    private void chargeFailed()
    {
        Debug.Log("dasdadas");
        charging = false;
        attacking = false;
        animator.SetTrigger("chargeFailed");
        animator.SetBool("charging", false);
    }

    public void releaseAttack()
    {
        if (attacking)
        {
            if (!charging)
            {
                animator.SetBool("punching", true);
                makeDamegeIfEnemyHasBeenBeaten(0, 10);
            }
            else
            {
                animator.SetBool("charging", false);
                makeDamegeIfEnemyHasBeenBeaten(1, 25);
            }

            timeTillAttackButtonPressed = 0;
            attacking = false;
            charging = false;
            movementController.blockMovement(false);
        }
    }

    private void makeDamegeIfEnemyHasBeenBeaten(int attackType, int damage)
    {
        int rayDirection = movementController.isFacingRight() ? 1 : -1;
        Vector3 rayOrigin = gameObject.transform.position;
        rayOrigin.x += (rayDirection * gameObject.GetComponent<Collider2D>().transform.localScale.x / 2);
        RaycastHit2D[] objectBeaten = Physics2D.RaycastAll(rayOrigin, new Vector2(rayDirection, 0), 0.3f);
        if (objectBeaten.Length > 0)
            foreach (RaycastHit2D raycast in objectBeaten)
            {
                raycast.collider.gameObject.SendMessage("ReceiveDamage", new Attack(attackType, damage), SendMessageOptions.DontRequireReceiver);
                Debug.Log(raycast.collider.gameObject.name);
            }
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
    }
}
