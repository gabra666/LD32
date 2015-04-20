using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ruleRandomizer : MonoBehaviour {


	public Sprite[] rules;
	private float random;
	// Use this for initialization
	void Start () {
        PassRule();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void PassRule()
    {
        random = (int)Random.Range(0, 7);
        this.GetComponent<Image>().sprite = rules[(int)random];
    }
}
