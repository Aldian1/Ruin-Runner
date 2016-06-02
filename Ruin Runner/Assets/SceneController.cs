using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public GameObject[] currentpillars;

    public Transform CurrentPositionOfLastPlatform;

    public int CurrentLevel;

    public int StartingAreaSize;

    public float difficultyMultiplier;

    public GameObject doublepillar,bottompillar;
	// Use this for initialization
	void Start () {
        //Load pillars from resources in future

        //Spawn our starting area of x tiles
     
        for(int i = 0; i < StartingAreaSize; i++)
        {
            //setting the random makes the platforms not concurrent but makes them look slightly spaced. 
            int u = Random.Range(2,6);
            GameObject go = Instantiate(bottompillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
           CurrentPositionOfLastPlatform = go.transform;

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
