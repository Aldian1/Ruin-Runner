using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionItem : MonoBehaviour {


    void OnEnable()
    {
        int currentamount = PlayerPrefs.GetInt("PotionAmount");
        GetComponentInChildren<Text>().text = "x " + currentamount.ToString();
    }
}
