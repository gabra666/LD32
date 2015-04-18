using UnityEngine;
using System.Collections;

public class CamBehaviour : MonoBehaviour {

	public Transform camTransform;
	public GameObject player;

	public float CamObjX;
	public float CamObjY;
	
	//Vector3 yy;

	// Use this for initialization
	void Start () {
		CamObjX=player.GetComponent<Transform>().position.x;
		CamObjY=player.GetComponent<Transform>().position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			CamObjX=player.GetComponent<Transform>().position.x+3;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			CamObjX=player.GetComponent<Transform>().position.x-3;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			CamObjY=player.GetComponent<Transform>().position.y-3;
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			CamObjY=player.GetComponent<Transform>().position.y+3;
		}

		if (CamObjX>camTransform.position.x) {//R
			camTransform.position=new Vector3(camTransform.position.x-((camTransform.position.x-CamObjX)*0.1f),camTransform.position.y,camTransform.position.z);
		}
		if (CamObjX<camTransform.position.x) {//L
			camTransform.position=new Vector3(camTransform.position.x+((CamObjX-camTransform.position.x)*0.1f),camTransform.position.y,camTransform.position.z);
		}
		if (CamObjY<camTransform.position.y) {//U
			camTransform.position=new Vector3(camTransform.position.x,camTransform.position.y-((camTransform.position.y-CamObjY)*0.1f),camTransform.position.z);
		}
		if (CamObjY>camTransform.position.y) {//D
			camTransform.position=new Vector3(camTransform.position.x,camTransform.position.y+((CamObjY-camTransform.position.y)*0.1f),camTransform.position.z);
		}
	}
}
