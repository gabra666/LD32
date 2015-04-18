using UnityEngine;
using System.Collections;
public class InputController : MonoBehaviour {
    MovementController movementController;
    CombatController combatController;

	// Use this for initialization
	void Start () {
        movementController = gameObject.GetComponent<MovementController>();
        combatController = gameObject.GetComponent<CombatController>();
	}
	
	// Update is called once per frame
	void Update () {
        movementController.setMovementVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Input.GetKeyDown(KeyCode.Space))
            combatController.chargeAttack();

        if (Input.GetKeyUp(KeyCode.Space))
            combatController.releaseAttack();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            combatController.block(true);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            combatController.block(false);
	}
}
