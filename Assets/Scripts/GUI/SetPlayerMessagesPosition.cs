using UnityEngine;
using System.Collections;

public class SetPlayerMessagesPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}

    void OnEnable()
    {
        GameObject.Find("PunchMessagesController").SendMessage(
            (gameObject.transform.parent.tag == "Player") ? "SetPlayer1TransformMessage" : "SetPlayer2TransformMessage", gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
