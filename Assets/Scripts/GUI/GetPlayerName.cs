using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetPlayerName : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    GetComponent<Text>().text = GameObject.FindGameObjectWithTag(transform.parent.name).name;  
	}
	
	// Update is called once per frame
	void Update () {
	}
}
