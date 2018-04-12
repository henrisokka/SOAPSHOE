using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
	public int points = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void morePoints (int newPoints) {
		points += newPoints;
		Debug.Log ("POINTS: " + points);
	}
}
