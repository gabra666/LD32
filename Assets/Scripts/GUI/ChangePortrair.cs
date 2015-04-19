using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangePortrair : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetPortrait(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
