using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public GameObject[] currentpillars;

    public GameObject foilagesrites;

    public Sprite[] foliagesprites_;

    public Transform CurrentPositionOfLastPlatform;

    public int CurrentLevel;

    public int StartingAreaSize;

    public float difficultyMultiplier;

    public GameObject doublepillar, bottompillar,waterplain;

    public GameObject Bow,Player;

    public float jumppower,rightjumppower;

    private Rigidbody2D rb; 

	public int clicks = 0;

    private int multiplier;

    private Image bar;

    private Text multitext;
    public float score_;
    public Text score;

    private Transform lastpos;
  
    // Use this for initialization
    void Start()
    {
        //loading area
        bar = GameObject.FindGameObjectWithTag("MovingBar").GetComponent<Image>();
        rb = Player.GetComponent<Rigidbody2D>();
        multitext = bar.GetComponentInChildren<Text>();

        StartCoroutine("MultiTimer");
        lastpos = Player.transform;

        //Load pillars from resources in future

        //Spawn our starting area of x tiles

        for (int i = 0; i < StartingAreaSize; i++)
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
            
            //setting the min jumpower incase someone fiddles in the editor with it
     
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        while(bar.fillAmount > 0)
        {
            bar.fillAmount -= 0.02F;
            yield return new WaitForSeconds(.05F);
        }

        if(bar.fillAmount <= 0)
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

}
