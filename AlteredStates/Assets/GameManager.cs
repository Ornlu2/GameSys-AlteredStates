using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject Lung1, Lung2;
    public GameObject RibCage;

    public Vector3 LungExpansionMax;
    public Vector3 deflatedlungs;

    public Vector3 RibCageExpansionMax;
    public Vector3 RibCageBottomOut;

    public float time;
    public float deflatetime;
   [SerializeField] private bool deflating = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(deflating)
        {
            Lung1.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, deflatedlungs, deflatetime * Time.deltaTime);
            Lung2.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, deflatedlungs, deflatetime * Time.deltaTime);
            Debug.Log("deflating");
        }
        if(!deflating)
        {
            Lung1.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, LungExpansionMax, time * Time.deltaTime);
            Lung2.transform.localScale = Vector3.Lerp(Lung1.transform.localScale, LungExpansionMax, time * Time.deltaTime);
            Debug.Log("inflating");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RibCage.transform.position += new Vector3(RibCage.transform.position.x, RibCage.transform.position.y + 0.1f, RibCage.transform.position.z);
        }
       
        if(Input.GetKeyUp(KeyCode.Space))
        {
            RibCage.transform.position = new Vector3(RibCage.transform.position.x, RibCage.transform.position.y - 0.5f*Time.deltaTime, RibCage.transform.position.z);

        }


    }
}
