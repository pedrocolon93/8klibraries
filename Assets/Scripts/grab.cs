using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


//Keeps track of when the left hand has entered the collider of a book

public class grab : MonoBehaviour {
	private Collider held;




	// Use this for initialization
	void Start () {
		held = null;//casual reminder that this might break all of the things and should probably be cleaned up later
		Debug.Log("Grab script running");

		//create hand
		DetectJoints handL = gameObject.AddComponent<DetectJoints>() as DetectJoints;
		JointType handLeft = JointType.HandLeft;
		handL.SetTrackedJoint(handLeft);
	}
	
	// Update is called once per frame
	void Update () {
	}
		//are you holding something right now?
	//you can't test this until you know how hands work.
	//check that this is a book or else it breaks things
	void OnTriggerEnter(Collider other){
		Debug.Log("You touched " + other.gameObject);
		if (other.gameObject.tag == "closedBook"){
			held = other;
			selectBook.setHeldObject(other.gameObject);
			Debug.Log("You're holding a thing!\n" + other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		Debug.Log("You are no longer touching " + other.gameObject);
		if(other == held){
			selectBook.setHeldObject(null);
		}
	}
}
