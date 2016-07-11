using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

    private GameObject Player;

    private Player_Controller PC;

    //coin smoothdamps

    public float smoothTime = 0.3F;
    private float yVelocity = 0.0F;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PC = Player.GetComponent<Player_Controller>();
    }
   
    void Update()
    {
        if(PC.MagnetOn)
        {
            if(Vector2.Distance(this.transform.position, Player.transform.position) < 2)
            {
				transform.position = Vector3.Lerp (transform.position, Player.transform.position, .1F);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Camera.main.GetComponent<SceneController>().score_ += 1;
        Destroy(this.gameObject);
    }
}
