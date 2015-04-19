using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortraitController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetPortrait(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
