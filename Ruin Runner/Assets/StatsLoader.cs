using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{

    public int coinplus;

    public enum type
    {
        coin = 1,
        distance = 2,
        coingplus = 3,

    };

    public type Type_;
    void Start()
    {
        if (this.tag == "Shop")
        {
            if (Type_ == type.coin)
            {
                int i = PlayerPrefs.GetInt("Coins");
                GetComponent<Text>().text = i.ToString();
            }
        }

    }
    void OnEnable()
    {
        if (Type_ == type.distance)
        {
            int i = PlayerPrefs.GetInt("MaxDistance");
            GetComponent<Text>().text = "Best: " + i.ToString();
        }

        if(Type_ == type.coin)
        {
            int i = PlayerPrefs.GetInt("Coins");
            GetComponent<Text>().text = i.ToString();
        }

        if(Type_ == type.coingplus)
        {
            GetComponent<Text>().text = "+" + coinplus;
        }
        this.enabled = false;
    }
  

}
