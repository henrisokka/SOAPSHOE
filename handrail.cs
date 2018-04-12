using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handrail : MonoBehaviour {

	Vector3 railPosition;
	Vector3 railRotation;




	void Start () {
		railPosition = transform.position;

	}

	void Update() {
		transform.position = railPosition;
		gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	void OnTriggerEnter2D (Collider2D coll) {
		Debug.Log ("ONTRIGGERENTER2D");
		coll.gameObject.GetComponent<playerController> ().onRail = true;
		coll.gameObject.GetComponent<playerController> ().railStarted = true;
		coll.gameObject.GetComponent<playerController> ().railEnded = false;
	}
		
	void OnTriggerExit2D(Collider2D coll) {
		coll.gameObject.GetComponent<playerController> ().onRail = false;
		Debug.Log ("Pois reililtä");
	}
}
