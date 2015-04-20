using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PunchMessagesController : MonoBehaviour {
    public GameObject player1Message;
    public GameObject player2Message;
    public Sprite[] sprites;


    private Transform player1MessagePosition;
    private Transform player2MessagePosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Show(GameObject player)
    {
        GameObject message = (player.tag == "Player") ? player1Message : player2Message;
        Transform messagePosition = (player.tag == "Player") ? player1MessagePosition : player2MessagePosition;
        int index = Random.Range(0, sprites.Length -1);
        message.SetActive(true);
        message.transform.position = messagePosition.position;
        message.GetComponent<Image>().sprite = sprites[index];
        StartCoroutine("MessageDisappear", message);
    }

    IEnumerator MessageDisappear(GameObject message)
    {
        yield return new WaitForSeconds(1.5f);
        message.SetActive(false);
    }

    void SetPlayer1TransformMessage(Transform player1Message)
    {
        player1MessagePosition = player1Message;
    }

    void SetPlayer2TransformMessage(Transform player2Message)
    {
        player2MessagePosition = player2Message;
    }
}
