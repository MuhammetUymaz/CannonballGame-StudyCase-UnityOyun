using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour {


	Rigidbody myRB;
	public ballProperties myProperties;

	void Awake()
	{
		//Get rigidbody
		myRB = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other)
	{
		//Collided Box
		if (other.tag == "box") {

			//Check the box's material
			//Assign myMaterial to its material if they are not in myMaterial

			if (other.GetComponent<MeshRenderer> ().material != myProperties.myMaterial) {
				other.GetComponent<MeshRenderer> ().material = myProperties.myMaterial;
				other.GetComponent<MeshRenderer> ().material.name = myProperties.myMaterialName;
			}

			/*Note: We don't make "rechecking" all boxes continously because of optimization.
			When this ball completes to collide all box which it needs to collide with (we determine its number as boxNumber. The number is
			decreased), then we make
			parent of the boxes rechecked the boxes if they are in correct color or not
			*/

			myProperties.boxNumber--;

			//If there is no boxes which this ball collides, make the parent box rechecked all boxes' materials

			if (myProperties.boxNumber <= 0) 
			{
				myProperties.theParentBoxScript.reCheck = true;
			}

			//Sound Effect
			GetComponent<AudioSource>().Play();

		}

		//Colliding with direction object
		//Note: Direction object (whose tag which is "changingDirection") is child of a normal box. When a ball collides with direction object
		//the ball collides with normal box, as well.
		else if (other.tag == "changingDirection") {

			//Set ball rotation
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);


			float rotationY = transform.rotation.eulerAngles.y ;

			//We can make a condition that restrict equal to 0, 90, 180 or 270 directly. However, there may be decimal such as 90.000001 instead of 90
			//Hence, I wrote interval for each statement
			if (rotationY >= 0 && rotationY < 90) //Go to upward
			{
				myRB.velocity = new Vector3 (0, 0, 1) * myProperties.ballSpeed;

			}
			else if (rotationY >= 90 && rotationY < 180) //Go to right
			{
				myRB.velocity = new Vector3 (1, 0, 0) * myProperties.ballSpeed;

			}
			else if (rotationY >= 180 && rotationY < 270) //Go to downward
			{
				myRB.velocity = new Vector3 (0, 0, -1) * myProperties.ballSpeed;

			}
			else if (rotationY >= 270 && rotationY < 359) //Go to left
			{
				myRB.velocity = new Vector3 (-1, 0, 0) * myProperties.ballSpeed;

			}
				

		}
	}
}
