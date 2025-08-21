using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour {
    public GameObject crash;
    public BoxCollider bcol;
    public Rigidbody rb;
    float tx, ty, tz;
    // Use this for initialization
    void Start () {
		crash = GameObject.Find("Crash");
        bcol = GetComponent<BoxCollider>();
        bcol.isTrigger = true;
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
        tx = crash.transform.position.x;
        ty = crash.transform.position.y;
        tz = crash.transform.position.z;

        transform.position = new Vector3(tx, ty, tz + 2.0f);
    }
}
