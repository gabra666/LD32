using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {


   public Image image;
   public GameObject splash;
   public GameObject selectionCharacterPanel;
   public GameObject creditsPanel;
   public Selectable firstSelectedObjectInMainMenu;
   public StandaloneInputModule inputModule;

   private CanvasGroup canvasGroup;
   private AudioSource theme;

   void Start () {
      StorageManager.Instance.SeenSplash = true;
      canvasGroup = gameObject.GetComponent<CanvasGroup>();
      image.CrossFadeAlpha(0f, 0f, true);
      if (StorageManager.Instance.SeenSplash == true) {
         splash.SetActive(false);
      }

      StorageManager.Instance.Player1CharacterName = "";
      StorageManager.Instance.Player2CharacterName = "";
      theme = gameObject.GetComponent<AudioSource>();
   }

   void Update () {
      if (Input.GetKeyDown(KeyCode.Escape))
         Application.Quit();
   }

   public void LoadCharacterSelectionMenu (int numberOfPlayers) {
      StorageManager.Instance.NumberOfPlayers = numberOfPlayers;
      StartCoroutine("CrossFade", selectionCharacterPanel);
   }

   public void LoadCreditsPanel () {
      theme.Stop();
      StartCoroutine("CrossFade", creditsPanel);
   }

   IEnumerator CrossFade (GameObject nextPanel) {
      image.CrossFadeAlpha(1f, 0.5f, true);
      canvasGroup.interactable = false;
      canvasGroup.blocksRaycasts = false;
      canvasGroup.alpha = 0;
      image.CrossFadeAlpha(0f, 0.5f, true);
      nextPanel.SendMessage("BringToFront");
      yield return new WaitForSeconds(1.0f);
   }

   public void QuitSplash () {
      Debug.Log("QuitSplash");
      StorageManager.Instance.SeenSplash = true;
      splash.GetComponent<Image>().CrossFadeAlpha(0.0f, 1f, true);
      splash.GetComponent<CanvasGroup>().interactable = false;
      splash.GetComponent<CanvasGroup>().blocksRaycasts = false;
   }

   void BringToFront () {
      inputModule.cancelButton = "block1";
      inputModule.submitButton = "attack1";
      inputModule.horizontalAxis = "movement1";
      if (!theme.isPlaying) {
         theme.Play();
      }
      firstSelectedObjectInMainMenu.Select();
      canvasGroup.interactable = true;
      canvasGroup.blocksRaycasts = true;
      canvasGroup.alpha = 1;
   }

}
