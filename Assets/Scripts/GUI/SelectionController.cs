using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectionController : MonoBehaviour {

   public Text characterSlectionMessage;
   public Image image;
   public GameObject mainMenuPanel;
   public GameObject rules;
   public Selectable firstSelectedObjectInSelectionMenu;
   public StandaloneInputModule inputModule;

   private StorageManager storageManager;
   private CanvasGroup canvasGroup;

   void Start () {
      storageManager = StorageManager.Instance;
      canvasGroup = gameObject.GetComponent<CanvasGroup>();
   }

   void Update () {
      if (Input.GetButtonDown(inputModule.cancelButton) && canvasGroup.alpha == 1) {
         goBack();
      }
   }

   public void CharacterSelected (string characterName) {
      if (storageManager.Player1CharacterName == "") {
         if (storageManager.NumberOfPlayers == 2) {
            inputModule.cancelButton = "block2";
            inputModule.submitButton = "attack2";
            inputModule.horizontalAxis = "movement2";
         }
         storageManager.Player1CharacterName = characterName;
         characterSlectionMessage.text = (storageManager.NumberOfPlayers == 1) ? "Select your oponent" : "Player 2, select your character";
      } else {
         storageManager.Player2CharacterName = characterName;
         loadGame();
      }
      rules.SendMessage("PassRule");
   }

   public void goBack () {
      if (storageManager.Player1CharacterName != "") {
         inputModule.cancelButton = "block1";
         inputModule.submitButton = "attack1";
         inputModule.horizontalAxis = "movement1";
         storageManager.Player1CharacterName = "";
         characterSlectionMessage.text = "Player 1, select your character";
         rules.SendMessage("PassRule");
      } else
         StartCoroutine("BackToMenu", 1);
   }

   IEnumerator BackToMenu (int levelnumber) {
      image.CrossFadeAlpha(1f, 0.5f, true);
      canvasGroup.interactable = false;
      canvasGroup.blocksRaycasts = false;
      canvasGroup.alpha = 0;
      image.CrossFadeAlpha(0f, 0.5f, true);
      mainMenuPanel.SendMessage("BringToFront");
      yield return new WaitForSeconds(1.0f);
   }

   private void loadGame () {
      canvasGroup.interactable = false;
      StartCoroutine("CrossFade", 1);
   }

   IEnumerator CrossFade (int levelnumber) {
      image.CrossFadeAlpha(1f, 1.0f, true);
      yield return new WaitForSeconds(1.0f);
      UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelnumber);
   }

   void BringToFront () {
      firstSelectedObjectInSelectionMenu.Select();
      canvasGroup.interactable = true;
      canvasGroup.blocksRaycasts = true;
      canvasGroup.alpha = 1;
   }

}
