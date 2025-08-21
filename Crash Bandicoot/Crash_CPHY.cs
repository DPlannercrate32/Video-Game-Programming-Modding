using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crash_CPHY : MonoBehaviour
{

    public GameObject crash, explosion, ex, Crfruit, crystal, gem, pathg;
    public GameObject spinbox, spinbox2, spinbox3, spinbox4, spinbox5, spinbox6, spinbox7, spinbox8, fp;
    public GameObject[] fruity, ghost;
    public CPMemory Cpm;
    public PauseScreen Ps;
    public CagedCrate CCr;
    public FollowPla FP;
    public LinearCamera camera;
    public TNT Tnts;
    public Pathguide PG;
    public Text wfts, crte, crteT;
    public float maxspeed, acceleration, accelLR, accelUD, spinlimit, jumptimer, trackerUD, trackerRL, slidetimer, wumpatimer, deathtimer, yaxis, ogyaxis, cpyaxis;
    public int fruits, lives, cagebounces, cratecounter, cratetotal;
    public LayerMask layer, bounce, enemy, incline;
    public Vector3 sp, max, crashground, checkpoint, fwdtracking, checkpoint2;
    public bool spin, hey, trigboxes, crouch, slide, bellyflop, bellyonoff, bouncing,iscontact, Ltrigger, Rtrigger, Htrigger, tracker, dtimer, CP, finished, isrotup;
    Renderer rend;
    public Spinboxing sboxes, sboxes2, sboxes3, sboxes4, sboxes5, sboxes6, sboxes7,sboxes8;
    
    private float posi, maxjptimer, higher, crouchspeed, slidetimermax, slidex, slidez, bellytimer, momentumcancel, xspeed, zspeed, negxspeed, negzspeed, leftspeed, rightspeed, negLspeed, negRspeed, ax, az, aw;
    public Rigidbody rb, rb2;
    private BoxCollider crashcol;
    private MeshRenderer meshrend;
    public Vector3 relvel;
    public Quaternion Rotcheckpoint, aaa;
    public Vector3 ford;
    public float oldx;
    Transform ft;
    public Vector3 pgv; 


    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "explosion")
        {
            dtimer = true;
            Cpm.dtimer = true;

        }//if (IsGrounded2(crashground) && col.gameObject.name == "Crate")
        // Destroy(col.gameObject);



        if (col.gameObject.tag == "CP" && col.gameObject.transform.position.y <= transform.localPosition.y && sp.y < 0.0f)
        {
            bouncing = true;
            checkpoint = col.transform.position;
            checkpoint2 = col.transform.position;
            cratecounter++;
            cpyaxis = yaxis;
            camera.cpyaxis = camera.Yaxis;
            Destroy(col.gameObject);
            Cpm.zeroed();

        }

        if(col.gameObject.name == "End Level" && col.gameObject.transform.position.y <= transform.localPosition.y)
        {
            finished = true;
            rb.isKinematic = true;
        }    

        if (col.gameObject.tag == "CP" && spin == true || col.gameObject.tag == "CP" && slide == true || col.gameObject.tag == "CP" && bellyflop == true)
        {
            checkpoint = col.transform.position;
            checkpoint2 = col.transform.position;
            cpyaxis = yaxis;
            camera.cpyaxis = camera.Yaxis;
            Destroy(col.gameObject);
            cratecounter++;
            Cpm.zeroed();
        }

            /*if (col.gameObject.tag == "CagedC" && col.gameObject.transform.position.y <= transform.localPosition.y)
            {
                bouncing = true;
                CCr.bounces -= 1;
            }*/


            if (col.gameObject.name == "spinbox")
        {
            Physics.IgnoreCollision(spinbox.GetComponent<Collider>(), GetComponent<Collider>());
            
        }
       

        
    }

    private void OnCollisionExit(Collision col)
    {
        //if (col.gameObject.name == "Crate")
          //  bouncing = false;
    }

        void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        crashcol = GetComponent<BoxCollider>();
        meshrend = GetComponent<MeshRenderer>();
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        Ps.PosCrash = transform.position;
        camera = GameObject.Find("Main Camera").GetComponent<LinearCamera>();
        pathg = GameObject.Find("Pathguider");
        PG = pathg.GetComponent<Pathguide>();
        maxspeed = 10.0f;
        acceleration = 10.0f;
        accelLR = acceleration * 2;
        accelUD = acceleration * 2;
        momentumcancel = 10.0f;
        crouchspeed = 1.0f;
        spin = false;
        spinlimit = 0.8f;
        hey = false;
        trigboxes = true;
        maxjptimer = 0.005f;
        jumptimer = maxjptimer;
        slidetimermax = 1.0f;
        slidetimer = slidetimermax;
        bellytimer = 0.7f;
        slidex = 60.0f;
        slidez = 60.0f;
        higher = 0.0f;
        bouncing = false;
        crouch = false;
        slide = false;
        bellyflop = false;
        bellyonoff = false;
        fruits = 0;
        wfts = GameObject.Find("Text").GetComponent<Text>() as Text;
        cratecounter = 0;
        crte = GameObject.Find("Text2").GetComponent<Text>() as Text;
        crteT = GameObject.Find("Text3").GetComponent<Text>() as Text;
        lives = 4;
        cagebounces = 5;
        fruity = GameObject.FindGameObjectsWithTag("WFruit");
        wumpatimer = 1.0f;
        crystal = GameObject.FindGameObjectWithTag("Crystal");
        gem = GameObject.FindGameObjectWithTag("Gem");
        Ltrigger = false;
        Rtrigger = false;
        Htrigger = false;
        FP = fp.GetComponent<FollowPla>();
        dtimer = false;
        deathtimer = 5.0f;
        checkpoint = transform.position;
        checkpoint2 = pathg.transform.position;
        cratetotal = Cpm.crategr.Length + Cpm.questiongr.Length + Cpm.cagedgr.Length + Cpm.arrowgr.Length + Cpm.tntgr.Length + Cpm.nitrogr.Length + Cpm.cpgr.Length;
        finished = false;
        xspeed = 0.0f;
        zspeed = 0.0f;
        negxspeed = 0.0f;
        negzspeed = 0.0f;
        leftspeed = 0.0f;
        rightspeed = 0.0f;
        negLspeed = 0.0f;
        negRspeed = 0.0f;
        aaa = transform.rotation;
        ax = transform.rotation.x;
        ogyaxis = transform.rotation.y;
        yaxis = 0.0f;
        cpyaxis = yaxis;
        az = transform.rotation.z;
        aw = transform.rotation.w;
        Rotcheckpoint = new Quaternion(0,transform.rotation.y, 0, transform.rotation.w);
    }

    void FixedUpdate()
    {
        isrotup = IsCamForw(transform.position);
        
        ford = Vector3.forward;
        fruity = GameObject.FindGameObjectsWithTag("WFruit");
        ghost = GameObject.FindGameObjectsWithTag("ghost");
        sp = transform.InverseTransformDirection(rb.velocity);
        
        crashground = transform.position;
        PG.rb.velocity = pathg.transform.TransformDirection(new Vector3(0,0, sp.z));
        PG.yaxis = yaxis;
        if(FollowPla.caughtup == false && PG.Istarget(pathg.transform.position, Vector3.forward))
        {
            pathg.transform.Translate((Vector3.forward * Time.deltaTime) * 10);
        }
        if(FollowPla.caughtup == false && PG.Istarget(pathg.transform.position, -Vector3.forward))
        {
            pathg.transform.Translate((-Vector3.forward * Time.deltaTime) * 10);
        }
        if(FollowPla.caughtup == true)
        {
            pathg.transform.Translate(Vector3.forward * 0);
        }
        //PG.rb.velocity = PG.rb.GetRelativePointVelocity(pathg.transform.position);
        //PG.rb.velocity = new Vector3(PG.rb.velocity.x, 0, sp.z);

        wfts.text = fruits.ToString("fruits: 00");
        crte.text = cratecounter.ToString("crates: 00");
        crteT.text = cratetotal.ToString("/00");
        if (PauseScreen.isRestart)
        {
            cratecounter = 0;
            fruits = 0;
        }
        if (dtimer == true)
        {
            deathtimer -= Time.deltaTime;
            meshrend.enabled = false;
            rb.isKinematic = true;

        }
        if (deathtimer < 0.0f)
        {
            dtimer = false;
            deathtimer = 5.0f;
            transform.position = checkpoint;
            pathg.transform.position = checkpoint2;
            meshrend.enabled = true;
            rb.isKinematic = false;
            yaxis = cpyaxis;
            camera.Yaxis = camera.cpyaxis;
            camera.axishold = camera.Yaxis;

        }

        if (bouncing == true)
        {
            
            rb.AddRelativeForce(new Vector3(0.0f, 10.0f * 30.0f + higher, 0.0f));
            
        }
        if (bouncing == true && sp.y > 0.5f)
            bouncing = false;
        if (IsGrounded(crashground))
            bouncing = false;

        

        foreach (GameObject frty in fruity)
        {
            if(frty.GetComponent<Wumpa>() == null)
                frty.AddComponent<Wumpa>();
            if (frty != null && ((transform.localPosition.x >= frty.transform.position.x - 1.0 && transform.localPosition.x <= frty.transform.position.x + 1.0) && (transform.localPosition.z >= frty.transform.position.z - 1.0 && transform.localPosition.z <= frty.transform.position.z + 1.0) && (transform.localPosition.y >= frty.transform.position.y - 1.0 && transform.localPosition.y <= frty.transform.position.y + 1.0)))
            {
                Destroy(frty);
                fruits++;
            }
        }

        foreach (GameObject gh in ghost)
        {
            Physics.IgnoreCollision(gh.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (GameObject.Find("Crystal") != null)
            crystal = GameObject.Find("Crystal");
        if (GameObject.Find("Gem") != null)
            gem = GameObject.Find("Gem");

        if (crystal != null && (transform.localPosition.x >= crystal.transform.position.x - 1.5 && transform.localPosition.x <= crystal.transform.position.x + 1.5) && (transform.localPosition.z >= crystal.transform.position.z - 1.5 && transform.localPosition.z <= crystal.transform.position.z + 1.5) && (transform.localPosition.y >= crystal.transform.position.y - 1.5 && transform.localPosition.y <= crystal.transform.position.y + 1.5)){
            Destroy(crystal);
            Ps.crystal = 1;
        }
        if (gem != null && (transform.localPosition.x >= gem.transform.position.x - 1.0 && transform.localPosition.x <= gem.transform.position.x + 1.0) && (transform.localPosition.z >= gem.transform.position.z - 1.0 && transform.localPosition.z <= gem.transform.position.z + 1.0) && (transform.localPosition.y >= gem.transform.position.y - 1.0 && transform.localPosition.y <= gem.transform.position.y + 1.0))
        {
            Destroy(gem);
            Ps.gem = 1;
        }
        /*sboxes.meshrend2.enabled = false;
        sboxes2.meshrend2.enabled = false;
        sboxes3.meshrend2.enabled = false;
        sboxes4.meshrend2.enabled = false;*/
        //---------------------------------------------------
        sboxes.crashcol2.isTrigger = trigboxes;
        sboxes2.crashcol2.isTrigger = trigboxes;
        sboxes3.crashcol2.isTrigger = trigboxes;
        sboxes4.crashcol2.isTrigger = trigboxes;
        sboxes5.crashcol2.isTrigger = trigboxes;
        sboxes6.crashcol2.isTrigger = trigboxes;
        sboxes7.crashcol2.isTrigger = trigboxes;
        sboxes8.crashcol2.isTrigger = trigboxes;
        posi = crash.transform.position.y;
        if (!(IsGrounded(crashground)))
            jumptimer -= Time.deltaTime;
        else
            jumptimer = maxjptimer;
        if (jumptimer <= 0.0f)
            rb.AddRelativeForce(new Vector3(0.0f, -18.0f, 0.0f));
        //-----------------------------------------------------------------------------left & right velocity
        if (dtimer == false && finished == false)
        {
            if (Math.Abs(sp.x) > maxspeed && sp.x > 0)
            {
                rb.AddRelativeForce(new Vector3(-9.0f, 0.0f, 0.0f));
                //acceleration -= 0.50f;
            }
            if (!IsGrounded(crashground) && Math.Abs(sp.x) > maxspeed - 1.0f && sp.x > 0)
                rb.AddRelativeForce(new Vector3(-200.0f, 0.0f, 0.0f));
            if (Math.Abs(sp.x) > maxspeed && sp.x < 0)
            {
                rb.AddRelativeForce(new Vector3(9.0f, 0.0f, 0.0f));
                //acceleration -= 0.50f;
            }
            if (!IsGrounded(crashground) && Math.Abs(sp.x) > maxspeed - 1.0f && sp.x < 0)
                rb.AddRelativeForce(new Vector3(200.0f, 0.0f, 0.0f));
            if (sp.x > -8.0f && sp.x < 8.0f)
                accelLR = acceleration * 2;
            else if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
                accelLR = 0.0f;
            else
                accelLR = 0.0f;
            if (sp.x > 0.01f && sp.x < 9.0f && !Input.GetKey("right"))
                rb.AddRelativeForce(new Vector3(-30.0f, 0.0f, 0.0f));
            if (sp.x < -0.01f && sp.x > -9.0f && !Input.GetKey("left"))
                rb.AddRelativeForce(new Vector3(30.0f, 0.0f, 0.0f));

            //-----------------------------------------------------------------------------Up & down velocity

                if (Math.Abs(sp.z) > maxspeed && sp.z > 0)
                {
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -9.0f));
                    //acceleration -= 0.50f;
                }
                if (!IsGrounded(crashground) && Math.Abs(sp.z) > maxspeed - 1.0f && sp.z > 0)
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -50.0f));
                if (Math.Abs(sp.z) > maxspeed && sp.z < 0)
                {
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 9.0f));
                    //acceleration -= 0.50f;
                }
                if (!IsGrounded(crashground) && Math.Abs(sp.z) > maxspeed - 1.0f && sp.z < 0)
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 50.0f));
                if (sp.z > -8.0f && sp.z < 8.0f)
                    accelUD = acceleration * 2;
                else if (Input.GetKeyUp("up") || Input.GetKeyUp("down"))
                    accelUD = 0.0f;
                else
                    accelUD = 0.0f;
                if (sp.z > 0.01f && sp.z < 9.0f && !Input.GetKey("up"))
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -30.0f));
                if (sp.z < -0.01f && sp.z > -9.0f && !Input.GetKey("down"))
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 30.0f));
            
            
        }
        //---------------------------------------------------------------------------------------------------------------

        if(spin == true)
        {
            spinlimit -= Time.deltaTime;
            crashcol.size = new Vector3(1.5f, 1.5f, 1.5f);
            //trigboxes = false;
            //crashcol.isTrigger = true;
        }

        /*if (spinlimit < 0)
        {
            spin = false;
            hey = true;
            spinlimit = 5.8f;
            rend.material.SetColor("_Color", new Color(1.0f, 0.5f, 0.0f));
            crashcol.size = new Vector3(0.0f, 0.0f, 0.0f);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Rotcheckpoint;
        transform.rotation = Quaternion.Euler(new Vector3(0, yaxis, 0));
        if (Ps.timelimit <= 0.1f)
            transform.position = Ps.PosCrash;
        fwdtracking = transform.forward;
        spinbox.transform.localPosition = new Vector3(crash.transform.position.x + 0.6f, crash.transform.position.y, crash.transform.position.z + 0.0f);
        spinbox2.transform.localPosition = new Vector3(crash.transform.position.x - 0.6f, crash.transform.position.y, crash.transform.position.z + 0.0f);
        spinbox3.transform.localPosition = new Vector3(crash.transform.position.x + 0.0f, crash.transform.position.y, crash.transform.position.z + 0.6f);
        spinbox4.transform.localPosition = new Vector3(crash.transform.position.x + 0.0f, crash.transform.position.y, crash.transform.position.z - 0.6f);
        spinbox5.transform.localPosition = new Vector3(crash.transform.position.x + 0.45f, crash.transform.position.y, crash.transform.position.z + 0.45f);
        spinbox6.transform.localPosition = new Vector3(crash.transform.position.x - 0.45f, crash.transform.position.y, crash.transform.position.z + 0.45f);
        spinbox7.transform.localPosition = new Vector3(crash.transform.position.x - 0.45f, crash.transform.position.y, crash.transform.position.z - 0.45f);
        spinbox8.transform.localPosition = new Vector3(crash.transform.position.x + 0.45f, crash.transform.position.y, crash.transform.position.z - 0.45f);
        //-------------------------------------------------------------------------------------------------moving
        if (PauseScreen.isPaused == false && finished == false)
        {

            if (slide == false && bellyflop == false && bellyonoff == false && dtimer == false)
            {
               
                    
                    if (Math.Abs(sp.x) > 12.0f || Math.Abs(sp.z) > 12.0f)
                         momentumcancel = 70.0f;
                     else
                         momentumcancel = 10.0f;
                    
                    if (Input.GetKey("up"))
                    {
                        rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (acceleration + accelUD) * crouchspeed));
                        //PG.rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (acceleration + accelUD) * crouchspeed));
                }
                    if (Input.GetKeyUp("up"))
                    {  
                        rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (-acceleration * momentumcancel) * crouchspeed));
                        //PG.rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (-acceleration * momentumcancel) * crouchspeed));

                }
                    if (Input.GetKey("down"))
                    {
                        rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -(acceleration + accelUD) * crouchspeed));
                        //PG.rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -(acceleration + accelUD) * crouchspeed));
                }
                    if (Input.GetKeyUp("down"))
                    {               
                        rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (acceleration * momentumcancel) * crouchspeed));
                        //PG.rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (acceleration * momentumcancel) * crouchspeed));
                }

                    if (Input.GetKey("left"))
                    {
                        rb.AddRelativeForce(new Vector3((-acceleration - accelLR) * crouchspeed, 0.0f, 0.0f));
                    }
                    if (Input.GetKeyUp("left"))
                    {
                        rb.AddRelativeForce(new Vector3((acceleration * momentumcancel) * crouchspeed, 0.0f, 0.0f));
                    }
                    if (Input.GetKey("right"))
                    {
                        rb.AddRelativeForce(new Vector3((acceleration + accelLR) * crouchspeed, 0.0f, 0.0f));
                    }
                    if (Input.GetKeyUp("right"))
                    {
                        rb.AddRelativeForce(new Vector3((-acceleration * momentumcancel) * crouchspeed, 0.0f, 0.0f));
                    }       
                
            }
            //---------------------------------------------------------------------------------------------------------------------
            if (Input.GetKeyDown("space") && IsGrounded(crashground) && bellyonoff == false && dtimer == false) //jumping
            {
                if (crouch == false && slide == false)
                    rb.AddRelativeForce(new Vector3(0.0f, 10.0f * 50.0f, 0.0f));
                else
                    rb.AddRelativeForce(new Vector3(0.0f, (10.0f * 50.0f) + ((10.0f * 50.0f) * 0.25f), 0.0f));
                slide = false;
                slidetimer = slidetimermax;
                slidex = 60.0f;
                slidez = 60.0f;
                //Debug.Log("Jump");
            }
            if (slide == true && Input.GetKeyDown("space"))
                rb.AddRelativeForce(new Vector3(0.0f, (10.0f * 50.0f) + ((10.0f * 50.0f) * 0.25f), 0.0f));
            if (bouncing == false)
                higher = 0.0f;
            if (Input.GetKey("space") && bouncing == true)
                higher = 60.0f;
            if (Input.GetKeyUp("space"))
                higher = 0.0f;
            //----------------------------------------------------------------------------------------------------------------------
            if (Input.GetKeyDown("f")) //spinning
            {
                spin = true;
                rend.material.SetColor("_Color", Color.yellow);
                trigboxes = false;
            }
            if (spinlimit < 0.0f)
            {
                spin = false;
                trigboxes = true;
                spinlimit = 0.8f;
                rend.material.SetColor("_Color", new Color(1.0f, 0.5f, 0.0f));
                crashcol.size = new Vector3(0.0f, 0.0f, 0.0f);

            }
            //--------------------------------------------------------------------------------------------------------------------
            if (Input.GetKey("d") && (Math.Abs(sp.x) >= 0 && Math.Abs(sp.x) < 1.6f) && (Math.Abs(sp.z) >= 0 && Math.Abs(sp.z) < 1.6f) && IsGrounded(crashground))
            {
                crouch = true;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
                crouchspeed = 0.0625f;
            }

            if (Input.GetKey("d") && IsGrounded(crashground))
                maxspeed = 1.5f;
            if (!IsGrounded(crashground))
            {
                crouch = false;
                maxspeed = 10.0f;
                crouchspeed = 1;
            }
            if (Input.GetKeyUp("d"))
            {
                crouch = false;
                transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
                crouchspeed = 1;
                maxspeed = 10.0f;
            }
            if ((Input.GetKeyDown("d") && Math.Abs(sp.x) > 1.0f) && IsGrounded(crashground) && slide == false || (Input.GetKeyDown("d") && Math.Abs(sp.z) > 1.0f) && IsGrounded(crashground) && slide == false)
            {
                slide = true;
                trigboxes = false;
            }
            if (slidetimer <= 0.5f)
            {
                slide = false;
                slidetimer = slidetimermax;
                slidex = 60.0f;
                slidez = 60.0f;
                trigboxes = true;
            }
            if (slide == true && slidetimer > 0)
            {
                slidetimer -= Time.deltaTime;
                if (slidetimer <= 0.7f && slidetimer > 0.5f)
                {
                    if (Math.Abs(sp.x) > 3.0f)
                        slidex = -100.0f;
                    if (Math.Abs(sp.z) > 3.0f)
                        slidez = -100.0f;
                }
                if (sp.x > 0.0f && sp.x < 3.0f || sp.x < 0.0f && sp.x > -3.0f)
                    slidex = 0.0f;
                if (sp.z > 0.0f && sp.z < 3.0f || sp.z < 0.0f && sp.z > -3.0f)
                    slidez = 0.0f;
                if (sp.x > 3.0f)
                    rb.AddRelativeForce(slidex, 0.0f, 0.0f);
                else
                    rb.AddRelativeForce(-slidex, 0.0f, 0.0f);
                if (sp.z > 3.0f)
                    rb.AddRelativeForce(0.0f, 0.0f, slidez);
                else
                    rb.AddRelativeForce(0.0f, 0.0f, -slidez);
            }
            //------------------------------------------------------------------------------------bodyslam

            if (!IsGrounded(crashground) && Input.GetKeyDown("b") && sp.y >= 1.0f)
            {
                bellyflop = true;
                bellyonoff = true;
            }
            if (IsGrounded(crashground) && bellyonoff == true)
            {
                bellyflop = false;
                bellytimer -= Time.deltaTime;
            }
            if (bellytimer <= 0.0f)
            {
                bellyonoff = false;
                bellytimer = 0.7f;
            }

            if (bellyflop == true)
            {
                if (sp.x > 1.0f)
                    rb.AddRelativeForce(new Vector3((-acceleration * 2) * crouchspeed, 0.0f, 0.0f));
                if (sp.x < -1.0f)
                    rb.AddRelativeForce(new Vector3((acceleration * 2) * crouchspeed, 0.0f, 0.0f));
                if (sp.z > 1.0f)
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (-acceleration * 2) * crouchspeed));
                if (sp.z < -1.0f)
                    rb.AddRelativeForce(new Vector3(0.0f, 0.0f, (acceleration * 2) * crouchspeed));
            }
        }

    }
    
    public bool IsGrounded(Vector3 ground)
    {
        return Physics.Raycast(ground, -Vector3.up, 0.65f, layer);
    }

    public bool IsGrounded2(Vector3 ground)
    {
        return Physics.Raycast(ground, -Vector3.up, 0.55f, bounce);
    }
    public bool IsForward(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 100.55f, enemy);
    }
    public bool IsCamForw(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 10.55f, incline);
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