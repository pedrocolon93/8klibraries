using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelfBook : MonoBehaviour {

    //constants to fuss with:
    //book dimenson ranges
    private const float MIN_WIDTH = 4.0f;
    private const float MAX_WIDTH = 8.0f;
    private const float MIN_HEIGHT = 30.0f;
    private const float MAX_HEIGHT = 35.0f;
    private const float DEPTH = 25.0f;
    private const int COLOR_COUNT = 15; //current number of colors to choose from

    private string author, title, objName;
    private float w, h;
    private Material coverColor;

    public string updatedAuthor, updatedTitle;

	// Use this for initialization
	void Start () {
        w = generateWidth();
        h = generateHeight();
        coverColor = generateCover();
        gameObject.transform.localScale = new Vector3(w, h, DEPTH);
        gameObject.GetComponent<Renderer>().material = coverColor;
        gameObject.tag = "closedBook";
    }

    private float generateWidth()
    {
        return Random.Range(MIN_WIDTH, MAX_WIDTH);
    }
    private float generateHeight()
    {
        return Random.Range(MIN_HEIGHT, MAX_HEIGHT);
    }
    private Material generateCover()
    {
        int colorCode = Random.Range(0, COLOR_COUNT);
        Colors colorName = (Colors)colorCode;
        Material colorMat = Resources.Load<Material>("Materials/"+colorName) as Material;
        return colorMat;
    }
    public float getWidth(){
        return w;
    }
    public void setAuthor(string authorName)
    {
        updatedAuthor = authorName;
    }
    public void setTitle(string titleName)
    {
        updatedTitle = titleName;
    }
    public void setName(string objectName)
    {
    	objName = objectName;
    	gameObject.name = objectName;
    }

    private void addTitleText(){
    	GameObject wrapper = GameObject.CreatePrimitive(PrimitiveType.Cube);
    	GameObject text = new GameObject();
 		TextMesh t = text.AddComponent<TextMesh>();
 		t.text = title;
 		t.fontSize = 15;
 		t.transform.parent = wrapper.transform;
 		wrapper.transform.parent = gameObject.transform;
        wrapper.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //wrapper.transform.localEulerAngles = new Vector3(0, 0, 0);
 		//then try to get the position right:
 		//full of hard-coded values to deal with
 		t.transform.localEulerAngles += new Vector3(0, 0, -90);
        //the following calculations are indescribably arbitrary
        t.transform.localPosition += new Vector3(w/3, (h/2)-2.0f, DEPTH/-2);    
    }
    
	private void addAuthorText(){
    	GameObject wrapper = GameObject.CreatePrimitive(PrimitiveType.Cube);
    	GameObject text = new GameObject();
 		TextMesh t = text.AddComponent<TextMesh>();
 		t.text = author;
 		t.fontSize = 7;
 		t.transform.parent = wrapper.transform;
 		wrapper.transform.parent = gameObject.transform;
        wrapper.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
 		//then try to get the position right:
 		//full of hard-coded things to deal with
 		//t.transform.localEulerAngles += new Vector3(0, 0, 0);
        //the following calculations are indescribably arbitrary
 		t.transform.localPosition += new Vector3(w/-3, (h/-2.2f), DEPTH/-2.0f);
    }
	// Update is called once per frame
	void Update () {
        //this is a workaround to let us change the object from shelfFiller, 
        //without putting too much in the update method (~efficiency~)
        if(updatedAuthor!=author){
            author = updatedAuthor;
            addAuthorText();

        }
        if(updatedTitle!=title){
            title= updatedTitle;
            addTitleText();
        }
		
	}


    enum Colors
    {
        RoseGold = 0,
        PrettyStandardGreen = 1,
        AcademicMaroon = 2,
        BlazerBlue = 3,
        DarkOrange = 4,
        Lavender = 5,
        MetallicTeal = 6,
        Black = 7,
        DarkMint = 8,
        Gold = 9,
        Plum = 10,
        PrettyStandardBlue = 11,
        Violet = 12,
        MatteLipstick = 13,
        Mint = 14,
        Orange = 15,
    }

}
