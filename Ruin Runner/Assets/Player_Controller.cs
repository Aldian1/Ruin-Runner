using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log ("Collided");
		Camera.main.GetComponent<SceneController>().clicks = 0;
	}

}
