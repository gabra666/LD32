using UnityEngine;
using System.Collections;

public class FollowPlayers : MonoBehaviour {
    public float minDistance;
    public float maxDistance;
    public float remotenessOffset;
    public float approachOffset;
    public float speed;

    private GameObject player1;
    private GameObject player2;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
	}
	
	// Update is called once per frame
	void Update () {

        //float xPosition = player2.transform.position.x + (player1.transform.position.x - player2.transform.position.x)/2;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (checkPlayerIsOutsideScreen(Camera.main.WorldToScreenPoint(player1.transform.position)) ||
            checkPlayerIsOutsideScreen(Camera.main.WorldToScreenPoint(player2.transform.position)))
        {
            if (transform.position.z > maxDistance)
                newPosition.z -= speed * Time.deltaTime;
        }
        else if (checkPlayerIsInsideScreen(Camera.main.WorldToScreenPoint(player1.transform.position)) ||
            checkPlayerIsInsideScreen(Camera.main.WorldToScreenPoint(player2.transform.position)))
            if (transform.position.z < minDistance)
                newPosition.z += speed * Time.deltaTime;
        transform.position = newPosition;
	}

    private bool checkPlayerIsOutsideScreen(Vector3 playerScreenPosition)
    {
        return (Screen.width - playerScreenPosition.x < remotenessOffset || playerScreenPosition.x < remotenessOffset
            || Screen.height - playerScreenPosition.y < remotenessOffset);
    }

    private bool checkPlayerIsInsideScreen(Vector3 playerScreenPosition)
    {
        return (Screen.width - playerScreenPosition.x > approachOffset && playerScreenPosition.x > approachOffset
            && Screen.height - playerScreenPosition.y > approachOffset);
    }
}
