using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
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

        float xPosition = player2.transform.position.x + (player1.transform.position.x - player2.transform.position.x)/2;
        Vector3 newPosition = new Vector3(xPosition, transform.position.y, transform.position.z);

        if (checkPlayerIsOutsideScreen(Camera.main.WorldToScreenPoint(player1.transform.position), remotenessOffset) ||
            checkPlayerIsOutsideScreen(Camera.main.WorldToScreenPoint(player2.transform.position), remotenessOffset))
        {
            if (transform.position.z < maxDistance)
                newPosition.z += Time.deltaTime * speed * 1;
        }
	}

    private bool checkPlayerIsOutsideScreen(Vector3 playerScreenPosition, float offset)
    {
        return (Screen.width - playerScreenPosition.x < offset || playerScreenPosition.x < offset
            || Screen.height - playerScreenPosition.y < offset);
    }
}
