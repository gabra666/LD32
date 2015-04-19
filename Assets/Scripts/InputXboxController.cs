using UnityEngine;
using XboxCtrlrInput;
using System.Collections;



public class InputXboxController : MonoBehaviour {

	public int playerNumber = 0;


	// Use this for initialization
	void Start () {
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		Debug.Log (" numero de mandos" + playerNumber.ToString() );

	}
	
	// Update is called once per frame
	void Update () {

		if(XCI.GetButtonDown(XboxButton.LeftStick, playerNumber))
		{
			Debug.Log("pulsado left stick " + playerNumber.ToString() );
		}
		if(XCI.GetButtonDown(XboxButton.A , playerNumber))
		{
			Debug.Log("pulsado botton A" + playerNumber.ToString() );
		}

	}
}
