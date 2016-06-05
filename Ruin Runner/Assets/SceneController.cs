﻿using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public GameObject[] currentpillars;

    public Sprite[] foilagesrites;

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
            int foil = Random.Range(0, 10);

      
            GameObject go = Instantiate(bottompillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
           CurrentPositionOfLastPlatform = go.transform;

            if(foil == 1 || foil == 2)
            {
                go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = foilagesrites[Random.Range(0, foilagesrites.Length)];
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
