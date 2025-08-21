using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wumpa : MonoBehaviour { 
    public float timer, wumpafloating;
    public bool swtch, expofinished;
    public GameObject[] ex;
    public Crash_CPHY crash2;
    public PauseScreen Ps;
    // Use this for initialization
    void Start() {
        crash2 = GameObject.Find("Crash").GetComponent<Crash_CPHY>();
        Ps = GameObject.Find("CanvasP").GetComponent<PauseScreen>();
        wumpafloating = 1.0f;
        timer = 1.3f;
        swtch = false;
        expofinished = false;
        Ps.PosWumpa[Ps.Windex] = transform.position;
        Ps.Windex++;
      	}
	
	// Update is called once per frame
	void Update () {
        if (PauseScreen.isRestart == true)
            Destroy(gameObject);
        if (PauseScreen.isPaused == false)
        {
            if (crash2.deathtimer < 0.5f && gameObject.name == "fruity")
                Destroy(gameObject);
            explosion();
            if (timer > 0.0f && swtch == false)
                timer -= Time.deltaTime;
            if (timer < 1.3f && swtch == true)
                timer += Time.deltaTime;
            if (timer < 0.0f)
            {
                swtch = true;
                wumpafloating = -1.0f;
            }
            if (timer > 1.3f)
            {
                swtch = false;
                wumpafloating = 1.0f;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.0009f * wumpafloating), transform.position.z);
        }
    }
    void explosion()
    {
        ex = GameObject.FindGameObjectsWithTag("explosion");
        if (expofinished == false)
        {
            foreach (GameObject Explo in ex)
            {
                if (Explo != null && ((transform.localPosition.x >= Explo.transform.position.x - 2.0 && transform.localPosition.x <= Explo.transform.position.x + 2.0) && (transform.localPosition.z >= Explo.transform.position.z - 2.0 && transform.localPosition.z <= Explo.transform.position.z + 2.0) && (transform.localPosition.y >= Explo.transform.position.y - 2.0 && transform.localPosition.y <= Explo.transform.position.y + 2.0)))
                {
                    
                    expofinished = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
