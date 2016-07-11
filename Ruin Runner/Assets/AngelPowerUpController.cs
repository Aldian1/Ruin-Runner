using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngelPowerUpController : MonoBehaviour
{

    GameObject player;
    private Animation ar;
    public GameObject fillbar, AngelPrompt,deadzone;
    private Image im;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
     
        im = fillbar.GetComponent<Image>();
        ar = GetComponentInChildren<Animation>();
    }
    //check the player has any angelpad powerups after death, if not then run as normal
    public void PlayerDied()
    {
        if (PlayerPrefs.GetInt("AngelAmount") > 0)
        {
            AngelPadPrompt();
            player.GetComponent<Player_Controller>().enabled = false;
			player.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else
        {
            PlayerIsDead();
			player.GetComponent<Rigidbody2D>().isKinematic = false;

        }
    }


    void PlayerIsDead()
    {
        //do death sequence as normal
        player.transform.GetChild(2).GetComponent<TrailRenderer>().enabled = false;
        player.GetComponent<Player_Controller>().Reset();
        deadzone.GetComponent<DeadZone>().ReturnPlayer(player);
        Camera.main.GetComponent<SceneController>().Dead();
        AngelPrompt.SetActive(false);
        im.fillAmount = 1;
    }

    public void AngelPadPrompt()
    {
        //prompt the player with UI
        AngelPrompt.SetActive(true);
        StartCoroutine("PromptTimer");
    }

    IEnumerator PromptTimer()
    {

        while (im.fillAmount > 0)
        {
            im.fillAmount -=.04F;
            yield return new WaitForSeconds(.2F);

        }

        //button wasnt clicked so we finish 
        PlayerIsDead();
    }


    public void AngelUse()
    {
        //player has pressed the button to use the angelpad
		PlayerPrefs.SetInt("AngelAmount",PlayerPrefs.GetInt("AngelAmount") - 1);

        StopAllCoroutines();
        //start playermovement
        player.GetComponent<Player_Controller>().enabled = true;
        //move the angel pad to player and activate
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - .2F, -4.5F);
        im.fillAmount = 1;
        AngelPrompt.SetActive(false);
        ar.Play();

        player.GetComponentInChildren<Rigidbody2D>().isKinematic = false;



    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Pillar")
            {
            Destroy(col.gameObject);
        }
    }
}
