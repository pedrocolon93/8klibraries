using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;//for list reasons

public class turnPage : MonoBehaviour {//this should p. be a monobehavior



	//page turning animation
	private Animation anim;

	private bool first = true; //are we on the first page?

	private int currentPage;
	private List<string> pages;

	public string text1;
	public string text2;
	public string text3;
	public string text4;
	public Text page1Text;
	public Text page2Text;
	public Text page3Text;
	public Text page4Text;

	//for Read()
	private StreamReader reader;
	string word = ""; //current word
	string page = ""; //current page --default is protected?
	public int PAGE_LENGTH = 350; //the number of characters which fit on the page-- you might not want const
	public int LINE_LENGTH = 35; //the number of characers which fit on the line
	int currentPageChar = 0; //how many characters into the page are you?
	int currentLineChar = 0; //how many characters into the line are you?



	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();

        //read this book
		pages = new List<string>();
        reader = new StreamReader("Assets/Text/"+ bookMeta.title);//you need to figure out how this is talking to the rest of this thing
        Read();

        //- points for style
        currentPage = 0;

        text1 = pages.ElementAt(currentPage);
        currentPage ++;
        text2 = pages.ElementAt(currentPage);
        currentPage ++;
        text3 = pages.ElementAt(currentPage);
        currentPage ++;
        text4 = pages.ElementAt(currentPage);
        currentPage ++;

        page1Text.text = text1;
        page2Text.text = text2;
        page3Text.text = text3;
        page4Text.text = text4;
	}
	
	// Update is called once per frame
	//right now this only flips forwards on page flips
	void Update () {
		if (Input.GetKeyDown("j")){//flip forwards
			if(first == false){
	        	print("j key was pressed");
	        	//make this more abstract later by setting whole page content to a var
	        	text1 = text3;
	        	page1Text.text = text1; //these might not be necessary
	        	text2 = text4;
	        	page2Text.text = text4;
	        	anim.Play(anim.clip.name);

	        	text3 = pages.ElementAt(currentPage);
        		currentPage ++;
                Debug.Log("current page: " + currentPage);

                text4 = pages.ElementAt(currentPage);
                currentPage++;
                Debug.Log("current page: " + currentPage);

	        	page3Text.text = text3;
	        	page4Text.text = text4;
	        }
	        //turning from the first page is special
	        else{
	        	first = false;
	        	anim.Play(anim.clip.name);
	        	Debug.Log("the animation should have played?");
	        }
	    }
        if (Input.GetKeyDown("h")){//take it back now y'all
			if(first == false && currentPage>0){
                print("h key was pressed");
	        	text3 = text1;
	        	page3Text.text = text3; //these might not be necessary
	        	text4 = text2;
	        	page4Text.text = text4;
	        	anim.Play(anim.clip.name);//eventually make this the other animation clip, or play the clip backwards?


                currentPage--;
                text2 = pages.ElementAt(currentPage);
                Debug.Log("current page: " + currentPage);

                currentPage--;
                text1 = pages.ElementAt(currentPage);
                Debug.Log("current page: " + currentPage);

                page1Text.text = text1;
	        	page2Text.text = text2;
	        }
	        //turning from the first page is special
	        else{
	        	Debug.Log("You can't flip backwards.  There are no more pages.");
                if (currentPage <= 0)
                {
                    first = true;//we're back on the first page
                }
	        }
	      }
    }
    	private void Read(){
    		while(reader.Peek() > 0){
																	Debug.Log("You got into the while loop");
			char nextChar = (char)reader.Read();
																	Debug.Log("nextChar: " + nextChar);
			if(nextChar == 92 && reader.Peek()==110){//if there's a newline character
				page += word;//you're done with that word
				int remainder = LINE_LENGTH - currentLineChar;//# of characters left in the line
				currentPageChar += remainder;
				currentLineChar = 0;//you're at the 0th positionon the new line
				if(currentPageChar >= PAGE_LENGTH){//are you done with the page?
					pages.Add(page);//this will be a nightmare and you know it
																	Debug.Log("This page was added: " + page);
					page = "";
					currentPageChar = 0;
				}
			}
			else if(nextChar == 32){//is there a space?
				//still add the space
				word += nextChar;
				currentLineChar++;

				if(currentLineChar + word.Length> LINE_LENGTH){//if the word doesn't fit on the line
					int remainder = LINE_LENGTH - currentLineChar;//space left in the line
					currentPageChar += remainder;
					currentLineChar = word.Length;
					if(currentPageChar >= PAGE_LENGTH){//are you done with the page?
						pages.Add(page);//this will be a nightmare and you know it
						page = "";
						currentPageChar = 0;
					}
				}
				page += word;
				currentPageChar += word.Length;
				word = "";
			}
			else{//just add a regular char
				word += nextChar;
				currentLineChar++;
			}
    	}
	}
}
