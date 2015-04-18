using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    public float speed = 0.2f;
    public float jumpForce;
    public bool facingRight;

    private bool canJump = false;
    private bool attacking = false;
    private Vector2 actualMovement;

    private Animator animator;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        actualMovement = new Vector3(0, 0, 0);
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        actualMovement *= speed;
        if (actualMovement.y < 0)
            actualMovement.x = 0;
        setYVelocity();
        rigidBody.velocity = actualMovement;
        /*
        if ((facingRight && actualMovement.x < 0) || (!facingRight && actualMovement.x > 0))
            flip();
         */
        animator.SetFloat("VerticalMovement", actualMovement.y);
        animator.SetFloat("HorizontalMovement", Mathf.Abs(actualMovement.x));
        actualMovement = new Vector3(0, 0, 0);
    }

    private void setYVelocity()
    {
        if (actualMovement.y > 0 && canJump)
            rigidBody.AddForce(new Vector2(0, jumpForce));
            /*
        else if (actualMovement.y < 0 && canJump)
            actualMovement.y = actualMovement.y;
             * */
        else
            actualMovement.y = rigidBody.velocity.y;

    }

    private void flip()
    {
        facingRight = !facingRight;
        Quaternion rotation = transform.rotation;
        rotation.y =  -(rotation.y - 180);
        transform.rotation = rotation;
    }

    public void setMovementVector(Vector2 movement)
    {
        if (!attacking)
            actualMovement = movement;
    }

    public void blockMovement(bool movementBlocked)
    {
        attacking = movementBlocked;
        if (attacking)
            actualMovement.x = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((transform.position - collision.transform.position).magnitude > 0 && collision.gameObject.layer == 8)
            canJump = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((transform.position - collision.transform.position).magnitude > 0 && collision.gameObject.layer == 8)
            canJump = false;
    }

    public bool isFacingRight()
    {
        return facingRight;
    }

}
