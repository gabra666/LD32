using UnityEngine;
using System.Collections;
public class InputController : MonoBehaviour {
    CharacterController characterController;

	// Use this for initialization
	void Start () {
        characterController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        characterController.setMovementVector(new Vector2(Input.GetAxis("Horizontal"), 0));
        if (Input.GetKeyDown(KeyCode.Space))
            characterController.hit();
	}
}
