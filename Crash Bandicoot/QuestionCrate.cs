using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCrate : MonoBehaviour {

    public GameObject Crash, Crfruit;
    public GameObject[] fruity, ex;
    public Crash_CPHY Crashcphy;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public float wumpatimer, offset, offset2;
    public bool waitndestroy, expofinished, indexcheck;
    private Rigidbody[] rb2;
    public MeshRenderer msh;
    public BoxCollider Norcol;
    public SphereCollider Wcol;


    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y >= transform.localPosition.y + 0.5f && Crashcphy.sp.y < 0.0f && Crashcphy.bellyflop == false)
        {
            Crashcphy.bouncing = true;
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            CreateWumpa();
            
        }
        if (col.gameObject.name == "Crash" && Crashcphy.spin == true && waitndestroy == false || col.gameObject.tag == "Spinbox" && waitndestroy == false || Crashcphy.bellyflop == true && waitndestroy == false)
        {
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            CreateWumpa();
        }
        //if (col.gameObject.name == "explosion")
            //Destroy(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        wumpatimer = 0.9f;
        waitndestroy = false;
        msh = GetComponent<MeshRenderer>();
        Norcol = GetComponent<BoxCollider>();
        Crash = GameObject.Find("Crash");
        Crashcphy = Crash.GetComponent<Crash_CPHY>();
        msh.material.color = Color.magenta;
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        fruity = new GameObject[15];
        rb2 = new Rigidbody[15];
        offset = 0.0f;
        offset2 = 0.0f;
        expofinished = false;
        indexcheck = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (Ps.QMindex < Ps.questioncount && indexcheck == false)
        {
            Ps.PosQuestionmarkcrate[Ps.QMindex] = transform.position;
            Ps.QMindex++;
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
                    Cpm.PosQuestionmarkcrate[Cpm.QMindex] = transform.position;
                    Cpm.QMindex++;
                    Cpm.questiondes++;
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
            for (int j = 0; j < fruity.Length; j++)
            {
                if (fruity[j] == null)
                    continue;
                rb2[j] = fruity[j].GetComponent<Rigidbody>();
                Destroy(rb2[j]);
                fruity[j].tag = "WFruit";
            }
            //Destroy(rb2);
            Cpm.PosQuestionmarkcrate[Cpm.QMindex] = transform.position;
            Cpm.QMindex++;
            Cpm.questiondes++;
            
            Destroy(gameObject);
        }
    }
    public void CreateWumpa()
    {
        int fr = Random.Range(1, 11);
        for (int i = 0; i < fr; i++)
        {
            float r = Random.Range(-15.0f, 15.0f);
            Crfruit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Crfruit.transform.position = transform.position;
            Crfruit.name = "fruity";
            Crfruit.tag = "ghost";
            Crfruit.transform.localScale *= 0.5f;
            Wcol = Crfruit.GetComponent<SphereCollider>();
            Wcol.isTrigger = true;
            rb2[i] = Crfruit.AddComponent<Rigidbody>();

            rb2[i].AddForce(new Vector3(offset2 * r, 150.0f * 2.0f, offset));
            offset += 5.0f;
            offset2 += 0.8f;
            waitndestroy = true;
            msh.enabled = false;
            Norcol.isTrigger = true;
            fruity[i] = Crfruit;
            expofinished = true;
        }
    }
}
