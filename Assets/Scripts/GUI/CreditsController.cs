using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    private CanvasGroup canvasGroup;
    public GameObject movingPanel;
    public float speed;

    private bool start = false;

    // Use this for initialization
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            Vector3 newPosition = movingPanel.transform.localPosition;
            newPosition.y += speed * Time.deltaTime;
            movingPanel.transform.localPosition = newPosition;
        }
	}

    void BringToFront()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        StartCoroutine("ShowCredits");
    }

    private IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("dasdasds");
        start = true;
    }

}
