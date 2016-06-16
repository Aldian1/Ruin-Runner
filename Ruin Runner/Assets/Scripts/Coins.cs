using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {




    void OnTriggerEnter2D(Collider2D collision)
    {

        Camera.main.GetComponent<SceneController>().score_ += 1;
        Destroy(this.gameObject);
    }
}
