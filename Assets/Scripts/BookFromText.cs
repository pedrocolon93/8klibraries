using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BookFromText : MonoBehaviour {//not everything has to be a monobehavior.  Better abstraction?
//visual studio and breakpoints??
	//there's a student version.
//lucid chart

//this could be straight on your book object 

	//reads the associated text and returns an array of pages, each of which are strings which exactly fit the page
	StreamReader reader = new StreamReader("Assets/Text/Siddhartha.txt");//you need to figure out how this is talking to the rest of this thing
	public List<string> pages = new List<string>(); //getter method???  also declare and initializer in the right places and use throw/catch statements
	string word = ""; //current word
	string page = ""; //current page --default is protected?
	public const int PAGE_LENGTH = 300; //the number of characters which fit on the page-- you might not want const
	public const int LINE_LENGTH = 35; //the number of characers which fit on the line
	int currentPageChar = 0; //how many characters into the page are you?
	int currentLineChar = 0; //how many characters into the line are you?


	// Use this for initialization
	void Start () {
																	Debug.Log("The start method of BFT was called!");
		while(reader.Peek() > 0){
																	Debug.Log("You got into the while loop");
			char nextChar = (char)reader.Read();
																	Debug.Log("nextChar: " + nextChar);
		// 	if(nextChar == 92 && reader.Peek()==110){//if there's a newline character
		// 		page += word;//you're done with that word
		// 		int remainder = LINE_LENGTH - currentLineChar;//# of characters left in the line
		// 		currentPageChar += remainder;
		// 		currentLineChar = 0;//you're at the 0th positionon the new line
		// 		if(currentPageChar >= PAGE_LENGTH){//are you done with the page?
		// 			pages.Add(page);//this will be a nightmare and you know it
		// 															Debug.Log("This page was added: " + page);
		// 			page = "";
		// 			currentPageChar = 0;
		// 		}
		// 	}
		// 	else if(nextChar == 32){//is there a space?
		// 		//still add the space
		// 		word += nextChar;
		// 		currentLineChar++;

		// 		if(currentLineChar + word.Length> LINE_LENGTH){//if the word doesn't fit on the line
		// 			int remainder = LINE_LENGTH - currentLineChar;//space left in the line
		// 			currentPageChar += remainder;
		// 			currentLineChar = word.Length;
		// 			if(currentPageChar >= PAGE_LENGTH){//are you done with the page?
		// 				pages.Add(page);//this will be a nightmare and you know it
		// 				page = "";
		// 				currentPageChar = 0;
		// 			}
		// 		}
		// 		page += word;
		// 		currentPageChar += word.Length;
		// 		word = "";
		// 	}
		// 	else{//just add a regular char
		// 		word += nextChar;
		// 		currentLineChar++;
		// 	}

		 }
	}
}
