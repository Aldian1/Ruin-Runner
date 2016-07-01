using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    public Transform startpos;
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("working");

          if (col.gameObject.tag == "Player")
          {
          col.gameObject.transform.position = startpos.position;

            Animator ar = col.GetComponent<Animator>();
            ar.SetBool("Jump",false);
            ar.SetBool("Air",false);
            ar.SetBool("Land",true);
  
            
            Camera.main.GetComponent<SceneController>().Dead();
            GetComponent<AudioSource>().Play();
    }

       // GameObject.FindGameObjectWithTag("Stats").GetComponentInParent<MenuController>().ChangeScreen(true); ;
    }

}
