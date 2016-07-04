using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class ShopController : MonoBehaviour {

    public GameObject MainSectionUI, ShopSection,CoinAmount,ShopCam;

    public Transform oldcampos;

    // Use this for initialization
    void Start () {
        oldcampos = Camera.main.GetComponent<CineCamera>().target.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void ActivateShop(bool activation)
    {
        if(activation)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        MainSectionUI.SetActive(false);
        ShopSection.SetActive(true);
        CoinAmount.GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
        TweenShop(true);
    }

    public void TweenShop(bool changestate)
    {
        if(changestate)
        {
            Camera.main.GetComponent<CineCamera>().target = ShopCam.transform;
        }
        if(!changestate)
        {

        }
    }
}
