using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathguide : MonoBehaviour {

    public GameObject crash;
    public LinearCamera camera;
    public float maxspeed, acceleration, accelLR, accelUD, yaxis, ogyaxis;
    public LayerMask layer;
    public Vector3 sp, max, crashground, checkpoint, fwdtracking;
    public bool crouch, Ltrigger, Rtrigger;
    Renderer rend;

    private float posi, maxjptimer, higher, crouchspeed, momentumcancel, xspeed, zspeed, negxspeed, negzspeed, leftspeed, rightspeed, negLspeed, negRspeed, ax, az, aw;
    public Rigidbody rb, rb2;
    public SphereCollider spcol;
    private MeshRenderer meshrend;
    public Vector3 relvel;
    public Quaternion Rotcheckpoint, aaa;
    public Vector3 ford;
    public float oldx;
    Transform ft;


   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        spcol = GetComponent<SphereCollider>();
        meshrend = GetComponent<MeshRenderer>();
        camera = GameObject.Find("Main Camera").GetComponent<LinearCamera>();
        maxspeed = 10.0f;
        acceleration = 10.0f;
        accelLR = acceleration * 2;
        accelUD = acceleration * 2;
        momentumcancel = 10.0f;
        crouchspeed = 1.0f;
        spcol.isTrigger = true;

        Ltrigger = false;
        Rtrigger = false;

        checkpoint = transform.position;

        aaa = transform.rotation;
        ax = transform.rotation.x;
        ogyaxis = transform.rotation.y;
        yaxis = 0.0f;
        az = transform.rotation.z;
        aw = transform.rotation.w;
        Rotcheckpoint = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.rotation = Rotcheckpoint;
        transform.Rotate(new Vector3(0, yaxis, 0));
        ford = rb.velocity;
        //sp = transform.InverseTransformDirection(rb.velocity);
        Physics.IgnoreCollision(GameObject.Find("PlayerTracker").GetComponent<Collider>(), GetComponent<Collider>());


    }

    // Update is called once per frame
    void Update()
    {
     


    }
    public bool Istarget(Vector3 ground, Vector3 dir)
    {
        return Physics.Raycast(ground, dir, 2000.55f, layer);
    }
}