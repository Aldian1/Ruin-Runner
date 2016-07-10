using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class ShopController : MonoBehaviour
{

    public GameObject MainSectionUI, ShopSection, CoinAmount, ShopCam, MainCamPos;

    public int MagnetUpgradeValue, PotionUpgradeValue;

    public GameObject[] magupgrades, PotionUPgrades;

    public int MagnetUpgradeCost, PotionUpgradeCost;

    public int gold, MagnetBuy, PotionBuy;

    public GameObject MagnetText, magnetupgradetext, PotionBuyText, PotionUpgradeText;
    // Use this for initialization
    void Start()
    {
     

        gold = PlayerPrefs.GetInt("Coins");
        CoinAmount.GetComponent<Text>().text = gold.ToString();

        MagnetBuy = 500;
        MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();
        magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString();

        PotionBuyText.GetComponent<Text>().text = PotionBuy.ToString();
        PotionUpgradeText.GetComponent<Text>().text = PotionUpgradeCost.ToString();

        PlayerPrefs.SetInt("Coins", 10000);


        //load the magnet and potion powers decide if anything needs to be changed
        int i = PlayerPrefs.GetInt("MagPower");
        int u = PlayerPrefs.GetInt("PotionPower");
        Debug.Log(i);
        if (i == 20)
        {
            //weve upgraded once before
            magupgrades[0].SetActive(true);
            magupgrades[1].SetActive(true);
            MagnetUpgradeCost = 5000;
            MagnetBuy = 300;
            MagnetUpgradeValue = 2;
            magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString();
            MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();

        }



        if (i == 30)
        {
            //weve upgraded once before
            magupgrades[0].SetActive(true);
            magupgrades[1].SetActive(true);
            magupgrades[2].SetActive(true);
            MagnetUpgradeValue = 3;
            MagnetBuy = 100;
            magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString();
            MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();
            magnetupgradetext.GetComponent<Text>().text = "MAX";

        }

        if (i == 5)
        {
            //weve upgraded once before
            magupgrades[0].SetActive(true);
            MagnetUpgradeCost = 2500;
            MagnetUpgradeValue = 1;
            magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString();
          
        }

        if (u == 5)
        {
            //weve upgraded once before
            PotionUPgrades[0].SetActive(true);
            PotionUpgradeCost = 1400;
            PotionBuy = 900;
            PotionUpgradeValue = 1;

        }



        if (u == 10)
        {
            //weve upgraded once before
            PotionUPgrades[0].SetActive(true);
            PotionUPgrades[1].SetActive(true);
            PotionUpgradeCost = 1600;
            PotionUpgradeValue = 2;
            PotionBuy = 1000;

        }

        if (u == 15)
        {
            //weve upgraded once before
            PotionUPgrades[0].SetActive(true);
            PotionUPgrades[1].SetActive(true);
            PotionUPgrades[2].SetActive(true);
            PotionBuy = 1200;
            PotionUpgradeValue = 3;
            PotionUpgradeText.GetComponent<Text>().text = "MAX";
            PotionBuyText.GetComponent<Text>().text = PotionBuy.ToString();
        }
    }



    public void ActivateShop(bool activation)
    {
        if (activation)
        {
            OpenShop();
        }

        if (!activation)
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
        if (changestate)
        {
            Camera.main.GetComponent<CineCamera>().target = ShopCam.transform;
        }
        if (!changestate)
        {
            Camera.main.GetComponent<CineCamera>().target = MainCamPos.transform;
            PlayerPrefs.Save();
        }
    }

    public void BuyAnItem(string ItemName)
    {
  
        #region MagnetUpdate

        if (ItemName == "MagnetUp")
        {
   

            switch (MagnetUpgradeValue)
            {


                // Debug.Log("12214124");
                //we havent upgraded the item yet
                case 0:

                    //if we have enough money
                    if (gold > MagnetUpgradeCost)
                    {

                        magupgrades[0].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 5);
                        MagnetUpgradeCost = 2500;
                        MagnetUpgradeValue = 1;
                        PlayerPrefs.SetInt("Coins", gold - 200);
                        gold = gold - 200;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                        MagnetBuy = 400;

                    }
                    break;
                //weve upgraded our item
                case 1:

                    if (gold > MagnetUpgradeCost)
                    {
                        magupgrades[1].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 20);
                        MagnetUpgradeCost = 5000;
                        MagnetUpgradeValue = 2;
                        PlayerPrefs.SetInt("Coins", gold - 200);
                        gold = gold - 200;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                        MagnetBuy = 300;

                    }


                    break;
                //weve upgrade our item twice
                case 2:
                    if (gold > MagnetUpgradeCost)
                    {
                        magupgrades[2].SetActive(true);
                        PlayerPrefs.SetInt("MagPower", 30);

                        MagnetUpgradeValue = 3;
                        PlayerPrefs.SetInt("Coins", gold - 100);
                        gold = gold - 100;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();

                        MagnetBuy = 100;
                       
                    }

                    break;
                //weve upgraded our item three times
                case 3:


                    break;


            }
            //save out
        
            MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();
            if (MagnetUpgradeValue != 3)
            { magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString(); }
            else { magnetupgradetext.GetComponent<Text>().text = "MAX"; }

        }
        #endregion
     
        #region PotionUpdate

        if (ItemName == "PotionUp")
        {
          

            switch (PotionUpgradeValue)
            {


                // Debug.Log("12214124");
                //we havent upgraded the item yet
                case 0:

                    //if we have enough money
                    if (gold > PotionUpgradeCost)
                    {

                        PotionUPgrades[0].SetActive(true);
                        PlayerPrefs.SetInt("PotionPower", 5);
                        PotionUpgradeCost = 1400;
                        PotionBuy = 900;
                        PotionUpgradeValue = 1;
                        PlayerPrefs.SetInt("Coins", gold - 1400);
                        gold = gold - 1400;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                      

                    }
                    break;
                //weve upgraded our item
                case 1:

                    if (gold > PotionUpgradeCost)
                    {
                        PotionUPgrades[1].SetActive(true);
                        PlayerPrefs.SetInt("PotionPower", 10);
                        PotionUpgradeCost = 1600;
                        PotionUpgradeValue = 2;
                        PotionBuy = 1000;
                        PlayerPrefs.SetInt("Coins", gold - PotionUpgradeCost);
                        gold = gold - PotionUpgradeCost;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();
                      

                    }


                    break;
                //weve upgrade our item twice
                case 2:
                    if (gold > PotionUpgradeCost)
                    {
                        PotionUPgrades[2].SetActive(true);
                        PlayerPrefs.SetInt("PotionPower", 15);
                        PotionBuy = 1200;
                        PotionUpgradeValue = 3;
                        PlayerPrefs.SetInt("Coins", gold - PotionUpgradeCost);
                        gold = gold - PotionUpgradeCost;
                        CoinAmount.GetComponent<Text>().text = gold.ToString();

                     

                    }

                    break;
                //weve upgraded our item three times
                case 3:


                    break;


            }
            //save out
           
            MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();
            PotionBuyText.GetComponent<Text>().text = PotionBuy.ToString();

            if (MagnetUpgradeValue != 3)
            { magnetupgradetext.GetComponent<Text>().text = MagnetUpgradeCost.ToString(); }
            else { magnetupgradetext.GetComponent<Text>().text = "MAX"; }

            if (PotionUpgradeValue != 3)
            { PotionUpgradeText.GetComponent<Text>().text = PotionUpgradeCost.ToString(); }
            else { PotionUpgradeText.GetComponent<Text>().text = "MAX"; }

        }
        #endregion

        #region magnetbuy
        if (ItemName == "MagnetBuy")
        {


            if (gold > MagnetBuy)
            {

                PlayerPrefs.SetInt("Coins", gold - MagnetBuy);
                PlayerPrefs.SetInt("MagnetAmount", PlayerPrefs.GetInt("MagnetAmount") + 1);
                gold = gold - MagnetBuy;
            }


            MagnetText.GetComponent<Text>().text = MagnetBuy.ToString();
            CoinAmount.GetComponent<Text>().text = gold.ToString();

            #endregion
          

        }
            #region PotionBuy
            if (ItemName == "PotionBuy")
            {
                
                if (gold > PotionBuy)
                {

                    PlayerPrefs.SetInt("Coins", gold - PotionBuy);
                    PlayerPrefs.SetInt("PotAmount", PlayerPrefs.GetInt("PotAmount") + 1);
                    gold = gold - PotionBuy;
                }

                PotionBuyText.GetComponent<Text>().text = PotionBuy.ToString();
                CoinAmount.GetComponent<Text>().text = gold.ToString();

                #endregion
            }

          if(ItemName == "AngelBuy")
            {
            if (gold > 2000)
            {

                PlayerPrefs.SetInt("Coins", gold - 2000);
                PlayerPrefs.SetInt("AngelAmount", PlayerPrefs.GetInt("AngelAmount") + 1);
                gold = gold - 2000;
            }           
            CoinAmount.GetComponent<Text>().text = gold.ToString();
        }
        }



    }

