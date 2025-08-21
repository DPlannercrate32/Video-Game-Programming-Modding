using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCamera : MonoBehaviour {
    public GameObject crash, onetrig, fp, fp2, fp3, pathg;
    public Crash_CPHY crash2;
    public FollowPla FP, FP2, FP3;
    public CTriggers CT;
    Vector3 v1, v2;
    public float offsetz, offsetx, zcos, xsin, angleclamp, timer, timer2, maxtimer, rotincL, Yaxis, offsetzpg, axishold, cpyaxis;
    public bool backforwd, Rtrigger, Ltrigger, check, timeout, right, left, work, nochangeold, nochangeoldH;
    public LayerMask layer;
    public Quaternion checkpoint;
    public int leftpts;
    public float diff, x1, y1, z1, scale, scaleH, oldzcrash, oldycrash, newzcrash, newycrash, posleft, degree, hdegree, oldDandi, oldy, j;
    public Pathguide PG;
    float oldrotz, oldrotx;



    // Use this for initialization
    void Start () {
        crash2 = crash.GetComponent<Crash_CPHY>();
        FP = fp.GetComponent<FollowPla>();
        //transform.position = crash.transform.position;
        v1 = transform.position;
        offsetz = 4;
        offsetx = 0;
        backforwd = true;
        transform.Rotate((Vector3.right * 10.0f));
        Rtrigger = false;
        Rtrigger = false;
        CT = onetrig.GetComponent<CTriggers>();
        //transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
        angleclamp = 1.0f;
        checkpoint = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w); 
        maxtimer = 0.5f;
        timer = 0.0f;
        timeout = false;
        right = false;
        left = false;
        leftpts = 0;
        rotincL = 1;
        //transform.Rotate(new Vector3(0.0f, -45.0f, 0.0f));
        fp2 = GameObject.Find("RotateblockR");
        fp3 = GameObject.Find("Centerblock");
        FP3 = fp3.GetComponent<FollowPla>();
        diff = 1.113f;
        Yaxis = 0.0f;
        cpyaxis = Yaxis;
        axishold = Yaxis;
        scale = CT.transform.localScale.z;
        scaleH = CT.transform.localScale.z;
        nochangeold = false;
        nochangeoldH = false;
        posleft = 0.001f;
        pathg = GameObject.Find("Pathguider");
        PG = pathg.GetComponent<Pathguide>();
        offsetzpg = pathg.transform.position.z - transform.position.z;
        degree = 45;
        oldrotx = transform.rotation.x;
        oldrotz = transform.rotation.z;
        oldDandi = 3.4f;
        oldy = oldDandi;
    }
    void Update()
    {
        transform.position = new Vector3(pathg.transform.position.x - offsetx, oldDandi, pathg.transform.position.z - offsetz);
        zcos = Mathf.Cos(Yaxis * Mathf.Deg2Rad);
        xsin = Mathf.Sin(Yaxis * Mathf.Deg2Rad);


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        check = IsRight(transform.position);
        //transform.position = new Vector3(transform.position.x, 3.4f, transform.position.z);
        x1 = transform.position.x; y1 = transform.position.y; z1 = transform.position.z;
        
        /*if (crash2.sp.z > 0 && ((x1 >= crash.transform.position.x - 20 && x1 <= crash.transform.position.x + 20.0f) && (z1 >= crash.transform.position.z - 20 && z1 <= crash.transform.position.z + 20.0f) && (y1 >= crash.transform.position.y - 50 && y1 <= crash.transform.position.y + 50.0f)))
        {
            diff = 1.101f;
            work = false;
        }
        else if (crash2.sp.z < 0 && ((x1 >= crash.transform.position.x - 20 && x1 <= crash.transform.position.x + 20.0f) && (z1 >= crash.transform.position.z - 20 && z1 <= crash.transform.position.z + 20.0f) && (y1 >= crash.transform.position.y - 50 && y1 <= crash.transform.position.y + 50.0f)))
        {
            diff = 1.1008f;
            work = false;
        }
        else
        {
            diff = 1.1008f;
            work = true;
        }*/
        /*
        if (crash2.sp.z > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z * diff);
            //fp2.transform.Translate(Vector3.right * Time.deltaTime * crash2.sp.z);
            //fp3.transform.Translate(Vector3.right * Time.deltaTime * crash2.sp.z);
            //fp.transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z);
        }
        if (crash2.sp.z < 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z * diff);
            //fp2.transform.Translate(Vector3.right * Time.deltaTime * crash2.sp.z);
            //fp3.transform.Translate(Vector3.right * Time.deltaTime * crash2.sp.z);
            //fp.transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z);
        }*/


        /*if (crash2.Ltrigger == true)
        {
            if (crash2.sp.z > 1.0f && rotincL > -45.0f)
            {
                transform.rotation = checkpoint;
                transform.Rotate(new Vector3(0.0f, Mathf.Clamp(rotincL , -45.0f, 0.0f) + 7, 0.0f));
                rotincL--;
            }
            if(crash2.sp.z < -1.0f && rotincL < 0.0f)
            {
                transform.rotation = checkpoint;
                transform.Rotate(new Vector3(0.0f, Mathf.Clamp(rotincL , -45.0f, 0.0f), 0.0f));
                rotincL++;
            }
            if (rotincL >= 0.0f || rotincL <= -45.0f)
                crash2.Ltrigger = false;
        }*/
        if (crash2.deathtimer < 0.1f)
            transform.rotation = checkpoint;
        /*if(crash2.sp.z > 1.0f)
        {
            
            transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z);
        }
        if(crash2.sp.z < -1.0f)
            transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z);*/


        /*if (crash2.Rtrigger == true && crash2.sp.z > 0.0f) {
         
            transform.Rotate((Vector3.up * Time.deltaTime) * 35.0f);

        }
        if (crash2.Rtrigger == true && crash2.sp.z < 0.0f)
        {
            transform.Rotate((Vector3.down * Time.deltaTime) * 35.0f);
        }*/

        if (crash2.Ltrigger == true) //&& timeout == false)// && crash2.sp.z > 0.8f )
        {
            if (crash2.sp.z > 0)
            {
                right = true;
                left = false;
            }
            else
            {
                right = false;
                left = true;
            }
            /*if (right == true)
            {
                timer2 = maxtimer;
                timer -= 0.1f;
            }
            if (left == true)
            {
                timer = maxtimer;
                timer2 -= 0.1f;
            }*/
            if (nochangeold == false)
            {
                if(CTriggers.positive == true)
                    oldzcrash = crash.transform.position.z;
                else
                    oldzcrash = crash.transform.position.z - scale;
                nochangeold = true;
            }
            newzcrash = crash.transform.position.z - oldzcrash;
            /*if (Mathf.Abs(newzcrash) >= scale*0 && Mathf.Abs(newzcrash) <= scale*0.5f)
                Yaxis = -45 * 0.5f;
            if (Mathf.Abs(newzcrash) >= scale * 0.5f && Mathf.Abs(newzcrash) <= scale)
                Yaxis = -45;*/

            for (float i = 0.00f; i <= 1.00f; i += 0.01f)
            {
                if (Mathf.Abs(newzcrash) >= scale * i && Mathf.Abs(newzcrash) <= scale * (i + 0.01f))
                {
                    if (CTriggers.negative == false)
                        Yaxis = axishold + (-degree * i);
                    else
                        Yaxis = axishold + (-degree * i); 

                    break;
                }
            }
            transform.rotation = checkpoint;
            
            
            fp3.transform.position = new Vector3(fp3.transform.position.x, fp3.transform.position.y, fp3.transform.position.z);
            //Yaxis = -5;

            //transform.Rotate(new Vector3(0, Yaxis, 0));

            transform.rotation = Quaternion.Euler(new Vector3(0, Yaxis, 0));
            transform.Rotate(new Vector3(25, 0, 0));
            crash2.yaxis = Yaxis;
            FP3.Yaxis = Yaxis;
            //fp3.transform.position = new Vector3(fp3.transform.position.x + (Yaxis * 0.01f), fp3.transform.position.y, fp3.transform.position.z);
        }

        if (crash2.Rtrigger == true) //&& timeout == false)// && crash2.sp.z > 0.8f )
        {
            if (crash2.sp.z > 0)
            {
                right = true;
                left = false;
            }
            else
            {
                right = false;
                left = true;
            }
            
            if (nochangeold == false)
            {
                if (CTriggers.positive == true)
                    oldzcrash = crash.transform.position.z;
                else
                    oldzcrash = crash.transform.position.z - scale;
                nochangeold = true;
            }
            newzcrash = crash.transform.position.z - oldzcrash;
            /*if (Mathf.Abs(newzcrash) >= scale*0 && Mathf.Abs(newzcrash) <= scale*0.5f)
                Yaxis = -45 * 0.5f;
            if (Mathf.Abs(newzcrash) >= scale * 0.5f && Mathf.Abs(newzcrash) <= scale)
                Yaxis = -45;*/

            for (float i = 0.00f; i <= 1.00f; i += 0.01f)
            {
                if (Mathf.Abs(newzcrash) >= scale * i && Mathf.Abs(newzcrash) <= scale * (i + 0.01f))
                {
                    if (CTriggers.negative == false)
                        Yaxis = axishold + (degree * i);
                    else
                        Yaxis = axishold + (degree * i);

                    break;
                }
            }
            transform.rotation = checkpoint;


            fp3.transform.position = new Vector3(fp3.transform.position.x, fp3.transform.position.y, fp3.transform.position.z);
            //Yaxis = -5;

            //transform.Rotate(new Vector3(0, Yaxis, 0));
            transform.rotation = Quaternion.Euler(new Vector3(0, Yaxis, 0));
            transform.Rotate(new Vector3(25, 0, 0));
            crash2.yaxis = Yaxis;
            FP3.Yaxis = Yaxis;
        }

        if (crash2.Htrigger == true) //&& timeout == false)// && crash2.sp.z > 0.8f )
        {
            
            if (nochangeoldH == false)
            {
                if (CTriggers.positive == true)
                {
                    oldycrash = crash.transform.position.z;
                    //oldy = transform.position.y;
                }
                else
                    oldycrash = crash.transform.position.z - scaleH;
                nochangeoldH = true;
            }
            newycrash = crash.transform.position.z - oldycrash;


            for (j = 0.00f; j <= 1.00f; j += 0.01f)
            {
                if (Mathf.Abs(newycrash) >= scaleH * j && Mathf.Abs(newycrash) <= scaleH * (j + 0.01f))
                {
                    
                    
                        if (CTriggers.negative == false)
                        {
                            oldDandi = (((hdegree + oldy) - oldy) * j) + oldy;
                        //transform.position = new Vector3(transform.position.x, oldDandi, transform.position.z);
                        
                        }
                        else
                        oldDandi = (((hdegree + oldy) - oldy) * j) + oldy; 

                }
                    
                
            }
            
        }




        if (/*timer > 0.0f && timer < maxtimer &&*/  left == false && Yaxis < 0 ) //&& crash2.sp.z > 0.5f)
        {
            //timer2 = maxtimer;
            //timer -= Time.deltaTime;
            //transform.Rotate(new Vector3(0, 0, 0));
            //Yaxis--;
            //transform.Rotate(new Vector3(0, Yaxis, 0));

            //crash.transform.Rotate(new Vector3(0, 0, 0));
            //crash.transform.Rotate(new Vector3(0, Yaxis, 0));
            //transform.Rotate(0, -Time.deltaTime * 15.0f, 0);          
            //crash2.yaxis -= Time.deltaTime * 15.0f;
            //fp2.transform.Rotate(0, -Time.deltaTime * 15.0f, 0);
            //fp3.transform.Rotate(0, -Time.deltaTime * 15.0f, 0);
            //fp3.transform.position = new Vector3(fp3.transform.position.x - 0.09f, fp3.transform.position.y, fp3.transform.position.z - 0.07f);
            
        }
        /*if (timer2 > 0.0f && timer2 < maxtimer && right == false && crash2.sp.z < -0.5f)
        {
            timer = maxtimer;
            timer2 -= Time.deltaTime;
            transform.Rotate(0, Time.deltaTime * 15.0f, 0);           
            crash2.yaxis += Time.deltaTime * 15.0f;
            
            //fp2.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
            //fp3.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
            //fp3.transform.position = new Vector3(fp3.transform.position.x + 0.09f, fp3.transform.position.y, fp3.transform.position.z + 0.07f);
            //fp.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
            //fp.transform.position = new Vector3(fp.transform.position.x + 0.09f, fp.transform.position.y, fp.transform.position.z + 0.07f);
        }*/

        /*if (timer < 0.0f || timer2 < 0.0f)
        {
            if (right == true)
            {
                transform.Rotate(0, Time.deltaTime * 15.0f, 0);
                crash2.yaxis += Time.deltaTime * 15.0f;
                //fp2.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
                //fp3.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
                //fp.transform.Rotate(0, Time.deltaTime * 15.0f, 0);
            }
            if (left == true)
            {
                transform.Rotate(0, -Time.deltaTime * 15.0f, 0);               
                crash2.yaxis -= Time.deltaTime * 15.0f;
                //fp2.transform.Rotate(0, -Time.deltaTime * 15.0f, 0);
                //fp3.transform.Rotate(0, -Time.deltaTime * 15.0f, 0);
                //fp.transform.Rotate(0, -Time.deltaTime * 15.0f, 0);
            }
            timer = maxtimer;
            timer2 = maxtimer;
            //crash2.Ltrigger = false;
        }*/
        /*if (crash2.Ltrigger == true)// && crash2.sp.z < -0.8f)
        {
            transform.Rotate((Vector3.up * Time.deltaTime) * 100.0f);
            //crash2.Ltrigger = false;
        }*/
        //if (leftpts == 0)
            //transform.position = new Vector3(v1.x + 2.0f, 4.25f, crash.transform.position.z - offsetz);
        
        if (crash2.sp.x < -6.8f && FollowPla.b3 == true && (crash2.Ltrigger == false || crash2.Rtrigger == false))
        {
            transform.Rotate((Vector3.down * Time.deltaTime) * 15.0f);
        }
        if (crash2.sp.x > 6.8f && FollowPla.b3 == true && (crash2.Ltrigger == false || crash2.Rtrigger == false))
        {
            transform.Rotate((Vector3.up * Time.deltaTime) * 15.0f);
        }
        if (crash2.sp.y > 0.1f && !crash2.IsGrounded(crash2.crashground))
        {
            transform.Rotate((Vector3.left * Time.deltaTime) * 16.0f);
        }
        if (crash2.sp.y < -0.1f && !crash2.IsGrounded(crash2.crashground))
        {
            transform.Rotate((Vector3.right * Time.deltaTime) * 16.0f);
        }
        if(crash2.IsCamForw(crash.transform.position) && crash2.sp.z > 0)
        {
            transform.Rotate((Vector3.left * (Time.deltaTime * 10.5f)));
        }
        if (crash2.IsCamForw(crash.transform.position) && crash2.sp.z < 0)
        {
            transform.Rotate((Vector3.right * (Time.deltaTime * 10.5f)));
        }
        if (crash2.sp.z < -0.8f)
        {
            backforwd = false;
        }
        if (crash2.sp.z > 0.8f)
        {
            backforwd = true;
        }
        if(zcos >= 0)
        {
            if(offsetz < 3 * zcos)
            {
                offsetz = 3 * zcos;
            }

            if (offsetz > 4 * zcos)
            {
                offsetz = 4 * zcos;
            }
            if (backforwd == false && offsetz <= 4 * zcos) {
                offsetz += (0.1f * Time.deltaTime) * 7;
                //offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            }
            if(backforwd == true && offsetz >= 3 * zcos)
            {
                offsetz -= (0.1f * Time.deltaTime) * 7;
                //offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            }
        }
        if(zcos < 0)
        {
            if (offsetz > -3 * zcos)
            {
                offsetz = -3 * zcos;
            }

            if (offsetz < -4 * zcos)
            {
                offsetz = -4 * zcos;
            }
            if (backforwd == false && offsetz >= -4 * Mathf.Abs(zcos))
            {
                offsetz -= (0.1f * Time.deltaTime) * 7;
                //offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            }
            if (backforwd == true && offsetz <= -3 * Mathf.Abs(zcos))
            {
                offsetz += (0.1f * Time.deltaTime) * 7;
               // offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            }
        }
        if (xsin >= 0)
        {
            if (offsetx < 3 * xsin)
            {
                offsetx = 3 * xsin;
            }

            if (offsetx > 4 * xsin)
            {
                offsetx = 4 * xsin;
            }
            if (backforwd == false && offsetx <= 4 * xsin)
            {
                offsetx += (0.1f * Time.deltaTime) * 7;
                //offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            }
            if (backforwd == true && offsetx >= 3 * xsin)
            {
                offsetx -= (0.1f * Time.deltaTime) * 7;
                //offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            }
        }
        if (xsin < 0)
        {
            if (offsetx > -3 * xsin)
            {
                offsetx = 3 * xsin;
            }

            if (offsetx < -4 * xsin)
            {
                offsetx = 4 * xsin;
            }
            if (backforwd == false && offsetx >= -4 * Mathf.Abs(xsin))
            {
                offsetx -= (0.1f * Time.deltaTime) * 7;
                //offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            }
            if (backforwd == true && offsetx <= -3 * Mathf.Abs(xsin))
            {
                offsetx += (0.1f * Time.deltaTime) * 7;
                //offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            }
        }
        /*if (backforwd == false && offsetz <= 4)
        {
            offsetz += (0.1f * Time.deltaTime) * 7;
            offsetx += (0.1f * Time.deltaTime) * 7;
            offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            

        }
        if (backforwd == true && offsetz >= 3)
        {
            offsetz -= (0.1f * Time.deltaTime) * 7;
            offsetx -= (0.1f * Time.deltaTime) * 7;
            offsetx *= Mathf.Sin(Yaxis * Mathf.Deg2Rad);
            offsetz *= Mathf.Cos(Yaxis * Mathf.Deg2Rad);
            
        }*/


    }
    public bool IsRight(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 100.55f, layer);
    }
}
