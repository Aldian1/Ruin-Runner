using UnityEngine;
using System.Collections;

public class CineCamera : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public bool testing;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


        if (testing == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                target.transform.Translate(Vector3.right * Time.deltaTime * 20);
            }
            if (Input.GetKey(KeyCode.A))
            {
                target.transform.Translate(-Vector3.right * Time.deltaTime * 20);
            }
        }
    }
}
