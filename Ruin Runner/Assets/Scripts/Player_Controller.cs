using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public Animator ar;
    public AudioSource as_;
    public int switcher;


    void Start()
    {
        as_ = GetComponent<AudioSource>();
        ar = GetComponent<Animator>();
    }

	void OnCollisionEnter2D(Collision2D col)
	{
        //Debug.Log ("Collided");
        if (this.enabled == true)
        {
        ar.SetBool("Land", true);
        ar.SetBool("Air", false);
        as_.Play();
        }
        Camera.main.GetComponent<SceneController>().clicks = 0;
   
        switcher = 0;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && switcher < 2)
        {
            ar.SetBool("Land", false);
            ar.SetBool("Jump", true);
            switcher++;
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ar.SetBool("Land", false);
            ar.SetBool("Jump", false);
            ar.SetBool("Air", true);
            return;

        }
    }
}
