using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    public GameObject stats, menuoverlay,overlaybutton,GameoverCanvas;
    public Image gameoverimage;
    public GameObject camera_;
    public GameObject particles;
    public bool change_;

    GameObject player;
 
    public Player_Controller pl;

    private AudioSource audio;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        stats = GameObject.FindGameObjectWithTag("Stats");
        stats.SetActive(false);
        menuoverlay = GameObject.FindGameObjectWithTag("OverLayImage");
        camera_ = Camera.main.gameObject;
        overlaybutton = GameObject.Find("Button");
        audio = GetComponent<AudioSource>();
       
        
     
	}

    public void MoveOn()
    {
        //fade representive screens out
        menuoverlay.GetComponent<Image>().CrossFadeAlpha(0, .4F, true);
        stats.GetComponent<Image>().CrossFadeAlpha(1, .2F, true);

        //enable our scripts and deactivate particles/overlay
        camera_.GetComponent<SceneController>().enabled = true;
        camera_.GetComponent<SceneController>().SpawnWorld();
        camera_.GetComponent<CineCamera>().enabled = true;
        particles.SetActive(false);
        overlaybutton.SetActive(false);
        pl.enabled = true;

        //turn our stats on
        stats.SetActive(true);

        gameoverimage = GameoverCanvas.GetComponent<Image>();

        audio.Play();


    }



    public void ChangeScreen(bool change)
    {
       
        if(change)
        {
            //  Debug.Log("yh");
            player.SetActive(false);
            GameoverCanvas.SetActive(true);
            change_ = true;
          
            return;
        }
        if(!change)
        {
            GameoverCanvas.SetActive(false);
            change_ = false;
            Camera.main.GetComponent<SceneController>().Reset();
           // player.SetActive(true);
          
            return;
        }
        
    }

}
