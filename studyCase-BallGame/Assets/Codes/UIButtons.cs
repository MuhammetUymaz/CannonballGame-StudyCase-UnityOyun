using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour {

	[Header("Level")]
	[Tooltip("Which scene the player goes to when he clicks this button")]
	public int buttonLevel;

	[Header("To Set Sound Effect")]
	public AudioClip soundEffect;


	public void clickingButton()
	{
		//Play sound effect and go to level (or quit game if the player presses Exit Game button)
		StartCoroutine (timer (0.3f));


	}

	//For timer
	IEnumerator timer(float time)
	{
		//To play sound effect
		gameObject.AddComponent<AudioSource> (); //Add component
		GetComponent<AudioSource> ().clip = soundEffect; //Set the clip
		GetComponent<AudioSource> ().Play (); //Play it

		//We wait for some second to go to the level
		//Because we would like the sound effect to be played completely
		yield return new WaitForSeconds(time);

		//Go to the level
		UnityEngine.SceneManagement.SceneManager.LoadScene (buttonLevel); //Go to the level
	}




}
