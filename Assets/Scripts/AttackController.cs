using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {
    private bool attacking = false;

    private Animator animator;
    private MovementController movementController;
	// Use this for initialization
	void Start () {
	    animator = gameObject.GetComponent<Animator>();
        movementController = gameObject.GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void attack()
    {
        attacking = true;
        animator.SetBool("punching", true);
        movementController.characterIsAttacking(attacking);
    }

    public void hitStopped()
    {
        attacking = false;
        movementController.characterIsAttacking(attacking);
    }
}
