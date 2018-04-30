using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class RibCage : MonoBehaviour {
   [SerializeField] public bool impact;
    [SerializeField] public bool StartUp;

    public SpriteRenderer blackboi;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag =="Lung")
        {
            Debug.Log("ow");
            impact = true;
            StartUp = false;
            Camera.main.backgroundColor = new Color(255, 0, 0);

            blackboi.color = new Color(255, 0, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Lung")
        {
            Debug.Log("lung exited");
            impact = false;
            StartUp = true;
            Camera.main.backgroundColor = new Color(0, 0, 0);
            blackboi.color = new Color(0, 0, 0);

        }
    }
}
