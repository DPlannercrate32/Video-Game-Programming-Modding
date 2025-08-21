using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
    public class RotatingBalls : MonoBehaviour
    {

        public GameObject sph, sph2, sph3, sph4, sph5, sph6;
        public GameObject[] list2;
        public Material puregauge;
        public float seconds, seconds2, seconds3, tlimit, circulartimer, x, z, degreeskip, multix;
        float a, b, c, maxseconds, maxseconds2, maxseconds3, maxlimit;
        private bool updown, clockwise, multiflag, therest;//, /*backwards,*/randomround;
        public bool gameover, timeout, doesitwork, c1, d1, backwards, randomround;
        public int rotateD;
        public Click1 ck, ck2, ck3, ck4, ck5, ck6;
        public Click1[] list;
        public int roundend, round, minusround, randmrd, evilball, gaugepts, num;
        public byte puritylevel, bite;
        public Vector3 localpos;
        
        int[] selectedlist, selectedlist2, selectLholder;
        string[] arc;
        public float[] maxsecs, vertsecs, d;
        public bool[] updownlist, vts;
        int index, holder, index2, randindex, listholder;
        public Text countertext, countertext2, countertext3;
        void Start()
        {
            Renderer rend = GetComponent<Renderer>();
            //countertext = GetComponent<Text>() as Text;
            print(rend.material.GetColor("_Color"));
            puregauge.color = new  Color32(255, 255, 255, 1);
            arc = new string[6] { "red", "blue", "yellow", "orange", "green", "black" };
            list = new Click1[6] {ck, ck2, ck3, ck4, ck5, ck6};
            list2 = new GameObject[6] { sph, sph2, sph3, sph4, sph5, sph6 };
            maxsecs = new float[3] {2.0f, 3.0f, 4.0f};
            selectedlist = new int[5] { 3, 1, 4, 0, 2 };
            selectedlist2 = new int[5] { 2, 4, 3, 0, 1 };
            updownlist = new bool[6] { true, true, true, true, true, true };
            vts = new bool[6] { false, false, false, false, false, false };
            puritylevel = 255;
            bite = 0;
            a = 0.0f;
            b = 0.0f;
            c = 0.0f;
            d = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
            c1 = false;
            d1 = false;
            rotateD = 0;
            maxseconds = 2.0f;
            seconds = maxseconds;
            vertsecs = new float[6] {maxseconds, maxseconds, maxseconds, maxseconds, maxseconds, maxseconds};
            maxseconds2 = 1.0f;
            seconds2 = maxseconds2;
            maxseconds3 = 3.0f;
            seconds3 = maxseconds3;
            maxlimit = 10.0f;
            tlimit = maxlimit;
            roundend = 0;
            updown = true;
            holder = 0;
            gameover = false;
            clockwise = true;
            timeout = false;
            multiflag = false;
            therest = false;
            doesitwork = false;
            backwards = false;
            randomround = false;
            minusround = 1;
            multix = 1.0f;
            evilball = 0;
            randindex = 0;
            list[0].setcolor(arc[5]);
            list[1].setcolor(arc[2]);
            list[2].setcolor(arc[1]);
            list[3].setcolor(arc[0]);
            list[4].setcolor(arc[4]);
            list[5].setcolor(arc[3]);
            
        }
        

        // Update is called once per frame
        void FixedUpdate()
        {
        if (gameover != true)
        {
            //a = a + 0.005f;
            degreeskip = 0.0f;
            if (round > 4)
                backwards = true;
            if (minusround > 4)
                backwards = false;
            if (round > 9)
                randomround = true;

            if (round % 5 == 2 && backwards == false && randomround == false || minusround == 2 && backwards == true ||  randomround == true && selectLholder[randindex] == 2)
            {
                if (multix >= 2.0)
                    multiflag = true;
                if (multiflag == true && multix > 1.0f)
                    multix -= Time.deltaTime;
                if (multix <= 1.0f && multiflag == true)
                    multiflag = false;

                if (multix < 2.0 && multiflag == false)
                    multix += Time.deltaTime;
            }

            if (round % 5 == 0 && backwards == false && randomround == false || minusround == 4 && backwards == true || randomround == true && selectLholder[randindex] == 0)
                multix = 1.0f;
            if (round % 5 == 1 && backwards == false && randomround == false || minusround == 3 && backwards == true || randomround == true && selectLholder[randindex] == 1)
                multix = 2.0f;
            //-----------------------------------------------------------------------------------------------------
            if (clockwise == true)
                circulartimer += Time.deltaTime * 2;
            else
                circulartimer -= Time.deltaTime * 2;
            for (int i = 0; i <= 5; i++)
            {
                if (i > 0)
                    degreeskip += 1.0f;
                //circulartimer += Time.deltaTime * 0.5f;
                if (timeout == false)
                {

                    x = Mathf.Cos(-(circulartimer + degreeskip)) * multix;
                    z = Mathf.Sin(-(circulartimer + degreeskip))/**0.8f*/;

                    list2[i].transform.position = new Vector3(x, 0.0f, z);
                }
                //list2[i].transform.position = new Vector3(x, 0.0f, z);
            }
            //sph6.transform.position = new Vector3(x/*+5*/, -0.0647f, z);
            localpos = sph.transform.position;
            //-----------------------------------------------------------------------------------
            if (timeout == false)
                tlimit -= Time.deltaTime;
            if (tlimit <= 0.0f)
            {
                timeout = true;
                round++;
                if (randindex < 5 && randomround == true)
                    randindex++;
                if (randmrd == 0)
                {
                    listholder = Random.Range(0, 1);
                    if (listholder == 0)
                        selectLholder = selectedlist;
                    if (listholder == 1)
                        selectLholder = selectedlist2;

                }
                if (backwards == true)
                    minusround++;
                if (randomround == true)
                    randmrd++; //= Random.Range(0, 4);
                if (randmrd > 4)
                {
                    randmrd = 0;
                    randindex = 0;
                }
                if (puritylevel <= 0)
                    puritylevel = 255;
                if (gaugepts == 0)
                {
                    num = puritylevel - 0;
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
                if (gaugepts == 1)
                {
                    num = puritylevel - 25;
                    if (num < 0)
                    {
                        num = 255;//num = num + (num * -1);
                        evilball += 1;
                        ck.textup2 = true;
                        if (evilball < 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 00");
                        if (evilball >= 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                    }
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
                if (gaugepts == 2)
                {
                    num = puritylevel - 50;
                    if (num < 0)
                    {
                        num = 255; 
                        evilball += 1;
                        ck.textup2 = true;
                        if (evilball < 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 00");
                        if (evilball >= 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                    }
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
                if (gaugepts == 3)
                {
                    num = puritylevel - 75;
                    if (num < 0)
                    {
                        num = 255;
                        evilball += 1;
                        ck.textup2 = true;
                        if (evilball < 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 00");
                        if (evilball >= 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                    }
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
                if (gaugepts == 4)
                {
                    num = puritylevel - 100;
                    if (num < 0)
                    {
                        num = 255;
                        evilball += 1;
                        ck.textup2 = true;
                        if (evilball < 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 00");
                        if (evilball >= 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                    }
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
                if (gaugepts == 5)
                {
                    num = puritylevel - 127;
                    if (num < 0)
                    {
                        num = 255;
                        evilball += 1;
                        ck.textup2 = true;
                        if (evilball < 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 00");
                        if (evilball >= 3)
                            ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                    }
                    puritylevel = System.Convert.ToByte(num);
                    puregauge.color = new Color32(puritylevel, puritylevel, puritylevel, 1);
                }
               

                list[0].setcolor(arc[5]);
                list[1].setcolor(arc[2]);
                list[2].setcolor(arc[1]);
                list[3].setcolor(arc[0]);
                list[4].setcolor(arc[4]);
                list[5].setcolor(arc[3]);
                roundend = 0;
                tlimit += 7.0f;
            }
            if (tlimit < maxlimit && timeout == true)
            {
                tlimit += Time.deltaTime;
            }
            if (tlimit >= maxlimit)
            {
                timeout = false;
            }
            //---------------------------------------------------------------------------------------------------------------------
            index2 = Random.Range(0, 2);
            if (clockwise == true && seconds3 > 0.0f && timeout == false)
            {
                transform.Rotate(new Vector3(0.0f, 3.0f, 0.0f));
                seconds3 -= Time.deltaTime;
            }
            if (seconds3 >= maxseconds3)
                clockwise = true;
            if (seconds3 <= 0.0f)
            {
                clockwise = false;
                maxseconds3 = maxsecs[index2];
            }
            if (clockwise == false && seconds3 < maxseconds3 && timeout == false)
            {
                transform.Rotate(new Vector3(0.0f, -3.0f, 0.0f));
                seconds3 += Time.deltaTime;
            }
            //-----------------------------------------------------------------------------------------------------------------------
            index = Random.Range(0, 5);
            seconds2 -= Time.deltaTime;
            if (seconds2 <= 0.0f)
            {
                list[index].setrandomcolor(arc[index]);
                seconds2 = maxseconds2;
            }
            //-----------------------------------------------------------------------------------------------------------------------
            countertext.text = tlimit.ToString("Time: 00");
            countertext2.text = rotateD.ToString("Score: 000");
            countertext3.text = round.ToString("Round 00");
            //------------------------------------------------------------------------------------
            blackandrounds();
            if (evilball == 5)
                gameover = true;
            //-----------------------------------------------------------------------------------------------------------------------
            if (round % 5 == 3 && backwards == false && randomround == false || minusround == 1 && backwards == true || randomround == true && selectLholder[randindex] == 3)
            {
                if (updown == true && seconds > 0.0f)
                {
                    a = a + 0.01f;
                    b = b - 0.01f;
                    c = 0;
                    for (int i = 0; i <= 5; i++)
                    {
                        if(i % 2 == 0)
                            list2[i].transform.localPosition = new Vector3(list2[i].transform.localPosition.x, a, list2[i].transform.localPosition.z);
                        if(i % 2 == 1)
                            list2[i].transform.localPosition = new Vector3(list2[i].transform.localPosition.x, b, list2[i].transform.localPosition.z);
                           
                      
                    }
                    seconds -= Time.deltaTime;
                }
                if (seconds <= 0.0f)
                    updown = false;
                if (seconds >= maxseconds)
                {
                    updown = true;
                    //holder = index;
                }
                if (updown == false && seconds < maxseconds)
                {
                    a = a - 0.01f;
                    b = b + 0.01f;
                    c = 0.0f;
                    for (int i = 0; i <= 5; i++)
                    {
                        if (i % 2 == 0)
                            list2[i].transform.localPosition = new Vector3(list2[i].transform.localPosition.x, a, list2[i].transform.localPosition.z);
                        if (i % 2 == 1)
                            list2[i].transform.localPosition = new Vector3(list2[i].transform.localPosition.x, b, list2[i].transform.localPosition.z);

                  

                    }
                    seconds += Time.deltaTime;
                }
            }

            if(round % 5 == 4 && backwards == false && randomround == false || randomround == true && selectLholder[randindex] == 4)
            {
                float lapse = 0.3f;
                for (int i = 0; i <= 5; i++)
                {
                    if (updownlist[i] == true && vertsecs[i] > 0.0f)
                    {
                        if(i == 0 && updownlist[0] == true)
                            vts[0] = true;
                        if (i > 0 && vertsecs[i-1] < maxseconds - lapse && updownlist[i] == true)
                        {
                            vts[i] = true;
                            
                        }
                        lapse += 0.3f;
                    }
                    if (vertsecs[i] <= 0.0f)
                       updownlist[i] = false;
                    if (vertsecs[i] >= maxseconds)
                   {
                        updownlist[i] = true;
                        holder = index;
                   }
                    if (updownlist[i] == false && vertsecs[i] < maxseconds)
                    {
                            
                            
                            vts[i] = false;

                        
                        
                    }

                }


                if (vts[0] == true)
                {
                    d[0] = d[0] + 0.015f;
                    vertsecs[0] -= Time.deltaTime;
                }
                if (vts[1] == true)
                {
                    vertsecs[1] -= Time.deltaTime;
                    d[1] = d[1] + 0.015f;
                }
                if (vts[2] == true)
                {
                    vertsecs[2] -= Time.deltaTime;
                    d[2] = d[2] + 0.015f;
                }
                if (vts[3] == true)
                {
                    vertsecs[3] -= Time.deltaTime;
                    d[3] = d[3] + 0.015f;
                }
                if (vts[4] == true)
                {
                    vertsecs[4] -= Time.deltaTime;
                    d[4] = d[4] + 0.015f;
                }
                if (vts[5] == true)
                {
                    vertsecs[5] -= Time.deltaTime;
                    d[5] = d[5] + 0.015f;
                }
                
                if (vts[0] == false)
                {
                    doesitwork = true;
                    d[0] = d[0] - 0.015f;
                    vertsecs[0] += Time.deltaTime;
                }
                if (vts[1] == false && d[1] != 0.0f)
                {
                    vertsecs[1] += Time.deltaTime;
                    d[1] = d[1] - 0.015f;
                }
                if (vts[2] == false && d[2] != 0.0f)
                {
                    vertsecs[2] += Time.deltaTime;
                    d[2] = d[2] - 0.015f;
                }
                if (vts[3] == false && d[3] != 0.0f)
                {
                    vertsecs[3] += Time.deltaTime;
                    d[3] = d[3] - 0.015f;
                }
                if (vts[4] == false && d[4] != 0.0f)
                {
                    vertsecs[4] += Time.deltaTime;
                    d[4] = d[4] - 0.015f;
                }
                if (vts[5] == false && d[5] != 0.0f)
                {
                    vertsecs[5] += Time.deltaTime;
                    d[5] = d[5] - 0.015f;
                }


                list2[0].transform.localPosition = new Vector3(list2[0].transform.localPosition.x, d[0], list2[0].transform.localPosition.z);
                list2[1].transform.localPosition = new Vector3(list2[1].transform.localPosition.x, d[1], list2[1].transform.localPosition.z);
                list2[2].transform.localPosition = new Vector3(list2[2].transform.localPosition.x, d[2], list2[2].transform.localPosition.z);
                list2[3].transform.localPosition = new Vector3(list2[3].transform.localPosition.x, d[3], list2[3].transform.localPosition.z);
                list2[4].transform.localPosition = new Vector3(list2[4].transform.localPosition.x, d[4], list2[4].transform.localPosition.z);
                list2[5].transform.localPosition = new Vector3(list2[5].transform.localPosition.x, d[5], list2[5].transform.localPosition.z);

                 
            }

        }
       }

       void blackandrounds()
       {
        for (int i = 0; i <= 5; i++)
        {
            if (list[i].color == "black")
                roundend++;
            if (i == 5)
                gaugepts = 5 - (roundend - 1);
            if (i == 5 && roundend < 6)
                roundend = 0;
            if (i == 5 && roundend == 6)
            {
                //round++;
                tlimit = 0.0f;
                list[0].setcolor(arc[5]);
                list[1].setcolor(arc[2]);
                list[2].setcolor(arc[1]);
                list[3].setcolor(arc[0]);
                list[4].setcolor(arc[4]);
                list[5].setcolor(arc[3]);
                roundend = 0;
                if(evilball > 0)
                {
                    evilball -= 1;
                    if(evilball < 3)
                        ck.countertext3.text = evilball.ToString("EvilBall: 00");
                    if(evilball >= 3)
                        ck.countertext3.text = evilball.ToString("EvilBall: 0/5");
                }
            }
        }
    }
     
    }
