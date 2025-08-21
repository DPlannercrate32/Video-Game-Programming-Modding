using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinboxing : MonoBehaviour {

    public GameObject spinbox, spinbox2, spinbox3, spinbox4;
    public GameObject crash, Crfruit;
    public GameObject[] ghost;
    public Crash_CPHY crash2;
    public CPMemory Cpm;
    private Rigidbody rb, rb2;
    public MeshRenderer meshrend2;
    public BoxCollider crashcol2;
    public float wumpatimer;
    public int a;
    public LinearCamera camera;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "CP")
        {
            crash2.checkpoint = col.transform.position;
            crash2.checkpoint2 = col.transform.position;
            Cpm.zeroed();
            crash2.cratecounter++;
            crash2.cpyaxis = crash2.yaxis;
            camera.cpyaxis = camera.Yaxis;
            Destroy(col.gameObject);
        }

    }
    void Start () {
        rb = GetComponent<Rigidbody>();
        meshrend2 = GetComponent<MeshRenderer>();
        crashcol2 = GetComponent<BoxCollider>();
        crash2 = crash.gameObject.GetComponent<Crash_CPHY>();
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        camera = GameObject.Find("Main Camera").GetComponent<LinearCamera>();
        a = 5;
        crashcol2.isTrigger = true;
        wumpatimer = 1.0f;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(0.3450f, 0.5450f, 0.3450f);
        if (crash2.slide == false)
            transform.localScale = spinbox.transform.localScale;
        else
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Physics.IgnoreCollision(crash.GetComponent<Collider>(), GetComponent<Collider>());
        ghost = GameObject.FindGameObjectsWithTag("ghost");
        foreach (GameObject gh in ghost)
        {
            Physics.IgnoreCollision(gh.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

   
}
