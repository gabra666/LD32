using UnityEngine;
using System.Collections;
public class InputController : MonoBehaviour {
   MovementController movementController;
   CombatController combatController;

   private delegate void PlayerInputChecker ();
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

   private void checkPlayer1Input () {
      Vector2 movement = new Vector2(0, 0);
      if (Input.GetKey(KeyCode.A))
         movement.x = -1;
      else if (Input.GetKey(KeyCode.D))
         movement.x = 1;

      if (Input.GetJoystickNames().Length > 0) {
         movement.x = Input.GetAxis("movement1");
      }

      movementController.setMovementVector(movement);

      if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("attack1"))
         combatController.chargeAttack();

      if (Input.GetKeyUp(KeyCode.F) || Input.GetButtonUp("attack1"))
         combatController.releaseAttack();

      if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("block1"))
         combatController.block(true);
      if (Input.GetKeyUp(KeyCode.G) || Input.GetButtonUp("block1"))
         combatController.block(false);

      if (Input.GetKeyDown(KeyCode.Escape))
         UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
   }

   private void checkPlayer2Input () {
      Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0);
      if (Input.GetJoystickNames().Length > 1) {
         movement.x = Input.GetAxis("movement2");
      }
      movementController.setMovementVector(movement);
      if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("attack2"))
         combatController.chargeAttack();

      if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetButtonUp("attack2"))
         combatController.releaseAttack();

      if (Input.GetKey(KeyCode.Return) || Input.GetButtonDown("block2"))
         combatController.block(true);
      if (Input.GetKeyUp(KeyCode.Return) || Input.GetButtonUp("block2"))
         combatController.block(false);

      if (Input.GetKeyDown(KeyCode.Delete))
         UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
   }
}
