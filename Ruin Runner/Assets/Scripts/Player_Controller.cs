using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public Animator ar;
    public AudioSource[] as_;
    public int switcher;
    public bool onground;

    void Start()
    {
        ar = GetComponent<Animator>();

    }

	void OnCollisionEnter2D(Collision2D col)
	{

        Debug.Log(col.gameObject.tag);
        //Debug.Log ("Collided");
        if (this.enabled == true)
        {
        ar.SetBool("Land", true);
        ar.SetBool("Air", false);
            if(col.gameObject.tag == "Slippy")
            {
                as_[1].Play();
                
            }
            else
            {
                as_[0].Play();
            }
     
        }
        Camera.main.GetComponent<SceneController>().clicks = 0;
   
        switcher = 0;

        onground = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        onground = false;
    }


    void Update()
    {
        if (onground)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && switcher < 2)
            {
                ar.SetBool("Land", false);
                ar.SetBool("Jump", true);
                switcher++;
                return;
            }
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
