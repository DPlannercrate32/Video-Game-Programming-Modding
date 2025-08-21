using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedCrate : MonoBehaviour {
    public GameObject Crash, Crfruit;
    public GameObject[] ex;
    public Crash_CPHY Crashcphy;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public int bounces, CCindex2;
    public float wumpatimer;
    public bool waitndestroy, expofinished, indexcheck;
    private Rigidbody rb2;
    public MeshRenderer msh;
    public BoxCollider Cagedcol;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Crash" && bounces < 0)
        {
            Cpm.PosCagedcrate[Cpm.CCindex] = transform.position;
            Cpm.CCindex++;
            Cpm.cageddes++;
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            Destroy(gameObject);
        }
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y >= transform.localPosition.y && Crashcphy.sp.y < 0.0f && Crashcphy.bellyflop == false)
        {
            Crashcphy.bouncing = true;
            Crashcphy.fruits += 2;
            bounces -= 1;
        }
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y <= transform.localPosition.y)
        {
            Crashcphy.fruits += 2;
            bounces -= 1;
        }
        if (col.gameObject.name == "Crash" && Crashcphy.spin == true && waitndestroy == false || col.gameObject.tag == "Spinbox" && waitndestroy == false)
        {
            Crfruit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Crfruit.transform.position = transform.position;
            Crfruit.name = "fruity";
            Crfruit.tag = "WFruit";
            Crfruit.transform.localScale *= 0.5f;
            rb2 = Crfruit.AddComponent<Rigidbody>();
            rb2.AddForce(new Vector3(0.0f, 150.0f * 2.0f, 0.0f));
            waitndestroy = true;
            msh.enabled = false;
            Cagedcol.isTrigger = true;
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
        }
        if (Crashcphy.bellyflop == true)
        {
            Cpm.PosCagedcrate[Cpm.CCindex] = transform.position;
            Cpm.CCindex++;
            Cpm.cageddes++;
            Cpm.destroyedcrates++;
            Destroy(gameObject);
        }
    }
   
    // Use this for initialization
    void Start ()
        {
            indexcheck = false;
            bounces = 3;
            wumpatimer = 0.9f;
            waitndestroy = false;
            msh = GetComponent<MeshRenderer>();
            Cagedcol = GetComponent<BoxCollider>();
            Crash = GameObject.Find("Crash");
            Crashcphy = Crash.GetComponent<Crash_CPHY>();
            msh.material.color = Color.yellow;
            Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
            expofinished = false;
            Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
       
    }
	
	// Update is called once per frame
	void Update () {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (Ps.CCindex < Ps.cagedcount && indexcheck == false)
        {
            Ps.PosCagedcrate[Ps.CCindex] = transform.position;
            Ps.CCindex++;
        }
        else
            indexcheck = true;
        ex = GameObject.FindGameObjectsWithTag("explosion");
        if (expofinished == false)
        {
            foreach (GameObject Explo in ex)
            {
                if (Explo != null && expofinished == false && ((transform.localPosition.x >= Explo.transform.position.x - 2.0 && transform.localPosition.x <= Explo.transform.position.x + 2.0) && (transform.localPosition.z >= Explo.transform.position.z - 2.0 && transform.localPosition.z <= Explo.transform.position.z + 2.0) && (transform.localPosition.y >= Explo.transform.position.y - 2.0 && transform.localPosition.y <= Explo.transform.position.y + 2.0)))
                {
                    expofinished = true;
                    Cpm.PosCagedcrate[Cpm.CCindex] = transform.position;
                    Cpm.CCindex++;
                    Cpm.cageddes++;
                    Crashcphy.cratecounter++;
                    Cpm.destroyedcrates++;
                    Destroy(gameObject);
                }
            }
        }
        if (waitndestroy == true)
            wumpatimer -= Time.deltaTime;
        if(wumpatimer < 0.0f)
        {
            Destroy(rb2);
            Cpm.PosCagedcrate[Cpm.CCindex] = transform.position;
            Cpm.CCindex++;
            Cpm.cageddes++;     
            Destroy(gameObject);
        }
	}
}
