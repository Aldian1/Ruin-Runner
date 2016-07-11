using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public List<GameObject> ForeGroundTrees = new List<GameObject>();
    public List<GameObject> MidGroundTrees = new List<GameObject>();
    public List<GameObject> Ruins = new List<GameObject>();

    public GameObject[] PillarTypes;

    public GameObject water;

    public int ChunckSize;

    public List<GameObject> ChunckList = new List<GameObject>();
    public List<GameObject> BGList = new List<GameObject>();




    public Transform CurrentPositionOfLastPlatform, LastForeGround, LastMidGround, LastRuin, CurrentBackground;

    private Transform ORGPlatform, ORGLastFore, ORGLastMid, ORGLastRuin, ORGcurrentBG;


    public GameObject doublepillar, bottompillar, waterplain, backgroundcolor, CrumblePillar, SlippyPillar;

    public GameObject Player;

    public float jumppower, doublejumppower, rightjumppower;

    private Rigidbody2D rb;

    public int clicks = 0;

    private int multiplier;

    private Image bar;

    private Text multitext;
    public float score_;
    public Text score;

    public Transform lastpos;
    private bool firstrun;

    public int difficultymultiplier;

    public GameObject CoinShout;

    public GameObject reset;

    public bool dead;

    private Text distancemeter;

    private int distancefrom;

    private bool movement;

    private float temps;




    // Use this for initialization
    void Start()
    {
        movement = true;
        distancemeter = GameObject.FindGameObjectWithTag("Distance").GetComponent<Text>();

        //setting our originals
        ORGPlatform = CurrentPositionOfLastPlatform;
        ORGLastFore = LastForeGround;
        ORGLastMid = LastMidGround;
        ORGLastRuin = LastRuin;
        ORGcurrentBG = CurrentBackground;

        //loading area
        //   bar = GameObject.FindGameObjectWithTag("MovingBar").GetComponent<Image>();
        rb = Player.GetComponent<Rigidbody2D>();
        // multitext = bar.GetComponentInChildren<Text>();



        firstrun = true;



        //setting the min jumpower incase someone fiddles in the editor with it

        //after everything has been set up we spawn the world for the first time
        SpawnWorld();

    }

    // Update is called once per frame
    void Update()
    {
        //Getting the feetposition
        float p = Vector3.Distance(lastpos.position, Player.transform.position);

        //cant pass Vector3.Distance as an int so we cast it as an int to hard set it
        int i = (int)p;

        //setting the text of the distance object to our int
        distancemeter.text = i.ToString() + "ft";

        //setting distancefrom too i
        distancefrom = i;

        DistanceChecking();

        score.text = score_.ToString();

        //checking for coin streaks
        if (score_ == 100)
        {
            Invoke("CoinShouter", .3F);
            score_ += 1;

        }





    }



    void DistanceChecking()
    {
        if (!dead)
        {

            if (CurrentPositionOfLastPlatform != null)
            {
                //spawning the level ahead of us
                if (Vector2.Distance(this.transform.position, CurrentPositionOfLastPlatform.transform.position) < 5)
                {
                    SpawnWorld();
                }
            }

            if (ChunckList.Count > 0 && ChunckList[0] != null)
            {
                if (Vector2.Distance(this.transform.position, ChunckList[0].transform.position) > 30)
                {
                    Destroy(ChunckList[0].gameObject);
                    ChunckList.Remove(ChunckList[0]);

                }
            }

            if (BGList.Count > 0)
            {
                if (Vector2.Distance(this.transform.position, BGList[0].transform.position) > 60)
                {

                    Destroy(BGList[0].gameObject);
                    BGList.Remove(BGList[0]);
                }
            }



        }
    }


    //called when we click the reset button
    public void ReloadScene()
    {
        // GameObject.FindGameObjectWithTag("Stats").GetComponentInParent<MenuController>().ChangeScreen(false); 
        Player.transform.position = lastpos.position;

        reset.SetActive(false);



        ShoppingIsDone();
    }



    public void ShoppingIsDone()
    {

        score_ = 0;
        dead = false;
        score.gameObject.transform.parent.parent.gameObject.SetActive(true);


        if (GameObject.FindGameObjectWithTag("OverLayImage"))
        {
            GameObject.FindGameObjectWithTag("OverLayImage").SetActive(false);
        }

        //re enabling everything
        movement = true;
        Player.GetComponent<Player_Controller>().enabled = true;
        Player.transform.GetChild(2).GetComponent<TrailRenderer>().enabled = true;
		Player.GetComponent<Rigidbody2D> ().isKinematic = false;


        SpawnWorld();
    }



    public void SpawnWorld()
    {





        //Load pillars from resources in future

        //Spawn our starting area of x tiles

        for (int i = 0; i < ChunckSize; i++)
        {
            //setting the random makes the platforms not concurrent but makes them look slightly spaced. 
            int u = Random.Range(2, 6);
            int foil = Random.Range(0, 10);
            float difficulty = Random.Range(difficultymultiplier, 0);
            //Debug.Log(difficulty);
            //makes sure we spawn ahead of the last pillar with some variance to height and width

            if (difficulty != difficultymultiplier)
            {
               
                GameObject go = Instantiate(PillarTypes[Random.Range(0, PillarTypes.Length)], new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
                //set the last current position
                CurrentPositionOfLastPlatform = go.transform;

                //get the foilage child
                Transform gochild = go.transform.GetChild(3).transform;

                //check if its the weird one or normal reeds
                if (gochild.name == "Foil1")
                {
                    //setting the new vector3
                    Vector3 PO = new Vector3(gochild.transform.position.x, waterplain.transform.position.y - .45F, gochild.transform.position.z);

                    //get the foilage child and set its y value to match the waters
                    go.transform.GetChild(3).transform.position = PO;
                }
                else
                {



                    //setting the new vector3
                    Vector3 PO = new Vector3(gochild.transform.position.x, waterplain.transform.position.y + .55F, gochild.transform.position.z);

                    //get the foilage child and set its y value to match the waters
                    go.transform.GetChild(3).transform.position = PO;
                }
                ChunckList.Add(go);

            }
            else
            {

                //set which type to spawn
                int type = Random.Range(0, 2);

                //  Debug.Log(type);

                if (type == 1)
                {
                    GameObject go = Instantiate(SlippyPillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
                    //set the last current position
                    CurrentPositionOfLastPlatform = go.transform;

                    //adding the chunk objects to a list to be deleted
                    //Debug.Log("1");
                    ChunckList.Add(go);
                }


                if (type == 0)
                {
                    GameObject go = Instantiate(CrumblePillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
                    //set the last current position
                    CurrentPositionOfLastPlatform = go.transform;

                    //adding the chunk objects to a list to be deleted

                    ChunckList.Add(go);
                }



            }





        }

        for (int t = 0; t < 3; t++)
        {



            //spawn ruins - ideal is 18.5
            // int ruinInt = Random.Range(-1, 1);
            Vector3 ruinT = new Vector3(LastRuin.transform.position.x + 20, LastRuin.transform.position.y, LastRuin.transform.position.z);
            GameObject ruin = Instantiate(Ruins[Random.Range(0, Ruins.Count)], ruinT, Quaternion.identity) as GameObject;
            LastRuin = ruin.transform;

            //spawn midground trees - 8

            int MidInt = Random.Range(0, 8);
            Vector3 MidT = new Vector3(LastMidGround.transform.position.x + 15, 6.5F, 8 + MidInt);
            GameObject MidgroundTree = Instantiate(MidGroundTrees[Random.Range(0, MidGroundTrees.Count)], MidT, Quaternion.identity) as GameObject;
            LastMidGround = MidgroundTree.transform;

            //spawn foreground trees - 2.75
            int ForInt = Random.Range(0, 2);
            Vector3 ForT = new Vector3(LastForeGround.transform.position.x + 15, 4.7F, 1F + ForInt);
            GameObject ForGroundTree = Instantiate(ForeGroundTrees[Random.Range(0, ForeGroundTrees.Count)], ForT, Quaternion.identity) as GameObject;
            LastForeGround = ForGroundTree.transform;

            //adding the foilage and chunk objects to a list to be deleted
            BGList.Add(ruin);
            BGList.Add(MidgroundTree);
            BGList.Add(ForGroundTree);


            firstrun = false;

            //checking to see if we are far awary from the background color and if a new one needs to be spawned. If so we spawn a new one. This is called every chunck rather
            //than on update to keep things performance light

            if (Vector2.Distance(this.transform.position, CurrentBackground.position) > 20)
            {
                Vector3 bT = new Vector3(CurrentBackground.position.x + 60, CurrentBackground.position.y, CurrentBackground.position.z);
                GameObject b = Instantiate(backgroundcolor, bT, Quaternion.identity) as GameObject;
                CurrentBackground = b.transform;
                BGList.Add(b);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }


    //called when we die
    public void Dead()
    {
        //turn off control components
        movement = false;
        Player.GetComponent<Player_Controller>().enabled = false;
        Player.GetComponent<Animator>().SetBool("Land", true);
        Player.GetComponent<Rigidbody2D>().isKinematic = false;
        //Reset
        CurrentPositionOfLastPlatform = ORGPlatform;
        LastForeGround = ORGLastFore;
        LastMidGround = ORGLastMid;
        LastRuin = ORGLastRuin;

        //Debug.Log(ORGPlatform);
        CurrentBackground = ORGcurrentBG;
        firstrun = true;
        reset.SetActive(true);
        transform.position = Player.transform.position;
        //destroying leftovers
        foreach (GameObject go in BGList)
        {
            Destroy(go);
        }


        foreach (GameObject t in ChunckList)
        {
            Destroy(t);
        }

        while (ChunckList.Count > 0)
        {
            ChunckList.Remove(ChunckList[0]);
        }

        while (BGList.Count > 0)
        {
            BGList.Remove(BGList[0]);
        }
        dead = true;



        SendMenu();
    }

    //called when a coinstreak is reached
    void CoinShouter()
    {
        CoinShout.SetActive(true);
        CoinShout.GetComponent<TextController>().enabled = true;

    }

    void SendMenu()
    {

		reset.transform.GetChild (0).gameObject.SetActive (true);

        score.gameObject.transform.parent.parent.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("MaxDistance") < distancefrom)
        {
            PlayerPrefs.SetInt("MaxDistance", distancefrom);

        }

        int coin = PlayerPrefs.GetInt("Coins");

        PlayerPrefs.SetInt("Coins", (int)score_ + coin);
        PlayerPrefs.Save();
        reset.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<StatsLoader>().enabled = true;

        GameObject go = GameObject.FindGameObjectWithTag("CoinStat");
        go.GetComponent<StatsLoader>().enabled = true;

        GameObject coinplus = GameObject.FindGameObjectWithTag("CoinPlus");
        coinplus.GetComponent<StatsLoader>().coinplus = (int)score_;
        coinplus.GetComponent<StatsLoader>().enabled = true;

        reset.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = distancefrom.ToString() + "ft";

		Player.GetComponentInChildren<Rigidbody2D>().isKinematic = false;
    }


}
