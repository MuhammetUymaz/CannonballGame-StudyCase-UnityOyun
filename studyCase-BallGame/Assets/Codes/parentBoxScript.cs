using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentBoxScript : MonoBehaviour {


	public GameObject winningGamePanel; //To show this panel when the player wins the game

	public Material[] theMaterials; //All material types

	[Header("About Boxes")]
	//We must assign/set below two variables repectively in UnitEditor to recheck boxes respectively
	public MeshRenderer[] myBoxes; //All boxes which this parent has
	public string[] eachBoxMaterialName; //Each box's material name

	int boxOrderToCheck = 0; //To check all boxes orderly

	//To check boxes to pass game
	[HideInInspector] public bool checkAllBoxes = false, reCheck = false;

	bool gameIsWinned = false; //When we win the game, we will not need to check the boxes for the level which the player wins on

	int materialOrder; 

	void Update () {

		//Re-Check boxes
		//When we shoot a ball and the ball is finishing colliding its all boxes (with number), the player may press another cannon to shoot.
		//So we need to check all boxes again
		if (reCheck && !gameIsWinned) {
			
			//Zeroing the values
			checkAllBoxes = true;
			boxOrderToCheck = 0;

			reCheck = false; //Stop for it runs over and over

		}


		//Check all boxes
		if (checkAllBoxes) {
			
			//Get material order of a boxes (we check respectively by using boxOrderToCheck)
			switch (eachBoxMaterialName [boxOrderToCheck]) {

			case "red":
				materialOrder = 0;
				break;

			case "green":
				materialOrder = 1;
				break;

			case "blue":
				materialOrder = 2;
				break;

			case "yellow":
				materialOrder = 3;
				break;

			case "orange":
				materialOrder = 4;
				break;

			}

			//If box's current material is same with material which the box must be in, the player paint box correctly

			if (myBoxes [boxOrderToCheck].material.name == theMaterials [materialOrder].name) {
				

				boxOrderToCheck++; //For next box's material

				//If we check all material, it means player wins game. 
				if (boxOrderToCheck == myBoxes.Length)
				{

					//Zeroing
					checkAllBoxes = false;
					boxOrderToCheck = 0;

					//Winning the game
					Debug.Log("The game is finished!");
					winningGamePanel.SetActive (true);
					GetComponent<AudioSource> ().Play (); //Play the winning game sound effect
					gameIsWinned = true; //We will not be made checked all boxes for the level which player has winned on
				}

			} 
			else //The box isn't in correct material (correct color). Hence, cancel to check
			{ 

				//Zeroing
				checkAllBoxes = false;
				boxOrderToCheck = 0;
			}

		}




	}
}
