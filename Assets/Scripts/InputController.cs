using UnityEngine;
using System.Collections;
public class InputController : MonoBehaviour {
    MovementController movementController;
    CombatController combatController;

    private delegate void PlayerInputChecker();
    private PlayerInputChecker playerInputChecker;
	// Use this for initialization
	void Start () {
        movementController = gameObject.GetComponent<MovementController>();
        combatController = gameObject.GetComponent<CombatController>();
        if (tag == "Player")
            playerInputChecker = new PlayerInputChecker(checkPlayer1Input);
        else
            playerInputChecker = new PlayerInputChecker(checkPlayer2Input);
	}
	
	// Update is called once per frame
	void Update () {
        playerInputChecker();
	}

    private void checkPlayer1Input()
    {
        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A))
            movement.x = -1;
        else if (Input.GetKey(KeyCode.D))
            movement.x = 1;

//        if (Input.GetKeyDown(KeyCode.W))
//            movement.y = 1;

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

    private void checkPlayer2Input()
    {
        movementController.setMovementVector(new Vector2(Input.GetAxis("Horizontal"), Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0));
        if (Input.GetKeyDown(KeyCode.RightShift))
            combatController.chargeAttack();

        if (Input.GetKeyUp(KeyCode.RightShift))
            combatController.releaseAttack();

        if (Input.GetKey(KeyCode.Return))
            combatController.block(true);
        if (Input.GetKeyUp(KeyCode.Return))
            combatController.block(false);

        if (Input.GetKeyDown(KeyCode.Delete))
            Application.LoadLevel("MainMenu");
    }
}
