using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIFinishFightController : MonoBehaviour {
    public Text winnerMessage;
    private CanvasGroup canvasGroup;
	public AudioSource win_snd;
	public AudioSource lose_snd;

	// Use this for initialization
	void Start () {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FightFinished(string winnerName)
    {
        winnerMessage.text = winnerName + " Wins";
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
		if (winnerName == "Player 1")
			win_snd.Play ();
		else 
			lose_snd.Play();
        StartCoroutine("BackToMenu");
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("MainMenu");
    }
}
