using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


//Keeps track of when the left hand has entered the collider of a book

public class grabLeft : MonoBehaviour {
	private Collider held;
	private DetectJoints handL;
	public JointType handLeft;
	private static bool holding = false;//you only have one left hand


	// Use this for initialization
	void Start () {
		held = null;//casual reminder that this might break all of the things and should probably be cleaned up later
		//create hand
		handL = gameObject.AddComponent<DetectJoints>() as DetectJoints;
		//JointType handLeft = JointType.HandLeft;
		handL.SetTrackedJoint(handLeft);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("o")){//replace with right hand grab
			Debug.Log("You pressed the o key");
			if(selectBook.getHeldObject().tag == "closedBook"){
				Debug.Log("(L)You should be holding " + selectBook.getHeldObject() + " now");
				selectBook.getHeldObject().transform.SetParent(this.gameObject.transform);
				holding = true;//we need to check that it worked before we do this
			}
		}
	}


	void OnTriggerEnter(Collider other){
		if(other.transform.root.gameObject.tag == "hand" && selectBook.tooManyBooks == false){
			Debug.Log("Hands are touching");
		}
		if(holding == false){
			//Debug.Log("You touched " + other.gameObject);
			if (other.gameObject.tag == "closedBook"){
				held = other;
				selectBook.setHeldObject(other.gameObject);
				//Debug.Log("(L)You're holding a thing! > " + other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.transform.root.gameObject.tag == "hand"){
			Debug.Log("Hands are touching");
			selectBook.handsAreTouching = false;
		}
		if(holding == false){
			//Debug.Log("(L)You are no longer touching " + other.gameObject);
			selectBook.setHeldObject(GameObject.Find("handL"));
		}
	}

	public static bool isHolding(){
		return holding;
	}
}
