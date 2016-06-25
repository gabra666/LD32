using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {


	public Image image;
	public GameObject splash;
    public GameObject selectionCharacterPanel;
    public GameObject creditsPanel;
   public Selectable firstSelectedObjectInMainMenu;

   private CanvasGroup canvasGroup;
    private AudioSource theme;

	// Use this for initialization
	void Start () {
		StorageManager.Instance.SeenSplash = true;
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
		image.CrossFadeAlpha (0f, 0f,true);
		if (StorageManager.Instance.SeenSplash == true) {
			splash.SetActive(false);
		}

        StorageManager.Instance.Player1CharacterName = "";
        StorageManager.Instance.Player2CharacterName = "";
        theme = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

	public void LoadCharacterSelectionMenu(int numberOfPlayers)
	{
        StorageManager.Instance.NumberOfPlayers = numberOfPlayers;
		StartCoroutine ("CrossFade", selectionCharacterPanel);
	}

	public void LoadCreditsPanel()
	{
        theme.Stop();
        StartCoroutine("CrossFade", creditsPanel);
	}

	IEnumerator CrossFade(GameObject nextPanel)
	{
		image.CrossFadeAlpha (1f, 0.5f,true);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        image.CrossFadeAlpha(0f, 0.5f, true);
        nextPanel.SendMessage("BringToFront");
		yield return new WaitForSeconds(1.0f);
	}

	public void QuitSplash()
	{
        Debug.Log("QuitSplash");
		StorageManager.Instance.SeenSplash = true;
		splash.GetComponent<Image> ().CrossFadeAlpha (0.0f, 1f, true);
		splash.GetComponent<CanvasGroup> ().interactable = false;
		splash.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

    void BringToFront()
    {
      if (!theme.isPlaying) {
        theme.Play();
      }
      firstSelectedObjectInMainMenu.Select();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
	
}
