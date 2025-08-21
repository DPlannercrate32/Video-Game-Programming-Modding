using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros : MonoBehaviour {
    public GameObject Crash;
    public Crash_CPHY Crashcphy;
    public GameObject Cpm2;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public GameObject explosion;
    public GameObject[] ex;
    public bool expofinished, norepeat;
    public MeshRenderer msh;
    public BoxCollider Ncol;
    public float expogone;
    public bool expg, indexcheck;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Crash" || col.gameObject.tag == "Spinbox")
        {         
            explosionmaker();
            expofinished = true;
        }
    }

    // Use this for initialization
    void Start () {
        indexcheck = false;
        msh = GetComponent<MeshRenderer>();
        Ncol = GetComponent<BoxCollider>();
        Crash = GameObject.Find("Crash");
        Crashcphy = Crash.GetComponent<Crash_CPHY>();
        msh.material.color = Color.green;
        //Cpm2 = GameObject.Find("ObjectMemory");
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        expogone = 0.5f;
        expg = false;
        expofinished = false;
        norepeat = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (Ps.Ndex < Ps.nitrocount && indexcheck == false)
        {
            Ps.PosNitros[Ps.Ndex] = transform.position;
            Ps.Ndex++;
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
                    Crashcphy.cratecounter++;
                    explosionmaker();
                    break;
                }
            }
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
        Crashcphy.cratecounter++;
        Cpm.destroyedcrates++;
        Ncol.isTrigger = true;
        if (norepeat == false)
        {
            norepeat = true;
            explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            explosion.transform.position = transform.position;
            explosion.name = "explosion";
            explosion.tag = "explosion";
            explosion.transform.localScale *= 2.0f;
            expg = true;
            Cpm.PosNitros[Cpm.Ndex] = transform.position;
            Cpm.Ndex++;
            Cpm.nitrodes++;
        }
    }
}
