using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCrate : MonoBehaviour {

    public GameObject Crash, Crfruit;
    public GameObject[] ex;
    public Crash_CPHY Crashcphy;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public float wumpatimer;
    public bool waitndestroy, expofinished, indexcheck;
    private Rigidbody rb2;
    public MeshRenderer msh;
    public BoxCollider Acol;
    public SphereCollider Wcol;

    private void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y >= transform.localPosition.y && Crashcphy.sp.y < 0.0f && Crashcphy.bellyflop == false)
        {
            Crashcphy.bouncing = true;
        }
        if (col.gameObject.name == "Crash" && Crashcphy.spin == true && waitndestroy == false || col.gameObject.tag == "Spinbox" && waitndestroy == false || Crashcphy.bellyflop == true && waitndestroy == false)
        {
            Cpm.destroyedcrates++;
            Crfruit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Crfruit.transform.position = transform.position;
            Crfruit.name = "fruity";
            Crfruit.tag = "WFruit";
            Crfruit.tag = "ghost";
            //Crfruit.AddComponent <TNT> ();
            Crfruit.transform.localScale *= 0.5f;
            Wcol = Crfruit.GetComponent<SphereCollider>();
            Wcol.isTrigger = true;
            rb2 = Crfruit.AddComponent<Rigidbody>();
            rb2.AddForce(new Vector3(0.0f, 150.0f * 2.0f, 0.0f));
            waitndestroy = true;
            msh.enabled = false;
            Acol.isTrigger = true;
            Crashcphy.cratecounter++;
        }
        
    }
    // Use this for initialization
    void Start()
    {   
        wumpatimer = 0.9f;
        waitndestroy = false;
        msh = GetComponent<MeshRenderer>();
        Acol = GetComponent<BoxCollider>();
        Crash = GameObject.Find("Crash");
        Crashcphy = Crash.GetComponent<Crash_CPHY>();
        msh.material.color = Color.blue;
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        indexcheck = false;
        expofinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        ex = GameObject.FindGameObjectsWithTag("explosion");
        if (Ps.Arrindex < Ps.arrowcount && indexcheck == false)
        {
            Ps.PosArrowcrate[Ps.Arrindex] = transform.position;
            Ps.Arrindex++;
        }
        else
            indexcheck = true;
        if (expofinished == false)
        {
            foreach (GameObject Explo in ex)
            {
                if (Explo != null && expofinished == false && ((transform.localPosition.x >= Explo.transform.position.x - 2.0 && transform.localPosition.x <= Explo.transform.position.x + 2.0) && (transform.localPosition.z >= Explo.transform.position.z - 2.0 && transform.localPosition.z <= Explo.transform.position.z + 2.0) && (transform.localPosition.y >= Explo.transform.position.y - 2.0 && transform.localPosition.y <= Explo.transform.position.y + 2.0)))
                {
                    expofinished = true;
                    Cpm.PosArrowcrate[Cpm.Arrindex] = transform.position;
                    Cpm.Arrindex++;
                    Cpm.arrowdes++;
                    Crashcphy.cratecounter++;
                    Cpm.destroyedcrates++;
                    Destroy(gameObject);
                }
            }
        }
        if (waitndestroy == true)
            wumpatimer -= Time.deltaTime;
        if (wumpatimer < 0.0f)
        {
            Crfruit.tag = "WFruit";
            Destroy(rb2);
            Cpm.PosArrowcrate[Cpm.Arrindex] = transform.position;
            Cpm.Arrindex++;
            Cpm.arrowdes++;  
            Destroy(gameObject);
        }
    }
    public void CreateWumpa()
    {
        Crfruit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Crfruit.transform.position = transform.position;
        Crfruit.name = "fruity";
        Crfruit.tag = "WFruit";
        Crfruit.tag = "ghost";
        Crfruit.transform.localScale *= 0.5f;
        Wcol = Crfruit.GetComponent<SphereCollider>();
        Wcol.isTrigger = true;
        rb2 = Crfruit.AddComponent<Rigidbody>();
        rb2.AddForce(new Vector3(0.0f, 150.0f * 2.0f, 0.0f));
        waitndestroy = true;
        msh.enabled = false;
        Acol.isTrigger = true;
        expofinished = true;
    }
}
