using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnetItem : MonoBehaviour {

	void OnEnable()
    {
       int currentamount = PlayerPrefs.GetInt("MagnetAmount");
        GetComponentInChildren<Text>().text = "x " + currentamount.ToString();
    }

}
