using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggers : MonoBehaviour {
    public Rigidbody rb;
    public BoxCollider bx;
    public bool Rt, Lt;
    public static bool positive, negative;
    public GameObject crash, camera;
    public LinearCamera camera2;
    public Crash_CPHY crash2;
    public float id, Hid;
    

    private void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.name == "Crash" && tag == "rightT" && crash2.tracker == false)
        //crash2.Rtrigger = true;
        //if (col.gameObject.name == "Crash" && tag == "leftT" && crash2.tracker == false)
        //crash2.Ltrigger = true;
        if (col.gameObject.tag == "CameraDir" && tag == "rightT")
        {
            camera2.degree = id;
            camera2.scale = transform.localScale.z;
            crash2.Rtrigger = true;
            if (crash2.sp.z > 0)
            {
                positive = true;
                negative = false;
            }
            else
            {
                negative = true;
                positive = false;
            }
            if (negative == true)
                camera2.axishold = camera2.axishold - camera2.degree;
        }
        if (col.gameObject.tag == "CameraDir" && tag == "leftT")
        {
            camera2.degree = id;
            camera2.scale = transform.localScale.z;
            crash2.Ltrigger = true;
            if (crash2.sp.z > 0) {
                positive = true;
                negative = false;
            }
            else
            {
                negative = true;
                positive = false;
            }
            
            if(negative == true)
                camera2.axishold = camera2.axishold + camera2.degree;
        }
        if(col.gameObject.tag == "CameraDir" && tag == "heightT")
        {
            crash2.Htrigger = true;
            camera2.scaleH = transform.localScale.z;
            camera2.hdegree = Hid;
            camera2.scale = transform.localScale.z;
            if (crash2.sp.z > 0)
            {
                positive = true;
                negative = false;
            }
            else
            {
                negative = true;
                positive = false;
            }
            if (crash2.sp.z < 0)
                camera2.oldy = camera2.oldDandi - camera2.hdegree;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "CameraDir" && tag == "rightT")
        {
            crash2.Rtrigger = false;
            camera2.timeout = false;
            if (positive == true && crash2.sp.z < 0)
            {
                camera2.left = true;
                camera2.right = false;
                camera2.timer2 -= 0.1f;
            }
            if (positive == true && crash2.sp.z > 0)
                camera2.leftpts++;
            if (negative == true && crash2.sp.z < 0)
                camera2.leftpts--;
            if (negative == true && crash2.sp.z > 0)
            {
                camera2.left = false;
                camera2.right = true;
                camera2.timer -= 0.1f;
            }
            camera2.nochangeold = false;

            //camera.transform.Rotate(0, Time.deltaTime * 100.0f, 0);
            if (negative == false || crash2.sp.z > 0)
                camera2.axishold = camera2.Yaxis;
            if (negative == true || crash2.sp.z < 0)
                camera2.axishold = camera2.Yaxis;
        }

        if (col.gameObject.tag == "CameraDir" && tag == "leftT")
        {
            crash2.Ltrigger = false;
            camera2.timeout = false;
            if (positive == true && crash2.sp.z < 0)
            {
                camera2.left = true;
                camera2.right = false;
                camera2.timer2 -= 0.1f;
            }
            if (positive == true && crash2.sp.z > 0)
                camera2.leftpts++;
            if (negative == true && crash2.sp.z < 0)
                camera2.leftpts--;
            if(negative == true && crash2.sp.z > 0)
            {
                camera2.left = false;
                camera2.right = true;
                camera2.timer -= 0.1f;
            }

            camera2.nochangeold = false;
            //camera.transform.Rotate(0, Time.deltaTime * 100.0f, 0);
            if (negative == false || crash2.sp.z > 0)
                camera2.axishold = camera2.Yaxis;
            if (negative == true || crash2.sp.z < 0)
                camera2.axishold = camera2.Yaxis;


        }
        if (col.gameObject.tag == "CameraDir" && tag == "heightT")
        {
            crash2.Htrigger = false;
            camera2.nochangeoldH = false;
            if (crash2.sp.z > 0)
                camera2.oldy = camera2.oldDandi;
            
        }
    }
        //Use this for initialization
    void Start () {
        
        //rb = gameObject.AddComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
        crash = GameObject.Find("Crash");
        crash2 = crash.GetComponent<Crash_CPHY>();
        camera = GameObject.Find("Main Camera");
        camera2 = camera.GetComponent<LinearCamera>();
        bx.isTrigger = true;
        
        Lt = false;
        Rt = false;
        
        
	}
	
	// Update is called once per frame
	void Update () {
        

	}
}
