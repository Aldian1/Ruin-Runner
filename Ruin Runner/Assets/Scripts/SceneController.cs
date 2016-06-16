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
    private List<GameObject> foliagechunck = new List<GameObject>();



    public Transform CurrentPositionOfLastPlatform, LastForeGround, LastMidGround, LastRuin,LastWater;

    public int CurrentLevel;



    public float difficultyMultiplier;

    public GameObject doublepillar, bottompillar, waterplain;

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

    // Use this for initialization
    void Start()
    {

        SpawnWorld();
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


        //spawning the level ahead of us
        if(Vector2.Distance(this.transform.position,CurrentPositionOfLastPlatform.transform.position) < 5)
        {
            SpawnWorld();
        }

      //  if(Vector2.Distance(this.transform.position, ChunckList[ChunckList.Count].transform.position) > 5)
      //  {
     //       Destroy(ChunckList[0].gameObject);
      //      ChunckList.Remove(ChunckList[0]);
      //  }


        score.text = score_.ToString();
        //deprecated
        /* //if we are holding the mouse we increase the jumppower, check that we arent exceeding the jumpower though
         if(Input.GetKey(KeyCode.Mouse0) && jumppower <= MaxJumpPower)
         {
             jumppower+= 5;
         }
         */


        //on release we want to change the jumpower back to the minimum one. And launch the player?

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

            //makes sure we spawn ahead of the last pillar with some variance to height and width

            GameObject go = Instantiate(bottompillar, new Vector3(CurrentPositionOfLastPlatform.position.x + u, Random.Range(-2, 0), -4.5F), Quaternion.identity) as GameObject;

            //set the last current position
            CurrentPositionOfLastPlatform = go.transform;

            //deciding if the pillar needs foliage or not

            //Deprecated ^^ we decided that all pillars should have foilage to cover the edges 13/06/2016


            //setting the vector to spawn at
            Vector3 PO = new Vector3(go.transform.position.x, waterplain.transform.position.y + .2F, go.transform.position.z);

            GameObject foila = Instantiate(foilagesrites, PO, Quaternion.identity) as GameObject;

            foila.GetComponent<SpriteRenderer>().sprite = foliagesprites_[Random.Range(0, foliagesprites_.Length)];



            if (!firstrun)
            {


                //spawn ruins - ideal is 18.5

                //spawn midground trees - 8

                //spawn foreground trees - 2.75

                //adding the foilage and chunk objects to a list to be deleted
                foliagechunck.Add(foila);
                ChunckList.Add(go);
            }
        }

        if (!firstrun)
        {
            //construct new vector for water
         //   Vector3 p = new Vector3(LastWater.transform.position.x + 90, LastWater.transform.position.y, LastWater.transform.position.z);

            //spawn new water plain
          // GameObject wt = Instantiate(water, p, Quaternion.identity) as GameObject;
          //  LastWater = wt.transform;
        }
        firstrun = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }
}
