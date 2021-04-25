using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//We would like to assign below class to ball which player (cannon) makes
public class ballProperties
{
	public int boxNumber; //Max box number that this cannon's ball can shoot
	public parentBoxScript theParentBoxScript; //We keep all boxes in a one parent. Get this script to make box parent rechecked its boxes to pass level
	public Material myMaterial; //Color of ball
	public string myMaterialName; //Material name to compare what boxes color is (?)
	public int ballSpeed;
}

public class cannonScript : MonoBehaviour {

	//To set where cannon shoots to (this struct just provides us direction)
	public enum shootingDirection{
		Up,
		Down,
		Right,
		Left
	}

	[Header("About Shooting Ball")]
	//Sample ball as a prefab to instantinate new balls
	public GameObject originalBall;

	public Transform muzzle; //Where the ball is instantiated
	public GameObject shootingFX; //The FX which is made when we shoot
	public AudioClip shootingSound; //The sound which is made when we shoot
	public shootingDirection myShootingDirection; //To set direction of shooting ball
	Vector3 ballMovementVector; //Where this cannon's ball goes to

	[Space(20)]
	[Header("To Set New Ball's Properties")]
	public ballProperties theBallProperties;

	void Awake()
	{

		//Set where the ball which this cannon will make will go to
		switch (myShootingDirection) {

		case shootingDirection.Up:
			ballMovementVector = new Vector3 (0, 0, 1);
			break;

		case shootingDirection.Down:
			ballMovementVector = new Vector3 (0, 0, -1);
			break;

		case shootingDirection.Right:
			ballMovementVector = new Vector3 (1, 0, 0);
			break;

		case shootingDirection.Left:
			ballMovementVector = new Vector3 (-1, 0, 0);
			break;
		}
	}


	void OnMouseDown()
	{
		//Make a new ball

		//FX
		GameObject fx = Instantiate(shootingFX);
		fx.transform.position = muzzle.position;
		Destroy (fx, 1);

		//Sound Effect
		/*If we make a only one gameobject for AudioSource, it will be very good for saving memory. However, if the player wants to shoot
		over and over countinously, next shooting sound effect stops before one even if before one doesn't play completely.
		It will not be nice. Hence, I make an object for each shooting
		*/
		GameObject soundEffect = new GameObject();
		soundEffect.AddComponent<AudioSource> ();
		soundEffect.GetComponent<AudioSource> ().clip = shootingSound;
		soundEffect.GetComponent<AudioSource> ().Play ();
		Destroy (soundEffect, 1);

		//Vibrate the telephone
		Handheld.Vibrate();

		//Newball
		GameObject newBall = Instantiate(originalBall, muzzle);
		newBall.transform.parent = null;
		//Edit properties of new ball
		newBall.GetComponent<ballScript> ().myProperties = theBallProperties;

		//Change ball's own material (color)
		newBall.GetComponent<MeshRenderer>().material = theBallProperties.myMaterial;
		//Change ball's trail renderer's material (color)
		newBall.GetComponent<TrailRenderer>().material = theBallProperties.myMaterial;

		//Shoot the ball
		newBall.GetComponent<Rigidbody> ().velocity = ballMovementVector * theBallProperties.ballSpeed;

		//Destroy the ball 2 seconds later
		Destroy(newBall, 2);

	}


}
