using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    public Transform startpos;
    public GameObject AngelPadChecker;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("working");

        if (col.gameObject.tag == "Player")
        {


            Animator ar = col.GetComponent<Animator>();
            ar.SetBool("Jump", false);
            ar.SetBool("Air", false);
            ar.SetBool("Land", true);

            //disable our trail renderer;
            AngelPadChecker.GetComponent<AngelPowerUpController>().PlayerDied();


            GetComponent<AudioSource>().Play();
           
			Debug.Log ("Player");
        }
    }

   public void ReturnPlayer(GameObject player)
    {
        player.gameObject.transform.position = startpos.position;
        player.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}



