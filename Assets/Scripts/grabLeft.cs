using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


//Keeps track of when the left hand has entered the collider of a book

public class grabLeft : MonoBehaviour {
	private Collider held;
	private DetectJoints handL, elbowL;
	public JointType handLeft, elbowLeft;
	private static bool holding = false;//you only have one left hand

	public float multiplier = 100f; //for hand position

	//calculate reach into the z axis
	public GameObject elbow, realHand;
	public float ARM_LENGTH;
	private float measuredArm;
	private float reachDepth;


	// Use this for initialization
	void Start () {
		held = null;
		//create arm
		handL = gameObject.AddComponent<DetectJoints>() as DetectJoints;
		handL.SetTrackedJoint(handLeft);
		elbowL = elbow.AddComponent<DetectJoints>() as DetectJoints;
		elbowL.SetTrackedJoint(elbowLeft);
	}
	
	// Update is called once per frame
	void Update () {
		if(holding == false && transform.childCount>0){
			//Debug.Log("(L)Your hand should be empty, but you're still holding something. Letting go now.");
			//Debug.Log("That child is " + transform.GetChild(0));
			transform.GetChild(0).transform.parent = GameObject.Find("world").transform;
		}
		//calculate reach into the z axis
		float xChunk = (handL.getPosX()-elbowL.getPosX());
		float yChunk = (handL.getPosY()-elbowL.getPosY());
		measuredArm = xChunk * xChunk + yChunk * yChunk;
		//you should technically take the square root of this, but mirroring reality exactly is lame
		reachDepth = ARM_LENGTH * ARM_LENGTH - measuredArm * measuredArm;
		//and update the position
		//only kind of a hardcoded nightmare
		gameObject.transform.localPosition = new Vector3(handL.getPosX() * multiplier, handL.getPosY() * multiplier, (reachDepth * multiplier*100) -40);
		
		//grab book
		if(handL.isLeftHandClosed || Input.GetKeyDown("o")){
			if(selectBook.getHeldObject().tag == "closedBook"){
				selectBook.getHeldObject().transform.SetParent(this.gameObject.transform);
				holding = true;
			}
		}
		//let go
		if((!handL.isLeftHandClosed && holding ==true) || (Input.GetKeyDown("o") && holding == true)){
			holding = false;
			//if its a hand, keep it on your body
			if(selectBook.getHeldObject().tag == "hand"){
				selectBook.getHeldObject().transform.parent = GameObject.Find("playerBody").transform;
			}
			else{//this part isn't working                                                                                   !
				selectBook.getHeldObject().transform.parent = GameObject.Find("world").transform;
			}
		}
	}


	void OnTriggerEnter(Collider other){
		if(other.transform.root.gameObject.tag == "hand" && selectBook.tooManyBooks == false){
			Debug.Log("Hands are touching");
			selectBook.handsAreTouching = true;
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
			//Debug.Log("Hands are touching");
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
