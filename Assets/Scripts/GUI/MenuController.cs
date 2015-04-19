﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {


	public Image image;
	public GameObject splash;
    public GameObject selectionCharacterPanel;
    public GameObject creditsPanel;

    private CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
		image.CrossFadeAlpha (0f, 0f,true);
		if (StorageManager.Instance.SeenSplash == true) {
			splash.SetActive(false);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadCharacterSelectionMenu(int numberOfPlayers)
	{
        StorageManager.Instance.NumberOfPlayers = numberOfPlayers;
		StartCoroutine ("CrossFade", selectionCharacterPanel);
	}

	public void LoadCreditsPanel()
	{
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
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
	
}