using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class ShopController : MonoBehaviour {

    public GameObject MainSectionUI, ShopSection,CoinAmount,ShopCam,MainCamPos;

    public int MagnetUpgradeValue;

    public GameObject[] magupgrades;

    public int MagnetUpgradeCost;

    public int gold,MagnetCost;
    // Use this for initialization
    void Start () {
        gold = PlayerPrefs.GetInt("Coins");

	}



    public void ActivateShop(bool activation)
    {
        if(activation)
        {
            OpenShop();
        }

        if(!activation)
        {
            CloseShop();
        }
    }


    //load any stat values
    public void OpenShop()
    {
        MainSectionUI.SetActive(false);
        ShopSection.SetActive(true);
        CoinAmount.GetComponent<Text>().text = gold.ToString();
        TweenShop(true);
    }

    //update any stat values
    public void CloseShop()
    {
        MainSectionUI.SetActive(true);
        ShopSection.SetActive(false);
        TweenShop(false);
    }


    public void TweenShop(bool changestate)
    {
        if(changestate)
        {
            Camera.main.GetComponent<CineCamera>().target = ShopCam.transform;
        }
        if(!changestate)
        {
            Camera.main.GetComponent<CineCamera>().target = MainCamPos.transform;
        }
    }

    public void BuyAnItem(string ItemName)
    {
        if(ItemName == "Magnet")
        {
            switch (MagnetUpgradeValue)
            {
                //we havent upgraded the item yet
                case 0:

                    //if we have enough money
                    if (gold > MagnetUpgradeCost)
                    {
                        magupgrades[0].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 1);
                        MagnetUpgradeCost = 1500;
                        MagnetUpgradeValue = 1;
                        PlayerPrefs.SetInt("Coins", gold - 500);
                        gold = gold - 200;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                        return;
                    }
                    break;
                //weve upgraded our item
                case 1:

                    if (gold > MagnetUpgradeCost)
                    {
                        magupgrades[1].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 2);
                        MagnetUpgradeCost = 3000;
                        MagnetUpgradeValue = 2;
                        PlayerPrefs.SetInt("Coins", gold - 200);
                        gold = gold  - 200;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                        return;
                    }


                    break;
                //weve upgrade our item twice
                case 2:
                    if (gold > MagnetUpgradeCost)
                    {
                        magupgrades[2].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 3);
                        MagnetUpgradeValue = 3;
                        PlayerPrefs.SetInt("Coins", gold - 100);
                        gold = gold - 100;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                        return;

                    }

                    break;
                //weve upgraded our item three times
                case 3:


                    break;
            }
           
        }
       
    }
}
