using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines if a book should be opened, and handles opening it
//By asimonson

public class selectBook : MonoBehaviour {
	private static GameObject heldObject;
	private GameObject book;
	public GameObject openBook;
	private static Collider held;
	private static string title = "no book selected";
	public static bool handsAreTouching = false;
	public static bool tooManyBooks = false; //do you have space on the screen for opening more books?
	// Use this for initialization
	void Start () {
		Debug.Log("Select Book script running.");
		heldObject = GameObject.Find("Sphere");
		held = heldObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
			if (Input.GetKeyDown("space") || handsAreTouching == true){
				Debug.Log("You pressed the space bar!");
				if(grabLeft.isHolding() || grabRight.isHolding()){//you'll need to sort this out more when which hand is grabbing matters
					if(heldObject.tag == "closedBook"){
						title = heldObject.name;
						Debug.Log("the title of this book is " + title);
						Destroy(heldObject);//does this set things to null or do you now have a weird broken ref?
						book = Instantiate(openBook);

						//position opened book relative to the camera
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-35, -25, 100);
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
						tooManyBooks = true;
						
						Debug.Log("The position of the book is: "+ book.transform.position + "and the script is actually updating");
						heldObject = GameObject.Find("Sphere");
						held = GameObject.Find("Sphere").GetComponent<Collider>();
						title = "no book selected";
					}
				}
			} 
	}
	public static string GetTitle(){
		return title;
	}

	public static void setHeldObject(GameObject grabbed){
		heldObject = grabbed;
		held = heldObject.GetComponent<Collider>();
		Debug.Log("heldObject set to : " + heldObject);

	}

	public static GameObject getHeldObject(){
		return heldObject;
	}
}
