using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public GameObject Lung1, Lung2;
    public GameObject RibCage;

    public Vector3 LungExpansionMax;
    public Vector3 deflatedlungs;

    public Vector3 RibCageExpansionMax;
    public Vector3 RibCageBottomOut;

    public float time;
    public float deflatetime;

    public UnityEvent FullChestExpansion;
    public UnityEvent Breathin;
    public UnityEvent Breathout;
    public UnityEvent LaboredBreath;

    public AudioSource inhale;
    public AudioClip clip;

   [SerializeField] private bool deflating = false;
   [SerializeField] private int energy;

    public Text Energy;
    public Text Health;
    public int energyMax;
    public int HealthMax;
    public int health;
   [SerializeField] private bool canPress = true;
    private bool startupstarted;

    // Use this for initialization
    void Awake ()
    {
        energy = energyMax;
        health = HealthMax;
        canPress = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LungImpact();

        Energy.text = "Energy: " + energy;
        Health.text = "Health: " + health;
        if(energy < 0)
        {
            canPress = false;
        }
        if (RibCage.transform.position.y <= RibCageBottomOut.y)
        {
            canPress = true;
        }
        if(RibCage.GetComponent<RibCage>().impact == true)
        {
            health--;
        }
        if(health <=0)
        {
            SceneManager.LoadScene(1);
        }


      if(energy < 0)
        {
            StartCoroutine(regainenergy());
        }



        if (deflating)
        {
            Lung1.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, deflatedlungs, deflatetime * Time.deltaTime);
            Lung2.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, deflatedlungs, deflatetime * Time.deltaTime);
            Debug.Log("deflating");
            if(startupstarted ==false)
           {
                Breathout.Invoke();
           // inhale.PlayOneShot(clip);

             }
        }
        if (!deflating)
        {
            Lung1.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, LungExpansionMax, time * Time.deltaTime);
            Lung2.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, LungExpansionMax, time * Time.deltaTime);
            Debug.Log("inflating");
            if(startupstarted == false)
           {
                 Breathin.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RibCage.transform.position.y <= 0.1)
            {
                if(energy >=1 )
                {
                    if(canPress == true)
                    {
                        RibCage.transform.position = new Vector3(RibCage.transform.position.x, RibCage.transform.position.y + 0.09f, RibCage.transform.position.z);
                        energy--;
                    }

                }

            }

            else
            {
                FullChestExpansion.Invoke();

                energy--;

            }

           energy--;


        }
        else
        {
            if(RibCage.transform.position.y >= -0.15)
            {

                RibCage.transform.position = Vector3.Lerp(RibCage.transform.position, new Vector3(0, -1, 0), 0.1f * Time.deltaTime);

                if(energy < energyMax)
                {
                   
                 //energy++;

                    
                }
               
            }
        }
        if(Lung1.transform.localScale.y >= LungExpansionMax.y-0.1)
        {
            deflating = true;

        }
        else if(Lung1.transform.localScale.y <= deflatedlungs.y+0.1)
        {
            deflating = false;
            if(RibCage.GetComponent<RibCage>().impact == false)
            {
                StartCoroutine(ChangeTime());

            }

        }


    }
    IEnumerator ChangeTime()
    {
        time = Random.Range(0.4f, 3);
        deflatetime = Random.Range(0.4f, 3);
        yield return null;
    }
    void LungImpact()
    {
        if(RibCage.GetComponent<RibCage>().impact == true && RibCage.GetComponent<RibCage>().StartUp == false)
        {
            deflating = true;
            deflatetime = 2f;
            
        }
        if(RibCage.GetComponent<RibCage>().StartUp == true && startupstarted == false)
        {
            StartCoroutine(StartUp1());
            LaboredBreath.Invoke();

            //deflating = false;
        }
    }
    IEnumerator StartUp1()
    {
        RibCageExpansionMax.y = 1.5f;
        LungExpansionMax.y = 1.5f;
        deflatetime = 0.5f;
        time = 0.5f;
        Debug.Log("startup1");
        startupstarted = true;

        yield return new WaitForSecondsRealtime(4);


        StartCoroutine(StartUp2());

        
        yield return null;
    }
    IEnumerator StartUp2()
    {
        RibCageExpansionMax.y = 2;
        LungExpansionMax.y = 2;
        Debug.Log("startup2");
        LaboredBreath.Invoke();

        yield return new WaitForSecondsRealtime(4);

      
            StartCoroutine(StartUp3());

        
        yield return null;

    }
    IEnumerator StartUp3()
    {
        RibCageExpansionMax.y = 3;
        LungExpansionMax.y = 3;
        Debug.Log("startup3");

        yield return new WaitForSecondsRealtime(4);
        
        StartCoroutine(StartUp4());

        
        yield return null;

    }
    IEnumerator StartUp4()
    {
        RibCageExpansionMax.y = 4.5f;
        LungExpansionMax.y = 4;
        RibCage.GetComponent<RibCage>().impact = false;
        RibCage.GetComponent<RibCage>().StartUp = false;
        LaboredBreath.Invoke();

        startupstarted = false;
        Debug.Log("startup4");
        yield return null;

    }
    IEnumerator regainenergy()
    {
        yield return new WaitForSecondsRealtime(2);
        if(energy <energyMax)
        {
            energy++;

        }


    }
}
