using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public float speed = 0.2f;
    private bool facingRight = true;
    private bool punching = false;
    private Vector3 actualMovement;

    private Animator animator;

	// Use this for initialization
	void Start () {
        actualMovement = new Vector3(0, 0, 0);
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (punching)
        {
            punching = false;
            animator.SetBool("punching", false);
        }
        gameObject.transform.position += actualMovement * speed * Time.deltaTime;
        animator.SetFloat("HorizontalMovement", Mathf.Abs(actualMovement.x));
        if ((facingRight && actualMovement.x < 0) || (!facingRight && actualMovement.x > 0))
            flip();
	}

    private void flip()
    {
        facingRight = !facingRight;
        Quaternion rotation = transform.rotation;
        rotation.y = facingRight ? 0 : 180;
        transform.rotation = rotation;
    }

    public void setMovementVector(Vector3 movement)
    {
        actualMovement = movement;
    }

    public void hit()
    {
        punching = true;
        animator.SetBool("punching", true);
    }
}
