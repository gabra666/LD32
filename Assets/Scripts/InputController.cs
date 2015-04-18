using UnityEngine;
using System.Collections;
public class InputController : MonoBehaviour {
    MovementController movementController;
    AttackController attackController;

	// Use this for initialization
	void Start () {
        movementController = gameObject.GetComponent<MovementController>();
        attackController = gameObject.GetComponent<AttackController>();
	}
	
	// Update is called once per frame
	void Update () {
        movementController.setMovementVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Input.GetKeyDown(KeyCode.Space))
            attackController.attack();
	}
}
