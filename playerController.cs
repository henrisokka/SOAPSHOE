using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	public float playerSpeed;
	public float xVel;
	public float yVel;
	public float gravity;
	public float groundY; // level of the ground

	float groundVector;
	float jumpStartTime;

	public bool onRail = false;
	public bool gameOn = false;
	public bool railStarted = false;
	public bool railEnded = false;

	bool jumpOn = false;

	public GameObject gameCtrl;
	Vector2 touchOrigin = Vector2.one;
	Animator anim;


	void Start () {
		groundVector = yVel;
		groundY = transform.position.y;
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - jumpStartTime > 0.3f && jumpOn) {
			Debug.Log ("JUMP OFF");
			jumpOn = false;
			anim.SetBool ("jump", false);
		}

		if (Input.GetKeyUp (KeyCode.Space))
			gameOn = true;
			//jump ();

		if (jumpOn) {
			yVel = 0.15f;
		} else {
			yVel = groundVector;
		}

		if (onRail && !jumpOn) {
			yVel = 0;
			//anim.SetBool ("onRail", true);
			Debug.Log ("ON RAIL!");
		} else {
			anim.SetBool ("onRail", false);
		}
		if (railStarted && !railEnded && !jumpOn) {
			anim.SetBool ("onRail", true);
		} else if (railStarted && railEnded) {
			anim.SetBool ("onRail", false);
			railStarted = false;
		}

		if (Input.touchCount > 0) { // jos kosketaan niin tarkastetaan sen suunta
			Touch myTouch = Input.touches [0];

			if (myTouch.phase == TouchPhase.Began) {
				if (!gameOn)
					gameOn = true;
				
				touchOrigin = myTouch.position;
			} else if (myTouch.phase == TouchPhase.Ended) {
				Vector2 touchEnd = myTouch.position;
				Vector3 newPosition = Vector3.one;

				float x = touchOrigin.x - touchEnd.x;
				float y = touchOrigin.y - touchEnd.y;


				float compareX = x > 0 ? x : -x;
				float compareY = y > 0 ? y : -y;

				bool vertical = compareX < compareY ? true : false;

				if (x < 0 && !vertical) {
					Debug.Log ("right");
					if (railStarted && !railEnded && !jumpOn) {
						gameCtrl.GetComponent<gameController> ().morePoints (10);
					}
				}

				if (x > 0 && !vertical) {
					Debug.Log ("left");

				}

				if (y < 0 && vertical) {
					Debug.Log ("up");
					if (railStarted && !railEnded) {
						gameCtrl.GetComponent<gameController> ().morePoints (33);
					}
					jump ();
				}

				if (y > 0 && vertical) {
					Debug.Log ("down");
				}
			}
		}
		Vector3 playerMovement = new Vector3 (xVel, yVel, 0);
		//Debug.Log ("Separate: " + xVel + yVel);
		//Debug.Log ("Before clamp:" );
		//Debug.Log(playerMovement.ToString("F4"));
		playerMovement = Vector3.ClampMagnitude (playerMovement, playerSpeed);
		//Debug.Log ("After clamp: ");
		//Debug.Log(playerMovement.ToString("F4"));

		if (gameOn) {
			if (gameObject.transform.position.y > groundY && !jumpOn && !onRail) {
				playerMovement.y -= gravity;
				gameObject.transform.position += playerMovement;
			} else {
				gameObject.transform.position += playerMovement;
			}

			gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
			groundY += groundVector;
		}
	}

	void jump() {
		Debug.Log ("Jump");
		anim.SetBool ("jump", true);
		jumpStartTime = Time.time;
		jumpOn = true;
	}
}
	