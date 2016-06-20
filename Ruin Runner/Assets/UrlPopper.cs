using UnityEngine;
using System.Collections;

public class UrlPopper : MonoBehaviour {



    public void UrlPop(string URL)
    {
        Application.OpenURL(URL);
    }
}
