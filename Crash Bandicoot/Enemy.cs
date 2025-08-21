using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject Crash;
    public Crash_CPHY Crashcphy;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public NormalCrate NC;
    public Rigidbody rb, rb2;
    public MeshRenderer msh;
    public LayerMask enemy;
    public bool indexcheck;
    public float walktime, xshare, zshare, xzforce;
    BoxCollider Ebox, Ebox2;
    bool E, NE, N, NW, W, SW, S, SE, left, right, dead;
    Vector3 EM, EMscale;
    public Vector3 sp;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Spinbox" && dead == false)
        {
            dead = true;
            Cpm.PosEnemy[Cpm.Enedex] = EM;
            Cpm.Enemyscale[Cpm.Enedex] = EMscale;
            Cpm.Enedex++;
            Cpm.enedes++;
        }
        if (Crashcphy.spin == true || Crashcphy.slide == true || col.gameObject.tag == "Spinbox")
        {
            walktime = 2.0f;
            rb.isKinematic = false;
        }
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y < transform.position.y + 0.5f) {
            if (Crashcphy.spin == false && Crashcphy.slide == false)
            {
                Crashcphy.dtimer = true;
                Cpm.dtimer = true;
            }
        }
        if (col.gameObject.name == "Crash" && col.gameObject.transform.position.y  >= transform.position.y + 0.5f && Crashcphy.bellyflop == false && Crashcphy.sp.y < 0.0f )
        {
            Crashcphy.bouncing = true;
            if (dead == false)
            {
                dead = true;
                Cpm.PosEnemy[Cpm.Enedex] = EM;
                Cpm.Enemyscale[Cpm.Enedex] = EMscale;
                Cpm.Enedex++;
                Cpm.enedes++;

            }
            Destroy(gameObject);
        }
        if (Crashcphy.bellyflop == true)
        {
            if (dead == false)
            {
                dead = true;
                Cpm.PosEnemy[Cpm.Enedex] = EM;
                Cpm.Enemyscale[Cpm.Enedex] = EMscale;
                Cpm.Enedex++;
                Cpm.enedes++;

            }
            Destroy(gameObject);
        }


        if (col.gameObject.name == "Crash" && Crashcphy.IsForward(Crashcphy.crashground) && Crashcphy.spin == true || col.gameObject.name == "spinbox3")
        {
            rb.AddForce(0.0f, 0.0f, xzforce);
            xshare = 0.0f;
            zshare = xzforce;
            //N = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "Crash" && Crashcphy.IsBackward(Crashcphy.crashground) && Crashcphy.spin == true || col.gameObject.name == "spinbox4")
        {
            rb.AddForce(0.0f, 0.0f, -xzforce);
            xshare = 0.0f;
            zshare = -xzforce;
            //S = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "Crash" && Crashcphy.IsLeft(Crashcphy.crashground) && Crashcphy.spin == true || col.gameObject.name == "spinbox2")
        {
            rb.AddForce(-xzforce, 0.0f, 0.0f);
            xshare = -xzforce;
            zshare = 0.0f;
            //W = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "Crash" && Crashcphy.IsRight(Crashcphy.crashground) && Crashcphy.spin == true || col.gameObject.name == "spinbox")
        {
            rb.AddForce(xzforce, 0.0f, 0.0f);
            xshare = xzforce;
            zshare = 0.0f;
            //E = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "spinbox5")
        {
            rb.AddForce(xzforce, 0.0f, xzforce);
            xshare = xzforce;
            zshare = xzforce;
            //NE = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "spinbox6")
        {
            rb.AddForce(-xzforce, 0.0f, xzforce);
            xshare = -xzforce;
            zshare = xzforce;
            //NW = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "spinbox7")
        {
            rb.AddForce(-xzforce, 0.0f, -xzforce);
            xshare = -xzforce;
            zshare = -xzforce;
            //SW = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        if (col.gameObject.name == "spinbox8")
        {
            rb.AddForce(xzforce, 0.0f, -xzforce);
            xshare = xzforce;
            zshare = -xzforce;
            //SE = true;
            Ebox.isTrigger = true;
            rb.useGravity = false;
        }
        //if(col.gameObject.name == "Quetion")
            //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        

    }

    private void OnTriggerEnter(Collider col)
    {
        /*if (col.gameObject.tag == "crate")
        {
            //rb.AddForce(0.0f, 50.0f, 0.0f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }*/
        if (col.gameObject.tag == "CagedC")
        {
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "crate")
        {
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            col.gameObject.GetComponent<NormalCrate>().CreateWumpa();
        }
        if (col.gameObject.tag == "question")
        {
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            col.gameObject.GetComponent<QuestionCrate>().CreateWumpa();
        }
        if (col.gameObject.tag == "Arrow")
        {
            Crashcphy.cratecounter++;
            Cpm.destroyedcrates++;
            col.gameObject.GetComponent<ArrowCrate>().CreateWumpa();
        }
        if(col.gameObject.tag == "Tnt")
        {
            
            col.gameObject.GetComponent<TNT>().expofinished = true;
            col.gameObject.GetComponent<TNT>().explosionmaker();
        }
        if (col.gameObject.tag == "nitros")
        {
            col.gameObject.GetComponent<Nitros>().expofinished = true;
            col.gameObject.GetComponent<Nitros>().explosionmaker();
        }

        if (col.gameObject.tag == "enemy" && rb.isKinematic == false)
            walktime = 2.0f;
        if (col.gameObject.tag == "enemy" && dead == false)
        {
                dead = true;
                Cpm.PosEnemy[Cpm.Enedex] = EM;
                Cpm.Enemyscale[Cpm.Enedex] = EMscale;
                Cpm.Enedex++;
                Cpm.enedes++;          
        }
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) < 0.9f) && (sp.z > 0))
        {
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(xshare, 0.0f, zshare);
        }
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) > 0.9f) && (transform.position.x > col.transform.position.x)  && (sp.z > 0))
        {
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(-xzforce, 0.0f, xzforce);
        }
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) > 0.9f) && (transform.position.x < col.transform.position.x) && (sp.z > 0))
        {
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(xzforce, 0.0f, xzforce);
        }
        //------------------------------------------------------------------------------------------------------------
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) < 0.9f) && (sp.z < 0))
        {
            
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(xshare, 0.0f, zshare);
        }
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) > 0.9f) && (transform.position.x > col.transform.position.x) && (sp.z < 0))
        {
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(-xzforce, 0.0f, -xzforce);
        }
        if ((col.gameObject.tag == "enemy" && rb.isKinematic == false) && (Math.Abs(transform.position.x - col.transform.position.x) > 0.9f) && (transform.position.x < col.transform.position.x) && (sp.z < 0))
        {
            rb2 = col.GetComponent<Rigidbody>();
            Ebox2 = col.GetComponent<BoxCollider>();
            rb2.isKinematic = false;
            Ebox2.isTrigger = true;
            rb2.useGravity = false;
            rb2.AddForce(xzforce, 0.0f, -xzforce);
        }
    }

        // Use this for initialization
    void Start () {
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        msh = GetComponent<MeshRenderer>();
        msh.material.color = Color.black;
        Crash = GameObject.Find("Crash");
        Crashcphy = Crash.GetComponent<Crash_CPHY>();
        enemy = LayerMask.NameToLayer("Enemy");
        Ebox = GetComponent<BoxCollider>();
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        indexcheck = false;
        NE = false;
        N = false;
        NW = false;
        W = false;
        SW = false;
        S = false;
        SE = false;
        dead = false;
        rb.isKinematic = true;
        walktime = 3.0f;
        right = true;
        left = false;
        EM = transform.position;
        EMscale = transform.localScale;
        xzforce = 2000.0f;

        Ps.EnemySc = EMscale;
	}

    private static T AddComponent<T>()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (Ps.Enedex < Ps.enemycount && indexcheck == false)
        {
            Ps.PosEnemy[Ps.Enedex] = transform.position;
            Ps.Enedex++;
        }
        else
            indexcheck = true;
        sp = GetComponent<Rigidbody>().velocity;
        
        if (rb.isKinematic == true && PauseScreen.isPaused == false)
        {
            if (walktime > 0.0f && right == true)
            {
                walktime -= Time.deltaTime;
                transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
            }
            if (walktime < 0.0f)
            {
                right = false;
                left = true;
            }
            if (walktime < 3.0f && left == true)
            {
                walktime += Time.deltaTime;
                transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
            }
            if (walktime > 3.0f)
            {
                right = true;
                left = false;
            }
        }
        if(rb.isKinematic == false)
        {
            walktime -= Time.deltaTime;
            if (walktime < 0.0f)
                Destroy(gameObject);
        }
    }
    public bool IsForward(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 100.55f, enemy);
    }
    public bool IsBackward(Vector3 ground)
    {
        return Physics.Raycast(ground, -Vector3.forward, 100.55f, enemy);
    }
    public bool IsLeft(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.left, 100.55f, enemy);
    }
    public bool IsRight(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.right, 100.55f, enemy);
    }
}
