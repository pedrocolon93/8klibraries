using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class shelfFiller : MonoBehaviour {

	//start here for each shelf--should this be a vector3?? 
	public const float startYPosition = -250; 
	public const float startXPosition = -100;

	private const float SHELF_SPACING = 100;//height of each shelf 
	private const float SHELF_WIDTH = 50;
	private const int SHELF_NUM = 5;//number of shelves per shelf

	private GameObject[] shelves;

	// Use this for initialization
	void Start () {
		shelves = GameObject.FindGameObjectsWithTag("shelf");
		GameObject currentShelf = shelves[0];
		Debug.Log("currentShelf: "+ currentShelf);
		string path = Application.dataPath + "/Text";
	    string[] filePaths = Directory.GetFiles(@path, "*.txt");
	    foreach (string file in filePaths)
	    {
	     	//create the new book, assign it's details
	        int startNamePosition = file.LastIndexOf('/');//where does the name of the text file start?
	        startNamePosition += 6; //deal with the fact that it likes to include "/Text\"
	        string fileName = file.Substring(startNamePosition);
	        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
	        shelfBook currentBook = cube.AddComponent<shelfBook>() as shelfBook;
	      	currentBook.setName(fileName);
	      	string[] nameParts = fileName.Split('-');
	      	currentBook.setAuthor(nameParts[0]);
	      	currentBook.setTitle(nameParts[1]);

	      	//set the position correctly
	      	//this is the part that isn't working
	      	//currentBook.transform.SetParent(currentShelf.transform);
	      	currentBook.transform.parent = currentShelf.transform;
	      	Debug.Log("currentBook parent: "+ currentBook.transform.parent);
	      	currentBook.transform.position = new Vector3(0.0f, 0.0f, 0.0f);//use to set position later
	      	Debug.Log(currentBook.transform.position);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
