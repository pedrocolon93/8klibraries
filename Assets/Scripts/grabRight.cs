using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


//Keeps track of when the left hand has entered the collider of a book

public class grabRight : MonoBehaviour {
	private Collider held;
	private DetectJoints handR, elbowR;
	public JointType handRight, elbowRight;
	private static bool holding = false;//you only have one right hand

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
		handR = gameObject.AddComponent<DetectJoints>() as DetectJoints;
		handR.SetTrackedJoint(handRight);
		elbowR = elbow.AddComponent<DetectJoints>() as DetectJoints;
		elbowR.SetTrackedJoint(elbowRight);
	}
	
	// Update is called once per frame

	void Update () {
		if(holding == false && transform.childCount>0){
			//Debug.Log("(R)Your hand should be empty, but you're still holding something. Letting go now.");
			//Debug.Log("That child is " + transform.GetChild(0));
			transform.GetChild(0).transform.parent = GameObject.Find("world").transform;
		}

		//calculate reach into the z axis
		float xChunk = (handR.getPosX()-elbowR.getPosX());
		float yChunk = (handR.getPosY()-elbowR.getPosY());
		measuredArm = xChunk * xChunk + yChunk * yChunk;
		//you should technically take the square root of this, but mirroring reality exactly is lame
		reachDepth = ARM_LENGTH * ARM_LENGTH - measuredArm * measuredArm;
		//and update the position
		//only kind of a hardcoded nightmare
		gameObject.transform.localPosition = new Vector3(handR.getPosX() * multiplier, handR.getPosY() * multiplier, (reachDepth * multiplier*100) -40);

		//grab a book
		if(handR.isRightHandClosed || Input.GetKeyDown("p")){
            if (selectBook.getHeldObject() != null)
                if (selectBook.getHeldObject().tag == "closedBook"){
				    selectBook.getHeldObject().transform.SetParent(this.gameObject.transform);
				    holding = true;//we need to check that it worked before we do this
			    }
		}
		//let go
		if((!handR.isRightHandClosed && holding ==true) || (Input.GetKeyDown("l") && holding == true)){
			holding = false;
            //if its a hand, keep it on your body
            if (selectBook.getHeldObject() != null)

                if (selectBook.getHeldObject().tag == "hand"){
				    selectBook.getHeldObject().transform.parent = GameObject.Find("playerBody").transform;
			    }
			else{//this part isn't working                                                                                   !
				selectBook.getHeldObject().transform.parent = GameObject.Find("world").transform;
			}
		}
	}
	
		//are you holding something right now?
	void OnTriggerEnter(Collider other){
		if(other.transform.root.gameObject.tag == "hand" && selectBook.tooManyBooks == false){
			Debug.Log("Hands are touching");
			selectBook.handsAreTouching = true;
		}
		if(holding == false){//if we aren't already holding something
			//Debug.Log("You touched " + other.gameObject);
			if (other.gameObject.tag == "closedBook"){
				held = other;
				selectBook.setHeldObject(other.gameObject);
				//Debug.Log("(R)You're holding a thing!\n" + other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.transform.root.gameObject.tag == "hand"){
			//Debug.Log("Hands are touching");
			selectBook.handsAreTouching = false;
		}
		if(holding == false){
			//Debug.Log("(R)You are no longer touching " + other.gameObject);
			selectBook.setHeldObject(GameObject.Find("handR"));
		}
	}
	public static bool isHolding(){
		return holding;
	}
}
