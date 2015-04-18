using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public float speed = 0.2f;
    private bool facingRight = true;
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
        actualMovement.y = rigidBody.velocity.y;
        rigidBody.velocity = actualMovement * speed;
        if ((facingRight && actualMovement.x < 0) || (!facingRight && actualMovement.x > 0))
            flip();
        animator.SetFloat("HorizontalMovement", Mathf.Abs(actualMovement.x));
    }

    private void flip()
    {
        facingRight = !facingRight;
        Quaternion rotation = transform.rotation;
        rotation.y = facingRight ? 0 : 180;
        transform.rotation = rotation;
    }

    public void setMovementVector(Vector2 movement)
    {
        actualMovement = movement;
    }

    public void hit()
    {
        animator.SetBool("punching", true);
    }

}
