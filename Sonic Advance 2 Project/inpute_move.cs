using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inpute_move : MonoBehaviour {
    public GameObject sonic, pthline;
    public LayerMask layer;
    public float accelspeed, accelspeed2, speedometer, speedometerUp, maxspeed, maxheight, seconds2, anglex, angley, timer, accelVerticle, x, y, CosQ1, CosQ2, CosQ3, CosQ4, CosD;
    private float accelHorizontal, /*accelVerticle,*/ maxseconds, maxseconds2, skidmaxseconds, skidseconds, maxspholder, seconds, accholder, maxrollspeed, revup, groundmulti, diff1, diff2;
    bool boostmode, rolling, crouch;
    public bool /*crouch,*/ timetrigger, spindash, rightleft, rightleft2, skid, touchPLine;
    public Vector3 sp, sonicground, angleground;
    private Rigidbody rb;
    

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SpringU")
        {
            //Destroy(col.gameObject);
            rb.AddForce(new Vector3(0.0f, 2 * (accelspeed2 * 10), 0.0f));
        }
      
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Pathline")
        {
            touchPLine = true;
            rb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Pathline")
        {
            touchPLine = false;
            rb.useGravity = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxspeed = 10.0f;
        maxheight = 3.0f;
        boostmode = false;
        maxseconds = 3.0f;
        maxseconds2 = 3.0f;
        skidmaxseconds = 2.0f;
        skidseconds = skidmaxseconds;
        maxspholder = 10.0f;
        accelspeed = 70.0f;
        accelspeed2 = 300.0f;
        accholder = 70.0f;
        anglex = 0.0f;
        angley = 0.0f;
        diff1 = 0.0f;
        diff2 = 0.0f;
        rolling = false;
        maxrollspeed = 30.0f;
        groundmulti = 1.0f;
        spindash = false;
        crouch = false;
        revup = 0.0f;
        rightleft = true;
        rightleft2 = true;
        skid = false;
        touchPLine = false;
    }

   
    void FixedUpdate()
    {
        Vector3 max = new Vector3(maxspeed, maxheight, 0.0f);
        sp = GetComponent<Rigidbody>().velocity;
        sonicground = transform.position; // new Vector3(transform.position.x, transform.position.y, transform.position.z);
//----------HANDLING SPEED & BOOST MODE-------------------------------------------------------------------------------------
        if (Math.Abs(sp.x) > 0.0f && Math.Abs(sp.x) < 3.0f)
        {
            maxspeed = maxspholder;
            accelspeed = accholder;
            boostmode = false;
        }
        if (Math.Abs(sp.x) > Math.Abs(max.x - 0.5f) && seconds >= 0 && boostmode == false && rolling == false)
        {
            seconds -= Time.deltaTime;

        }
        if(timetrigger == true)
            seconds2 -= Time.deltaTime;     
       
        if (seconds2 <= 0.0f)
        {
            timetrigger = false;
            seconds2 = maxseconds2;

        }
        if (Math.Abs(sp.x) < Math.Abs(max.x - 0.5f))
        {
            seconds = maxseconds;
            if(timetrigger == false)
                seconds2 = maxseconds2;
        }

        if (Math.Abs(sp.x) > Math.Abs(max.x - 0.5f) && seconds <= 0.0)
        {
            boostmode = true;
            maxspeed *= 2;
            accelspeed *= 2;
        }
//------------------keeps sonic in 2d plane-------------------------------------------------------------------------------
        if (sonic.transform.position.z > 0.09f)
            rb.AddForce(new Vector3(0.0f, 0.0f, -100.0f));
        if (sonic.transform.position.z < -0.09f)
            rb.AddForce(new Vector3(0.0f, 0.0f, 100.0f));
        if (sp.z > 0.05 && sp.z < 0.35)
            rb.AddForce(new Vector3(0.0f, 0.0f, -100.0f));
        if (sp.z > -0.05 && sp.z < -0.65)
            rb.AddForce(new Vector3(0.0f, 0.0f, 100.0f));

//---------------------MOVING LEFT OR RIGHT-------------------------------------------------------------------------------------------------
        if (!rolling && spindash == false && skid == false)
        {
            accelHorizontal = (Input.GetAxis("Horizontal") / 1.0f) * accelspeed;
            if( sp.x > 0.01f &&  sp.x < 3.6f || touchPLine == true && sp.x > 0.01f && sp.x < 100.6f)
                rb.AddForce(new Vector3(-30.0f, 0.0f, 0.0f));
            if (sp.x < -0.01f && sp.x > -3.6f || touchPLine == true && sp.x < -0.01f && sp.x > -100.6f)
                rb.AddForce(new Vector3(30.0f, 0.0f, 0.0f));
        }
        else
            accelHorizontal = 0.0f;
       
        if (Math.Abs(sp.x) > Math.Abs(max.x) && rolling == false)// && Math.Abs(sp.x) < Math.Abs(max.x + 1.0f) )&& sp.y >= -0.3f) //Checks if maximum speed is reached before keeping speed capped at that maximum speed.
        {
            accelHorizontal = (Input.GetAxis("Horizontal") / 1.0f) * 1.0f;
        }
//--------------------Jumping-------------------------------------------------------------------------------------------------- 
        /*if (/*IsGrounded(sonicground) && *//* Input.GetKeyDown("up") && crouch == false && skid == false)
            accelVerticle = 10 * accelspeed2;
        else
            accelVerticle = 0;

        if (IsGrounded(sonicground) == false && sp.y > 1) //Could be the issue.
            accelVerticle = -300;
        if (IsGrounded(sonicground))
            sp = new Vector3(sp.x, 0.0f, sp.z);*/

//--------------CROUCHING---------------------------------------------------------------------------------------------------------
        if (!IsGrounded(sonicground) || Math.Abs(sp.x) > 0.1f)
            crouch = false;
        if (crouch == true)
        {
            sonic.transform.localScale = new Vector3(0.35f, 0.7f, 0.7f);

            if (spindash == true)
                sonic.transform.Rotate(new Vector3(0.0f, 0.0f, -30.0f));
        }

//------------------------ROLLING------------------------------------------------------------------------------------------------
        if (rolling == true && Math.Abs(sp.x) > maxrollspeed)
            rb.AddForce(new Vector3(-30.0f, 0.0f, 0.0f));
        if (rolling == true && IsGrounded(sonicground) && sp.y < -0.05f)
        {
            rb.AddForce(new Vector3(30.0f, 0.0f, 0.0f));
        }
       
        if(rolling == true)
            sonic.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        if ((rolling == true && Math.Abs(sp.x) < 0.5f && timetrigger == false) || IsGrounded(sonicground) == false)
            rolling = false;
        if(rolling == false && crouch == false)
            sonic.transform.localScale = new Vector3(1.0253f, 1.0f, 1.0f);

//----------------------------READING ACCEL VARIABLE VALUES-------------------------------------------------------
       speedometer = accelHorizontal;
       speedometerUp = accelVerticle;

//--------------------------------SETS GRAVITY & ADDS FORCES-----------------------------------------------------------------
        Physics.gravity = new Vector3(0.0f, -19.8f, 0.0f);

        rb.AddForce(new Vector3(accelHorizontal, 0.0f, 0.0f));
    }

//--------------------------------UPDATE FUNCTION-----------------------------------------------------------------

    private void Update()
    {

        if (Input.GetKeyDown("down") && skid == false)           //ROLLING
        {
            rolling = true;
            if (Math.Abs(sp.x) >= 0.0f && Math.Abs(sp.x) < 0.1f && IsGrounded(sonicground))
                crouch = true;
        }
        if (Input.GetKeyUp("down") && skid == false)
        {
            crouch = false;
            spindash = false;
            rb.AddForce(new Vector3(revup, 0.0f, 0.0f));
            rolling = true;
        }
    //-------------------------------------------------------------------------------------------------------------------
        if (crouch == true && Input.GetKeyDown("up"))      //SPINDASHING
        {
            spindash = true;
            timetrigger = true;
            if(rightleft2 == true)
                revup = 150.0f * 20;
            if (rightleft2 == false)
                revup = -(150.0f * 20);
        }
        if (spindash == false)
            revup = 0.0f;

   //--------------------------------- CHECKING FOR LEFT OR RIGHT MOVEMENT-----------------------------------------------------------------------------------
        if (Input.GetKeyDown("right"))
            rightleft2 = true;
        if (Input.GetKeyDown("left"))
            rightleft2 = false;
        if (rightleft2 == true && sp.x >= 0.0f)
            rightleft = true;
        if (rightleft2 == false && sp.x <= 0.0f)
            rightleft = false;
    //-----------------------------------SKIDDING TIME-------------------------------------------------------------------
        if (Input.GetKeyDown("f") && rightleft2 == true && sp.x > 5.0f) {
            rb.AddForce(new Vector3(-1000*2, 0.0f, 0.0f));
            skid = true;
        }
        if (Input.GetKeyDown("f") && rightleft2 == true && sp.x >= 0.0f && sp.x < 5.0)
        {
            rb.AddForce(new Vector3(1000 * 2, 0.0f, 0.0f));
            skid = true;
        }
        if (Input.GetKeyDown("f") && rightleft2 == false && sp.x < -5.0)
        {
            rb.AddForce(new Vector3(1000 * 2, 0.0f, 0.0f));
            skid = true;
        }
        if (Input.GetKeyDown("f") && rightleft2 == false && sp.x <= 0.0 && sp.x > -5.0)
        {
            rb.AddForce(new Vector3(-1000 * 2, 0.0f, 0.0f));
            skid = true;
        }
        if(skid == true)
        {
            skidseconds -= Time.deltaTime;
        }
        if(skid == true && sp.x == 0.0f && skidseconds <= 0.0)
        {
            skid = false;
            skidseconds = skidmaxseconds;
        }

        if (IsGrounded(sonicground) &&  Input.GetKeyDown("up") && crouch == false && skid == false)
            accelVerticle = 10 * accelspeed2;
        else
            accelVerticle = 0;

        if (IsGrounded(sonicground) == false && sp.y > 1 && touchPLine == false) //Could be the issue.
            accelVerticle = -300;
        if (IsGrounded(sonicground))
            sp = new Vector3(sp.x, 0.0f, sp.z);
        rb.AddForce(new Vector3(0.0f, accelVerticle, 0.0f));
    }   

    public bool IsGrounded(Vector3 ground)
    {
        return Physics.Raycast(ground,-Vector3.up, 0.65f, layer);
    }

}
