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
        Vector2 movement = new Vector2(0,0);
        if (Input.GetKey(KeyCode.A))
            movement.x = -1;
        else if (Input.GetKey(KeyCode.D))
            movement.x = 1;

        if (Input.GetKeyDown(KeyCode.W))
            movement.y = 1;

        movementController.setMovementVector(movement);

        if (Input.GetKeyDown(KeyCode.F))
            combatController.chargeAttack();

        if (Input.GetKeyUp(KeyCode.F))
            combatController.releaseAttack();

        if (Input.GetKeyDown(KeyCode.G))
            combatController.block(true);
        if (Input.GetKeyUp(KeyCode.G))
            combatController.block(false);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel("MainMenu");
	}
}
