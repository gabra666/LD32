﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PunchMessagesController : MonoBehaviour {
    public GameObject player1Message;
    public GameObject player2Message;
    public Sprite[] sprites;


    public Transform player1MessagePosition;
    public Transform player2MessagePosition;

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
        message.transform.position = messagePosition.position;
        int index = Random.Range(0, sprites.Length -1);
        message.GetComponent<Image>().sprite = sprites[index];
        message.SetActive(true);
        StartCoroutine("MessageDisappear", message);
    }

    IEnumerator MessageDisappear(GameObject message)
    {
        yield return new WaitForSeconds(1.5f);
        message.SetActive(false);
    }

}
