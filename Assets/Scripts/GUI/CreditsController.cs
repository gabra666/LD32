using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsController : MonoBehaviour {

    private CanvasGroup canvasGroup;
    public GameObject menu;
    public Scrollbar scroll;

    private bool start = false;
    public float speed;
    private AudioSource creditsAudio;
    // Use this for initialization
    void Start()
    {
        creditsAudio = gameObject.GetComponent<AudioSource>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        scroll.value = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            if (scroll.value == 0)
                StartCoroutine("BackToMenu");
            else
                scroll.value -= speed*Time.deltaTime;
        }
	}

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(4);
        creditsAudio.Stop();
        scroll.value = 1;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        start = false;
        menu.SendMessage("BringToFront");
    }

    void BringToFront()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        StartCoroutine("ShowCredits", true);
        creditsAudio.Play();
    }

    private IEnumerator ShowCredits(bool show)
    {
        yield return new WaitForSeconds(1);
        start = show;
    }

}
