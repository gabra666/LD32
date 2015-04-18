using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = player.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
	}
}
