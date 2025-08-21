using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathplane : MonoBehaviour {

    public GameObject crash;
    public Crash_CPHY crash2;
    public BoxCollider boxcol;
    private MeshRenderer meshrend;
    public CPMemory Cpm;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Crash")
        {
            crash2.deathtimer = 2.0f;
            crash2.dtimer = true;
            Cpm.dtimer = true;

        }
    }
        // Use this for initialization
    void Start () {
        boxcol = GetComponent<BoxCollider>();
        boxcol.isTrigger = true;
        meshrend = GetComponent<MeshRenderer>();
        meshrend.enabled = false;
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
