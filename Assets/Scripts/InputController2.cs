using UnityEngine;
using System.Collections;

public class InputController2 : MonoBehaviour {

    MovementController movementController;
    CombatController combatController;

    // Use this for initialization
    void Start()
    {
        movementController = gameObject.GetComponent<MovementController>();
        combatController = gameObject.GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
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
