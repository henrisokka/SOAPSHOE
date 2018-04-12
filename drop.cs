using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D coll) {
		coll.gameObject.GetComponent<playerController> ().groundY -= 0.9f;
	}
}
