using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PunchMessagesController : MonoBehaviour {
    public GameObject player1Message;
    public GameObject player2Message;
    public Image image1;
    public Image image2;
    public Sprite[] sprites;


    private GameObject player1MessagePosition;
    private GameObject player2MessagePosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Show(GameObject player)
    {
        GameObject message = (player.tag == "Player") ? player1Message : player2Message;
        Image image = (player.tag == "Player") ? image1 : image2;
        Transform messagePosition = (player.tag == "Player") ? player1MessagePosition.transform : player2MessagePosition.transform;
        int index = Random.Range(0, sprites.Length -1);
        message.SetActive(true);
        message.transform.position = Camera.main.WorldToScreenPoint(messagePosition.position);
        image.sprite = sprites[index];
        StartCoroutine("MessageDisappear", message);
    }

    IEnumerator MessageDisappear(GameObject message)
    {
        yield return new WaitForSeconds(1.5f);
        message.SetActive(false);
    }

    void SetPlayer1TransformMessage(GameObject player1Message)
    {
        Debug.Log("player1");
        player1MessagePosition = player1Message;
    }

    void SetPlayer2TransformMessage(GameObject player2Message)
    {
        Debug.Log("Player2");
        player2MessagePosition = player2Message;
    }
}
