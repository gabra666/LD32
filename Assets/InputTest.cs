using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour {

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
        if (Input.GetKeyDown(KeyCode.M))
            combatController.block(true);
        if (Input.GetKeyUp(KeyCode.M))
            combatController.block(false);
    }
}
