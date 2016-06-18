using UnityEngine;
using System.Collections;

public class WaterFollow : MonoBehaviour {

    public GameObject camera_;

    private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
        camera_ = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

       // Debug.Log("running");
        Vector3 p = new Vector3(camera_.transform.position.x, transform.position.y, transform.position.z);

        transform.position = p;
	}
}
