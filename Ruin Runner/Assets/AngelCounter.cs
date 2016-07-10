using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AngelCounter : MonoBehaviour {

    void Start()
    {


        GetComponent<Text>().text = PlayerPrefs.GetInt("AngelAmount").ToString();
    }
    
}
