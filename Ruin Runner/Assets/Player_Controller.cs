using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public GameObject BarHolder, PowerBar,directional;

    public Image img;

    public float currentpower;

    public int jumpingstage;

    private Animator animbar;
	// Use this for initialization
	void Start () {
        img = BarHolder.GetComponent<Image>();
        animbar = BarHolder.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (jumpingstage == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PowerBar.SetActive(true);
                jumpingstage = 1;
            }
        }
        if (jumpingstage == 1)
        {
            InvokeRepeating("Stage1", .5F, .003F);
            animbar.Play(0);
            jumpingstage = 2;
            directional.SetActive(false);
            return;
        }

        
    }

    public void Stage1()
    {
        Debug.Log("1312");
       

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (jumpingstage == 2)
            {
                InvokeRepeating("Stage2", .5F, .03F);
                jumpingstage = 3;
                directional.SetActive(true);
                directional.GetComponent<Animation>().Play();
                animbar.Stop();
                CancelInvoke("Stage1");
                return;
            }

            

        }

        
    }

    public void Stage2()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
    }

}
