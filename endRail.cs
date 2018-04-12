using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endRail : MonoBehaviour {
	void OnTriggerExit2D (Collider2D coll) {
		coll.gameObject.GetComponent<playerController> ().railEnded = true;
	}
}
