using System.Collections;
using System.Collections.Generic;
using System;//if your code is broken, it might be this
using UnityEngine;
using Windows.Kinect;

//Manages position input from the Kinect regarding a single joint

public class DetectJoints : MonoBehaviour {

	public GameObject BodySrcManager;
	private JointType TrackedJoint;
	private BodySourceManager bodyManager;
	private Body[] bodies;
	public float multiplier = 100f; 
	public HandState handBehavior;
	//private Vector3 rot;//rotation

	public bool isLeftHandClosed;
	public bool isRightHandClosed;

	private float posX, posY;

	// Use this for initialization
	void Start () 
	{
		BodySrcManager = GameObject.Find("BodySourceManager");
		if(BodySrcManager == null){
			Debug.Log("Assign Game Object to Body Source Manager");
		}
		else
		{
			bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(bodyManager == null)
		{
			Debug.Log("Body Manager not assigned");
			Debug.Log("Aubrey's super gigantic change which is important");
			return;
		}
		bodies = bodyManager.GetData();

		if(bodies == null)
		{
			return;
		}
		foreach(var body in bodies)
		{
			if (body == null)
			{
				continue;
			}
			if(body.IsTracked)
			{
				var pos = body.Joints[TrackedJoint].Position;
				posX = pos.X;
				posY = pos.Y;
				if(!(TrackedJoint == Windows.Kinect.JointType.HandLeft || TrackedJoint == Windows.Kinect.JointType.HandRight)){//if what we're tracking isn't a hand
					gameObject.transform.localPosition = new Vector3(pos.X * multiplier, pos.Y * multiplier);
				}

				isLeftHandClosed = body.HandLeftState == HandState.Closed;
				isRightHandClosed = body.HandRightState == HandState.Closed;

				//calculate z axis position of hands
				//how to check if it's a hand
			}
		}
		
	}

	public JointType GetTrackedJoint(){
		return TrackedJoint;
	}
	public void SetTrackedJoint(JointType jointName){
		TrackedJoint = jointName;
		Debug.Log("Tracked Joint> " + TrackedJoint);
	}
	public float getPosX(){
		return posX;
	}
	public float getPosY(){
		return posY;
	}
}
