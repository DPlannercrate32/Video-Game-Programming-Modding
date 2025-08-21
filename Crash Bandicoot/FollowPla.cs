using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPla : MonoBehaviour {
    public GameObject Crash, pathg;
    public Crash_CPHY crash2;
    public BoxCollider bx;
    public SphereCollider sp;
    public LayerMask layer, layer2;
    public bool b, b2, caughtup2;
    public static bool b3, caughtup, pos, neg;
    public float tx, ty, tz, tw, diff;
    public Quaternion Rotcheckpoint;
    public float Yaxis;



    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Crash")
            b3 = true;
        if (col.gameObject.name == "Pathguider")
        {
            caughtup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Crash")
            b3 = false;

        if (other.gameObject.name == "Pathguider")
        {
            caughtup = false;
        }
    }
    // Use this for initialization
    void Start () {
       Crash = GameObject.Find("Crash");
       crash2 = Crash.GetComponent<Crash_CPHY>();
        if (tag == "Centerblock")
        {
            sp = GetComponent<SphereCollider>();
            sp.isTrigger = true;
        }
        if (tag == "RightBlock" || tag == "LeftBlock" || tag == "Tracker")
        {
            bx = GetComponent<BoxCollider>();
            bx.isTrigger = true;
        }
       diff = 1.1005f;
       Yaxis = 0.0f;
        if (tag == "Centerblock")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Rotcheckpoint = transform.rotation;
            tx = transform.rotation.x;
            ty = transform.rotation.y;
            tz = transform.rotation.z;
            tw = transform.rotation.w;
        }
        /*if (tag == "RightBlock")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Rotcheckpoint = transform.rotation;
        }*/
        if (tag == "Tracker")
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pathg = GameObject.Find("Pathguider");
        b3 = true;
        caughtup = true;
        pos = false;
        neg = false;
    }
        void Update()
        {
            if(tag == "Centerblock")
                transform.position = new Vector3(pathg.transform.position.x, transform.position.y, pathg.transform.position.z - 3);
            if (tag == "LeftBlock")
            {
                transform.position = Crash.transform.position;    
                transform.rotation = new Quaternion(transform.rotation.x, Crash.transform.rotation.y, transform.rotation.z, transform.rotation.w);
            //transform.Rotate(0, -90, 0);
            }
        caughtup2 = caughtup;    
        }
    // Update is called once per frame
    void FixedUpdate () {
        
        if (tag == "Centerblock")
        {

            /*if (crash2.sp.z > 0)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z * diff);
                
            }
            if (crash2.sp.z < 0)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * crash2.sp.z * diff);
                
            }*/
            //transform.position = transform.TransformDirection(new Vector3(Crash.transform.position.z + 10.0f, 0, 22.0f));
            //transform.rotation = Crash.transform.rotation;
            transform.rotation = Rotcheckpoint;
            transform.Rotate(new Vector3(0, Yaxis, 0));
        }
        if(tag == "Tracker")
        {
            transform.rotation = Rotcheckpoint;
            transform.Rotate(new Vector3(0, Yaxis, 0));
        }
        //if(tag == "Tracker")
        //transform.position = new Vector3(tx, ty, tz + 0.5f);
        //if (tag == "Tracker2")
        //transform.position = new Vector3(tx, ty, tz + 2.0f);
        /*if (tag == "LeftBlock")
            transform.position = new Vector3(-19.78f, ty, transform.position.z);
        if (tag == "RightBlock")
            transform.position = new Vector3(30.54f, ty, transform.position.z);*/

        b = IsRight(transform.position);
        b2 = IsRight2(transform.position);
    }
    public bool IsRight(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 100.55f, layer);
    }
    public bool IsRight2(Vector3 ground)
    {
        return Physics.Raycast(ground, Vector3.forward, 100.0f, layer2);
    }
    
}
