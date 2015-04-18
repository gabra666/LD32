using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {

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
            if (timeTillAttackButtonPressed >= 0.25)
                animator.SetBool("charging", true);
        }
	}

    public void chargeAttack()
    {
        attacking = true;
        movementController.blockMovement(attacking);
    }

    public void releaseAttack()
    {
        if (timeTillAttackButtonPressed < 0.25)
            animator.SetBool("punching", true);
        else
            animator.SetBool("charging", false);
        timeTillAttackButtonPressed = 0;
        attacking = false;
        movementController.blockMovement(false);
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
}
