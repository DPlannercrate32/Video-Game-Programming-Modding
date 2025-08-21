using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Click1 : MonoBehaviour {
    public RotatingBalls rb;
    public int d;
    public string color;
    string[] arc;
    public Text countertext2, countertext3;
    public float timer, timer2, timer3, timer4;
    public bool textup, timer2out, textup2, timer4out, stayup;

    void Start()
    {
        arc = new string[6]{"red", "blue", "yellow", "orange", "green", "black"}; 
        d = 0;
        color = "";
        timer = 0.0f;
        timer2 = 0.0f;
        textup = false;
        timer2out = false;
        timer3 = 0.0f;
        timer4 = 0.0f;
        textup2 = false;
        timer4out = false;
        stayup = false;
    }
    void OnMouseDown()
    {
        //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //Debug.Log("Hey");
        if (rb.timeout == false)
        {
            if (color == "black")
            {
                rb.evilball += 1;
                if(rb.evilball < 3)
                    countertext3.text = rb.evilball.ToString("EvilBall: 00");//refresh it
                else
                    countertext3.text = rb.evilball.ToString("EvilBall: 0/5");
                textup2 = true;
            }
            rb.rotateD += d;
            color = "black";
            countertext2.text = d.ToString("00");
            textup = true;
            //countertext2.rectTransform.position = new Vector3(countertext2.rectTransform.position.x, countertext2.rectTransform.position.y, countertext2.rectTransform.position.z);
        }
    }
    public void setpoints(int a)
    {
        d = a;
    }
     
    public void setcolor(string b)
    {
        color = b; 
    }

    public void setrandomcolor(string r)
    {
        if (color != "black")
            color = r;
    }

    void colorender()
    {
        Renderer rend = GetComponent<Renderer>();
        if (color == arc[0])
            rend.material.SetColor("_Color", Color.red);
        if (color == arc[1])
            rend.material.SetColor("_Color", Color.blue);
        if (color == arc[2])
            rend.material.SetColor("_Color", Color.yellow);
        if (color == arc[3])
            rend.material.SetColor("_Color", new Color(1.0f, 0.54f, 0.0f));
        if (color == arc[4])
            rend.material.SetColor("_Color", Color.green);
        if (color == arc[5])
            rend.material.SetColor("_Color", Color.black);
        //rend.material.SetColor("_Color", Color.gray);
    }

    void colorpoints()
    {
        if (color == "black")
            d = -25;
        if (color == "yellow")
            d = 20;
        if (color == "blue")
            d = 30;
        if (color == "red")
            d = 40;
        if (color == "green")
            d = 50;
        if (color == "orange")
            d = 60;
    }

    void FixedUpdate()
    {
        colorender();
        colorpoints();
//------------------------------------------------------------------------------------------------------------------------
        if(textup == true)
        {
            countertext2.rectTransform.position = new Vector3(countertext2.rectTransform.position.x, countertext2.rectTransform.position.y + 5.0f, countertext2.rectTransform.position.z);
            timer += Time.deltaTime;
        }
        if (timer >= 0.1f)
            textup = false;
        if (textup == false && timer >= 0.1f && timer2out == false)
            timer2 += Time.deltaTime;
        if (timer2 >= 0.2f)
            timer2out = true;
                             
        if (textup == false && timer > 0.0f && timer2out == true)
        {
            countertext2.rectTransform.position = new Vector3(countertext2.rectTransform.position.x, countertext2.rectTransform.position.y - 5.0f, countertext2.rectTransform.position.z);
            timer -= Time.deltaTime;
            timer2 = 0.0f;
            
        }
        if (timer == 0.0)
            timer2out = false;

        //--------------------------------------------------------------------------------
        if (textup2 == true && rb.evilball < 4 && stayup == false)
        {
            if(timer4out == false)
                countertext3.rectTransform.position = new Vector3(countertext3.rectTransform.position.x, countertext3.rectTransform.position.y + 5.0f, countertext3.rectTransform.position.z);
            timer3 += Time.deltaTime;
        }
        if (rb.evilball >= 4)
            stayup = true;
        if (rb.evilball < 3)
            stayup = false;
        if (timer3 >= 0.1f)
            textup2 = false;
        if (textup2 == false && timer3 >= 0.1f && timer4out == false)
            timer4 += Time.deltaTime;
        if (timer4 >= 0.43f)
            timer4out = true;

        if (textup2 == false && timer3 > 0.0f && timer4out == true && rb.evilball < 3)
        {
            countertext3.rectTransform.position = new Vector3(countertext3.rectTransform.position.x, countertext3.rectTransform.position.y - 5.0f, countertext3.rectTransform.position.z);
            timer3 -= Time.deltaTime;
            timer4 = 0.0f; //basically resetting that variable.

        }
        if (timer3 == 0.0)
            timer4out = false;
    }
}
