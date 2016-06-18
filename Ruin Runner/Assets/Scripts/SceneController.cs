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

    public GameObject water;

    public GameObject foilagesrites;

    public Sprite[] foliagesprites_;

    public int ChunckSize;

    public List<GameObject> ChunckList = new List<GameObject>();
    public List<GameObject> BGList = new List<GameObject>();




    public Transform CurrentPositionOfLastPlatform, LastForeGround, LastMidGround, LastRuin,CurrentBackground;

    private Transform ORGPlatform,ORGLastFore,ORGLastMid,ORGLastRuin,ORGcurrentBG;


    public GameObject doublepillar, bottompillar, waterplain,backgroundcolor,CrumblePillar;

    public GameObject Bow, Player;

    public float jumppower, rightjumppower;

    private Rigidbody2D rb;

    public int clicks = 0;

    private int multiplier;

    private Image bar;

    private Text multitext;
    public float score_;
    public Text score;

    private Transform lastpos;
    private bool firstrun;

    public int difficultymultiplier;

    public GameObject CoinShout;

    // Use this for initialization
    void Start()
    {
        //setting our originals
        ORGPlatform = CurrentPositionOfLastPlatform;
        ORGLastFore = LastForeGround;
        ORGLastMid = LastMidGround;
        ORGLastRuin = LastRuin;
        ORGcurrentBG = CurrentBackground;
      
        //loading area
        bar = GameObject.FindGameObjectWithTag("MovingBar").GetComponent<Image>();
        rb = Player.GetComponent<Rigidbody2D>();
        multitext = bar.GetComponentInChildren<Text>();

        StartCoroutine("MultiTimer");
        lastpos = Player.transform;
        firstrun = true;



        //setting the min jumpower incase someone fiddles in the editor with it


    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentPositionOfLastPlatform != null)
        {
            //spawning the level ahead of us
            if (Vector2.Distance(this.transform.position, CurrentPositionOfLastPlatform.transform.position) < 5)
            {
                SpawnWorld();
            }
        }
        if (ChunckList[0] != null)
        {
            if (Vector2.Distance(this.transform.position, ChunckList[0].transform.position) > 30)
            {
                Destroy(ChunckList[0].gameObject);
                ChunckList.Remove(ChunckList[0]);

            }
        }
        if (BGList[0] != null)
        {
            if (Vector2.Distance(this.transform.position, BGList[0].transform.position) > 60)
            {

                Destroy(BGList[0].gameObject);
                BGList.Remove(BGList[0]);
            }
        }

        score.text = score_.ToString();

        //checking for coin streaks
        if (score_ == 100)
        {
            CoinShout.SetActive(true);
            CoinShout.GetComponent<TextController>().enabled = true;
          
        }

        //jump controller section
        if (Input.GetKeyDown(KeyCode.Mouse0) && clicks < 2)
        {
            rb.AddRelativeForce(Vector2.up * jumppower);
            rb.AddRelativeForce(Vector2.right * rightjumppower);
            clicks += 1;
            multiplier += 1;
            bar.fillAmount = 1;
            multitext.text = multiplier.ToString() + "x";


        }

        //trying the combo system based on clicks first



    }

    IEnumerator MultiTimer()
    {
        while (bar.fillAmount > 0)
        {
            bar.fillAmount -= 0.02F;
            yield return new WaitForSeconds(.05F);
        }

        if (bar.fillAmount <= 0)
        {
            multiplier = 0;
            bar.fillAmount = 1;
            multitext.text = multiplier.ToString() + "x";
            StartCoroutine("MultiTimer");

        }

    }

    public void Reset()
    {
        Player.transform.position = lastpos.position;
        multiplier = 1;
        bar.fillAmount = 1;
        multitext.text = multiplier.ToString() + "x";
        score_ = 0;
        // Player.SetActive(true);

    }

    public void ReloadScene()
    {
        GameObject.FindGameObjectWithTag("Stats").GetComponentInParent<MenuController>().ChangeScreen(false); ;
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
                GameObject go = Instantiate(bottompillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
                //set the last current position
                CurrentPositionOfLastPlatform = go.transform;
                //deciding if the pillar needs foliage or not

                //Deprecated ^^ we decided that all pillars should have foilage to cover the edges 13/06/2016


                //setting the vector to spawn at
                Vector3 PO = new Vector3(go.transform.position.x, waterplain.transform.position.y + .2F, go.transform.position.z);

                GameObject foila = Instantiate(foilagesrites, PO, Quaternion.identity) as GameObject;

                foila.GetComponent<SpriteRenderer>().sprite = foliagesprites_[Random.Range(0, foliagesprites_.Length)];

                //adding the foilage and chunk objects to a list to be deleted
                ChunckList.Add(foila);
                ChunckList.Add(go);

            }
            else
            {
                GameObject go = Instantiate(CrumblePillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;
                //set the last current position
                CurrentPositionOfLastPlatform = go.transform;

                //adding the chunk objects to a list to be deleted

                ChunckList.Add(go);
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

                int MidInt = Random.Range(0, 10);
                Vector3 MidT = new Vector3(LastMidGround.transform.position.x + 15, LastMidGround.transform.position.y, 8 + MidInt);
                GameObject MidgroundTree = Instantiate(MidGroundTrees[Random.Range(0, MidGroundTrees.Count)], MidT, Quaternion.identity) as GameObject;
                LastMidGround = MidgroundTree.transform;

                //spawn foreground trees - 2.75
                int ForInt = Random.Range(0, 2);
                Vector3 ForT = new Vector3(LastForeGround.transform.position.x + 15, LastForeGround.transform.position.y, 2.75F + ForInt);
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

    public void Dead()
    {
        //Reset
        CurrentPositionOfLastPlatform = ORGPlatform;
        LastForeGround = ORGLastFore;
        LastMidGround = ORGLastMid;
        LastRuin = ORGLastRuin;
        CurrentBackground = ORGcurrentBG;
        firstrun = true;

        //destroying leftovers
        foreach(GameObject go in BGList)
        {
            Destroy(go);
        }

       
        foreach (GameObject t in ChunckList)
        {
            Destroy(t);
        }
    }
}
