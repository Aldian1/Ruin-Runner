using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class ShopController : MonoBehaviour {

    public GameObject MainSectionUI, ShopSection,CoinAmount;

	// Use this for initialization
	void Start () {
	
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
        TweenShop(true);
    }

    public void TweenShop(bool changestate)
    {
        if(changestate)
        {

        }
        if(!changestate)
        {

        }
    }
}
