using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    public GameObject stats, menuoverlay,overlaybutton,GameoverCanvas;
    public Image gameoverimage;
    public GameObject camera_;
    public GameObject particles;
    public bool change_;
	// Use this for initialization
	void Start () {
        stats = GameObject.FindGameObjectWithTag("Stats");
        stats.SetActive(false);
        menuoverlay = GameObject.FindGameObjectWithTag("OverLayImage");
        camera_ = Camera.main.gameObject;
        overlaybutton = GameObject.Find("Button");
     
	}

    public void MoveOn()
    {
        //fade representive screens out
        menuoverlay.GetComponent<Image>().CrossFadeAlpha(0, .4F, true);
        stats.GetComponent<Image>().CrossFadeAlpha(1, .2F, true);

        //enable our scripts and deactivate particles/overlay
        camera_.GetComponent<SceneController>().enabled = true;
        camera_.GetComponent<CineCamera>().enabled = true;
        particles.SetActive(false);
        overlaybutton.SetActive(false);

        //turn our stats on
        stats.SetActive(true);

        gameoverimage = GameoverCanvas.GetComponent<Image>();


    }



    public void ChangeScreen(bool change)
    {
       
        if(change)
        {
            Debug.Log("yh");
            GameoverCanvas.SetActive(true);
            change_ = true;
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            return;
        }
        if(!change)
        {
 
            return;
        }
        
    }

}
