using UnityEngine;
using System.Collections;

public class AxelController : MonoBehaviour {
	private float moveX;
	private float moveY;
	
	private Animator anim;
	public float speed = 10;
	private bool facingRight = false;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		//Debug.Log(moveX);
		if(moveX != 0){
			anim.SetFloat("Speed",Mathf.Abs(moveX));
		}
		else if(moveY != 0){
			anim.SetFloat("Speed",Mathf.Abs(moveY));
		}

		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, moveY * speed);


		if(moveX > 0 && !facingRight){
			Flip ();
		}
		else if(moveX < 0 && facingRight){
			Flip ();
		}
	}

	private void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isFacingRight(){
		return facingRight;
	}
}
