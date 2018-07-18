using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


//Keeps track of when the left hand has entered the collider of a book

public class grabRight : MonoBehaviour {
	private Collider held;
	private DetectJoints handR;
	public JointType handRight;
	private static bool holding = false;//you only have one right hand


	// Use this for initialization
	void Start () {
		held = null;//casual reminder that this might break all of the things and should probably be cleaned up later
		Debug.Log("GrabRight script running");

		//create hand
		handR = gameObject.AddComponent<DetectJoints>() as DetectJoints;
		//JointType handRight = JointType.HandRight;
		handR.SetTrackedJoint(handRight);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("p")){//replace with right hand grab
			Debug.Log("You pressed the p key");
			if(selectBook.getHeldObject().tag == "closedBook"){
				Debug.Log("(R)You should be holding " + selectBook.getHeldObject() + " now");
				selectBook.getHeldObject().transform.SetParent(this.gameObject.transform);
				holding = true;//we need to check that it worked before we do this
			}
		}
	}
	
		//are you holding something right now?
	void OnTriggerEnter(Collider other){
		if(holding == false){//if we aren't already holding something
			Debug.Log("You touched " + other.gameObject);
			if (other.gameObject.tag == "closedBook"){
				held = other;
				selectBook.setHeldObject(other.gameObject);
				Debug.Log("(R)You're holding a thing!\n" + other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(holding == false){
			Debug.Log("(R)You are no longer touching " + other.gameObject);
			selectBook.setHeldObject(GameObject.Find("handR"));
		}
	}
	public static bool isHolding(){
		return holding;
	}
}
