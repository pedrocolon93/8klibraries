 using UnityEngine;
 using System.Collections;
 
 public class CamRotation : MonoBehaviour 
 {
     private float x;
     private float y;
     private Vector3 rotateValue;
     private float currentPosition;

     private float currentRotation;

     public float moveSpeed;
     public float rotationSpeed;

     private Rigidbody rb;

     void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
     }
 
     // void Update()
     // {

     //     //forwardsObjecgd
     //     if(Input.GetKey("s")){
     //         rb.AddForce(Vector3.down);
     //     }
     //     //back
     //    if(Input.GetKey("w")){
     //        rb.AddForce(Vector3.up);
     //     }
     //    //left
     //     if(Input.GetKey("a")){
     //        currentRotation += rotationSpeed;
     //        gameObject.transform.eulerAngles = new Vector3(0, currentRotation, 0);
     //     }
     //     //back
     //    if(Input.GetKey("d")){
     //        currentRotation -= rotationSpeed;
     //        gameObject.transform.eulerAngles = new Vector3(0, currentRotation, 0);
     //     }

     // }
         void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
 }
