using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionItem : MonoBehaviour {


    void OnEnable()
    {
        int currentamount = PlayerPrefs.GetInt("PotAmount");
        GetComponentInChildren<Text>().text = "x " + currentamount.ToString();
    }
}
