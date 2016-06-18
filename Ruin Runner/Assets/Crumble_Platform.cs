using UnityEngine;
using System.Collections;

public class Crumble_Platform : MonoBehaviour {

 void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GetComponent<Animation>().Play();
            GetComponent<AudioSource>().Play();
            StartCoroutine("CrumbleTimer");
        }
    }
    IEnumerator CrumbleTimer ()
    {
        yield return new WaitForSeconds(.40F);
        GetComponent<Animation>().Stop();
        GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);

    }
}
