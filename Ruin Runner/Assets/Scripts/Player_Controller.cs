using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{

    public Animator ar;
    public AudioSource[] as_;
    public int switcher;
    public bool onground;

    public Sprite MagnetItem;

    public GameObject HoldableItem, Cloud, potitem;

    public bool MagnetOn;


    private Rigidbody2D rb;

    public float jumppower, rightjumppower,magtime;

    public Text TimerText;

    public GameObject mgitem;

    public bool CanClick;

    void Start()
    {
      
        ar = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        PlayerPrefs.SetInt("PotionTime", 15);
        PlayerPrefs.SetInt("PotionAmount", 20);
    }

    //magnets field
    void OnTriggerEnter2D(Collider2D col)
    {
        
    }


    void OnCollisionEnter2D(Collision2D col)
    {

        Debug.Log(col.gameObject.tag);
        //Debug.Log ("Collided");
        if (this.enabled == true)
        {
            ar.SetBool("Land", true);
            ar.SetBool("Air", false);

            if (col.gameObject.tag == "Slippy")
            {
                as_[1].Play();

            }
            else
            {
                as_[0].Play();
            }

        }
      
        if (col.transform.tag == "Pillar" || col.transform.tag == "Slippy")
        {
            switcher = 0;
        }

        Cloud.SetActive(false);
        onground = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        onground = false;
    }



    void Update()
    {



        if (switcher <= 1)
        {
            Movement();
        }
        if (onground)
        {
            switcher = 0;

            if (Input.GetKeyDown(KeyCode.Mouse0) && switcher < 2)
            {
                ar.SetBool("Land", false);
                ar.SetBool("Jump", true);


            }

     



        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ar.SetBool("Land", false);
            ar.SetBool("Jump", false);
            ar.SetBool("Air", true);
            // Cloud.SetActive(false);
            return;

        }
    }



    public void ActivateItem(GameObject item)
    {
        if (item.name == "MagnetItem" && PlayerPrefs.GetInt("MagnetAmount") > 0)
        {
            magtime = PlayerPrefs.GetInt("MagPower");
            MagnetOn = true;
            item.SetActive(false);
            StartCoroutine("ItemTimer", item);
            TimerText.enabled = true;
            mgitem = item;
            PlayerPrefs.SetInt("MagnetAmount", PlayerPrefs.GetInt("MagnetAmount") - 1);

        }
        
        if(item.name == "PotionItem" && PlayerPrefs.GetInt("PotionAmount") > 0)
        {
            item.SetActive(false);
            magtime = PlayerPrefs.GetInt("PotionTime");
            TimerText.enabled = true;
            mgitem.SetActive(false);
            Time.timeScale = .5F;
            StartCoroutine("ItemTimer", item);
        }

            PlayerPrefs.Save();
        }

    public void Movement()
    {




        //jump controller section

        //short click
        if (Input.GetKeyDown(KeyCode.Mouse0) && switcher < 1)
        {


            //long click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {



                    //doublejumppower
                    rb.AddForce(new Vector2(0, jumppower));
                    rb.velocity = new Vector2(2, 0);
                    rb.AddForce(Vector2.right * rightjumppower);
                    switcher += 1;
           
                

            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.down * 100);
        }

    }


  public IEnumerator ItemTimer(GameObject i)
    {
        potitem.SetActive(false);
       

        TimerText.text = magtime.ToString();

        while (magtime > 0)
        {
            yield return new WaitForSeconds(1);
            TimerText.text = magtime.ToString();
            magtime--;

        }

   
      
    
    
        Reset();
    }


    public void Reset()
    {
        TimerText.text = magtime.ToString();
        magtime = 0;
        Time.timeScale = 1;
         StopAllCoroutines();
        TimerText.enabled = false;
        MagnetOn = false;
        potitem.SetActive(true);
        if (mgitem != null)
        {
            mgitem.SetActive(true);
        }
    }
}
