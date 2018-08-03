using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines if a book should be opened, and handles opening it
//By asimonson

public class selectBook : MonoBehaviour {
	private static GameObject heldObject;
	private GameObject book;
	private GameObject [] openBooks = new GameObject [5];
	public GameObject openBook, playerBody;
	private static Collider held;
	private static string title = "no book selected";
	public static bool handsAreTouching = false;
	public static bool tooManyBooks = false; //do you have space on the screen for opening more books?
	public static Vector3 [] storedBookLocations = new Vector3 [5]; //where to put open book back
	public static GameObject [] putBookBackonthisShelves = new GameObject[5]; 
	public static int numOpenBooks = 0;                                                                                          //!

	// Use this for initialization
	void Start () {
		heldObject = GameObject.Find("Sphere");
        if (heldObject != null)
            held = heldObject.GetComponent<Collider>();
        else
            held = null;
	}
	
	// Update is called once per frame
	void Update () {
		//close book
		if(Input.GetKeyDown("x")){

		    numOpenBooks --;
			Destroy(openBooks[numOpenBooks]);
			tooManyBooks = false;//this will always be true if you've just closed a book
			//reshelve
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		    shelfBook currentBook = cube.AddComponent<shelfBook>() as shelfBook;
		    currentBook.setName(title);
		    currentBook.transform.parent = putBookBackonthisShelves[numOpenBooks].transform;                                                                     //!
		    currentBook.transform.position = storedBookLocations[numOpenBooks];
		    string[] nameParts = title.Split('-');
		    currentBook.setAuthor(nameParts[0]);
		    currentBook.setTitle(nameParts[1]);
		    currentBook.transform.localEulerAngles = new Vector3(0, 285, 0);
		}

		//open book
		if (Input.GetKeyDown("space") && tooManyBooks ==false || handsAreTouching == true && tooManyBooks == false){
			Debug.Log("Opening a book now");
			if(grabLeft.isHolding() || grabRight.isHolding()){//you'll need to sort this out more when which hand is grabbing matters
				if(heldObject.tag == "closedBook"){
					title = heldObject.name;
					Debug.Log("the title of this book is " + title);
					book = Instantiate(openBook);
					openBooks[numOpenBooks] = book.gameObject;//unity doesn't like this???                           <------------------this is the problem right now
					Debug.Log("number of open books: " + numOpenBooks);

					//position opened book relative to the camera
					if(numOpenBooks == 0){
						Debug.Log("You're opening book 0");
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-115, -50, 150);
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 1){
						Debug.Log("You're opening book 1");
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(115, -50, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 2){
						Debug.Log("You're opening book 2");
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-115, 60, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 3){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(115, 60, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 4){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(115, 0, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 5){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-115, 0, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 6){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(50, -50, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 7){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-50, -50, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 8){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(50, 60, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
					}
					else if(numOpenBooks == 9){
						book.transform.SetParent(Camera.main.transform);
						book.transform.localPosition = new Vector3(-50, 60, 150);//these numbers need hard-coded jiggling
						book.transform.localEulerAngles = new Vector3(0, 0, 0);
						tooManyBooks = true;
					}
					Destroy(heldObject);//does this set things to null or do you now have a weird broken ref?
					numOpenBooks ++;
					
					//clear out variables (this is garbage that returns a null error but its fine)
					heldObject = null;
					held = null;
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
	}

	public static GameObject getHeldObject(){
		return heldObject;
	}
}
