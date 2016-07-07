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
                float newPositionx = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref yVelocity, smoothTime);
                //float newPositiony = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref yVelocity, smoothTime);
                transform.position = new Vector3(newPositionx, transform.position.y, transform.position.z);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Camera.main.GetComponent<SceneController>().score_ += 1;
        Destroy(this.gameObject);
    }
}
