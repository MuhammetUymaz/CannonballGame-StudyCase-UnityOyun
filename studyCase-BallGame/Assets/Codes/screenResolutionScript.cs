using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenResolutionScript : MonoBehaviour {

	//Screen properties
	float width, height, deep;

	void Awake () {

		Screen.SetResolution (268, 476, false); //Set the screen size

		//SCREEN SETTıNG
		float newWidth = Screen.width * 1f;
		float newHeight = Screen.height * 1f;

		float basicRatio = 16f/9f;

		float currentRatio = (newHeight)  / (newWidth);
		float ratioOfCurrent_Basic = 1f; ///Initial value to be used

		ratioOfCurrent_Basic = currentRatio / basicRatio;

		if (ratioOfCurrent_Basic > 0 && ratioOfCurrent_Basic <= 1) {

			width = 1f;
			height = 1 * ratioOfCurrent_Basic;


			deep = width * height; //I dont know why it is run

		} else if (ratioOfCurrent_Basic > 1) {


			width = 1;
			height = 1f / ratioOfCurrent_Basic;


			deep = width * height; //I dont know why it is run
		}

		//Set the scale of the camera
		//Everything in the hiearchy is child of the camera. When we change scale of the camera, their scales will be changed, too.
		gameObject.GetComponent<Transform>().localScale = new Vector3(width, height, deep);
	}
}
