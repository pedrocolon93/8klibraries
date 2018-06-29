using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectBook : MonoBehaviour {
	private GameObject heldObject, book;
	Collider held;

	// Use this for initialization
	void Start () {
		//held = null;//you could hack this by setting it to an empty game object if you're not allowed to do this, and them checking if what you're holding is that empty object
		//for now, this is hardcoded
		heldObject = GameObject.Find("closedBook");
		held = heldObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){
			Debug.Log("You pressed the space bar!");
			if(!held.Equals(null)){//should you be using == here instead???
				Destroy(heldObject);//does this set things to null or do you now have a weird broken ref?
				book = Instantiate(Resources.Load("Book")) as GameObject;
			}
		}
		
	}

	// //are you holding something right now?
	// //you can't test this until you know how hands work.
	// void OnTriggerEnter(Collider other){
	// 	held = other.gameObject;
	// 	Debug.log("You're holding a thing!\n" + held);
	// }

	// void OnTriggerExit(Collider other){
	// 	held = null;
	// }


}
