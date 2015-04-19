using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {


	public Image image;
	public GameObject splash;
	// Use this for initialization
	void Start () {

		image.CrossFadeAlpha (0f, 0f,true);
		if (Globals.seenSplash == true) {
			splash.SetActive(false);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene(int numberOfPlayers)
	{
        StorageManager.Instance.NumberOfPlayers = numberOfPlayers;
		StartCoroutine ("CrossFade", 1);
	}

	public void LoadCreditsPanel()
	{

	}

	public void LoadMainMenu()
	{

	}

	IEnumerator CrossFade(int levelnumber)
	{
		image.CrossFadeAlpha (1f, 1.0f,true);
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel (levelnumber);
	}

	public void QuitSplash()
	{
		Debug.Log ("Quit SPlash");
		Globals.seenSplash = true;
		splash.GetComponent<Image> ().CrossFadeAlpha (0.0f, 1f, true);
		splash.GetComponent<CanvasGroup> ().interactable = false;
		splash.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	

	}
	
}
