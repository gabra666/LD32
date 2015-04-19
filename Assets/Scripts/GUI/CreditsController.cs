using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    private CanvasGroup canvasGroup;

    // Use this for initialization
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void BringToFront()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}
