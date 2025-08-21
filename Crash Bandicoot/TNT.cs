using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour {
    public float timer, explotimer;
    public bool tntimer, extimer, indexcheck;
    public GameObject Crash;
    public GameObject[] ex;
    public Crash_CPHY Crashcphy;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public GameObject explosion;
    public MeshRenderer msh;
    public BoxCollider tntcol;
    public float expogone;
    public bool expg, expofinished, norepeat;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y >= transform.localPosition.y && Crashcphy.sp.y < 0.0f)
        {
            Crashcphy.bouncing = true;
            tntimer = true;
            expofinished = true;

        }
        if (col.gameObject.name == "Crash" && Crashcphy.spin == true || col.gameObject.name == "Crash" && Crashcphy.slide == true || col.gameObject.tag == "Spinbox" || Crashcphy.bellyflop == true)
        {
            expofinished = true;
            //Crashcphy.cratecounter++;
            explosionmaker();
            expofinished = true;

        }
    }

        // Use this for initialization
        void Start () {
        indexcheck = false;
        tntimer = false;
        extimer = false;
        timer = 3.0f;
        explotimer = 2.0f;
        msh = GetComponent<MeshRenderer>();
        tntcol = GetComponent<BoxCollider>();
        Crash = GameObject.Find("Crash");
        Crashcphy = Crash.GetComponent<Crash_CPHY>();
        msh.material.color = Color.red;
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        expogone = 0.5f;
        expg = false;
        expofinished = false;
        norepeat = false;

    }

    // Update is called once per frame
    void Update() {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (Ps.TNdex< Ps.tntcount && indexcheck == false)
        {
            Ps.PosTNTs[Ps.TNdex] = transform.position;
            Ps.TNdex++;
        }
        else
            indexcheck = true;
        ex = GameObject.FindGameObjectsWithTag("explosion");
        if (expofinished == false) {           
            foreach (GameObject Explo in ex)
            {
                if (Explo != null && expofinished == false && ((transform.localPosition.x >= Explo.transform.position.x - 2.0 && transform.localPosition.x <= Explo.transform.position.x + 2.0) && (transform.localPosition.z >= Explo.transform.position.z - 2.0 && transform.localPosition.z <= Explo.transform.position.z + 2.0) && (transform.localPosition.y >= Explo.transform.position.y - 2.0 && transform.localPosition.y <= Explo.transform.position.y + 2.0)))
                {
                    timer = -1.0f;
                    expofinished = true;
                    break;

                }
            }
        }
        if (tntimer == true)
        {
            timer -= Time.deltaTime;
        }
        if(timer < 0)
        {
            tntimer = false;
            timer = 3.0f;
            extimer = true;
            msh.enabled = false;
            tntcol.isTrigger = true;
            //Crashcphy.cratecounter++;

            explosionmaker();
        }
        if (expg == true)
            expogone -= Time.deltaTime;
        if (expogone < 0.0f)
            expg = false;
        if (expg == false && expogone < 0.0f)
        {
            Destroy(explosion);
            Destroy(gameObject);
        }


    }
    public void explosionmaker()
    {
        tntcol.isTrigger = true;
        Crashcphy.cratecounter++;
        Cpm.destroyedcrates++;
        if (norepeat == false)
        {
            norepeat = true;
            explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            explosion.transform.position = transform.position;
            explosion.name = "explosion";
            explosion.tag = "explosion";
            explosion.transform.localScale *= 2.0f;
            expg = true;
            Cpm.PosTNTs[Cpm.TNdex] = transform.position;
            Cpm.TNdex++;
            Cpm.tntdes++;
        }
    }
}
