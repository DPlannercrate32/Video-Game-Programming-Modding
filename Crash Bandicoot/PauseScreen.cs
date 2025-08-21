using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject PauseUI, Finished;
    public static bool isPaused, isRestart;
    public Vector3[] PosNormcrates, PosArrowcrate, PosCagedcrate, PosQuestionmarkcrate, PosTNTs, PosNitros, PosEnemy, Enemyscale, PosWumpa, PosCP;
    public Vector3 PosCrash, EnemySc;
    public GameObject Obj;
    public int CCindex, Normindex, QMindex, Arrindex, TNdex, Ndex, Enedex, Windex, CPindex;
    public int normcount, arrowcount, cagedcount, questioncount, tntcount, nitrocount, enemycount, wumpacount, crystal, gem;
    public GameObject[] fruity, CPs;
    public CPMemory Cpm;
    public float timelimit;
    public bool indexcheck, destroyedcp, flag;
    public Crash_CPHY crash2;
    public MeshRenderer msh;
    public Vector3 Poscrystal, Posgem, gemscale;
    // Use this for initialization
    void Start()
    {
        PauseUI = GameObject.FindGameObjectWithTag("Pause");
        PauseUI.SetActive(false);
        Finished = GameObject.FindGameObjectWithTag("Finish");
        Finished.SetActive(false);
        isPaused = false;
        isRestart = false;
        crash2 = GameObject.Find("Crash").GetComponent<Crash_CPHY>();
        Cpm = GameObject.Find("ObjectMemory").GetComponent<CPMemory>();
        fruity = GameObject.FindGameObjectsWithTag("WFruit");
        CPs = GameObject.FindGameObjectsWithTag("CP");
        CCindex = 0; Normindex = 0; QMindex = 0; Arrindex = 0; TNdex = 0; Ndex = 0; Enedex = 0; Windex = 0; CPindex = 0;
        PosCP = new Vector3[CPs.Length];
        PosCagedcrate = new Vector3[Cpm.Cagedcrate.Length];
        PosNormcrates = new Vector3[Cpm.Normcrates.Length];
        PosQuestionmarkcrate = new Vector3[Cpm.Questionmarkcrate.Length];
        PosArrowcrate = new Vector3[Cpm.Arrowcrate.Length];
        PosTNTs = new Vector3[Cpm.TNTs.Length];
        PosNitros = new Vector3[Cpm.Nitros.Length];
        PosEnemy = new Vector3[Cpm.Enemy.Length];
        Enemyscale = new Vector3[Cpm.Enemy.Length];
        PosWumpa = new Vector3[fruity.Length];
        normcount = Cpm.Normcrates.Length;
        arrowcount = Cpm.Arrowcrate.Length;
        cagedcount = Cpm.Cagedcrate.Length;
        questioncount = Cpm.Questionmarkcrate.Length;
        tntcount = Cpm.TNTs.Length;
        nitrocount = Cpm.Nitros.Length;
        enemycount = Cpm.Enemy.Length;
        wumpacount = fruity.Length;
        timelimit = 3.0f;
        indexcheck = false;
        destroyedcp = false;
        flag = false;
        crystal = 0;
        gem = 0;
        Poscrystal = GameObject.Find("Crystal").transform.position;
        Posgem = GameObject.Find("Gem").transform.position;
        gemscale = GameObject.Find("Gem").transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(crash2.finished == true)
        {
            Finished.SetActive(true);
            GameObject.Find("Canvas").SetActive(false);
            Time.timeScale = 0;
            if (flag == false)
            {
                if (crystal == 1)
                {
                    Obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    Obj.transform.position = GameObject.Find("Your Finished").transform.position;
                    Obj.transform.position = new Vector3(Obj.transform.position.x - 2.0f, Obj.transform.position.y - 1.5f, Obj.transform.position.z);
                    Obj.transform.localScale *= 0.75f;
                    Obj.name = "Crystal";
                    Obj.tag = "Crystal";
                    msh = Obj.GetComponent<MeshRenderer>();
                    msh.material.color = new Color(1.0f, 0.0f, 0.8f);
                }
                if (gem == 1)
                {
                    Obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    Obj.transform.position = GameObject.Find("Your Finished").transform.position;
                    Obj.transform.position = new Vector3(Obj.transform.position.x + 0.5f, Obj.transform.position.y - 1.5f, Obj.transform.position.z);
                    Obj.transform.localScale = gemscale * 0.85f;
                    Obj.name = "Gem";
                    Obj.tag = "Gem";
                    msh = Obj.GetComponent<MeshRenderer>();
                    msh.material.color = Color.gray;
                }
                flag = true;
            }
        }

        if (CPindex < CPs.Length && indexcheck == false)
        {
            foreach (GameObject CP in CPs)
            {
                PosCP[CPindex] = CP.transform.position;
                CPindex++;
            }
        }
        else
            indexcheck = true;
        if (isRestart == true)
        {
            timelimit -= Time.deltaTime;
            if (destroyedcp == false)
                DestroyCP();
            crash2.rb.isKinematic = true;
        }
        if(timelimit <= 0.0f)
        {
            isRestart = false;
            resetindex();
            CrCrate();
            resetindex();
            timelimit = 3.0f;
            destroyedcp = false;
            crash2.rb.isKinematic = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Restart()
    {
        isRestart = true;
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void resetindex()
    {
        CCindex = 0;
        Normindex = 0;
        QMindex = 0;
        Arrindex = 0;
        TNdex = 0;
        Ndex = 0;
        Enedex = 0;
        CPindex = 0;
        Windex = 0;
    }
    public void CrCrate()
    {
        int i, j, k, l, m, n, o, p, w;
        

        for (i = 0; i < normcount; i++)
        {
            Obj= GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosNormcrates[Normindex];
            Obj.name = "Crate";
            Obj.tag = "crate";
            Obj.AddComponent<NormalCrate>();
            Normindex++;
        }

        for (l = 0; l < arrowcount; l++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosArrowcrate[Arrindex];
            Obj.name = "ArrowCrate";
            Obj.tag = "Arrow";
            Obj.AddComponent<ArrowCrate>();
            Arrindex++;
        }
        for (j = 0; j < cagedcount; j++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosCagedcrate[CCindex];
            Obj.name = "CagedCrate";
            Obj.tag = "CagedC";
            Obj.AddComponent<CagedCrate>();
            CCindex++;
        }
        for (k = 0; k < questioncount; k++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosQuestionmarkcrate[QMindex];
            Obj.name = "Question";
            Obj.tag = "question";
            Obj.AddComponent<QuestionCrate>();
            QMindex++;
        }
        for (m = 0; m < tntcount; m++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosTNTs[TNdex];
            Obj.name = "Tnt";
            Obj.tag = "Tnt";
            Obj.AddComponent<TNT>();
            TNdex++;
        }
        for (n = 0; n < nitrocount; n++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosNitros[Ndex];
            Obj.name = "Nitro";
            Obj.tag = "nitros";
            Obj.AddComponent<Nitros>();
            Ndex++;
        }
        for (o = 0; o < enemycount; o++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosEnemy[Enedex];
            Obj.transform.localScale = EnemySc;
            Obj.name = "Enemy";
            Obj.tag = "enemy";
            Obj.AddComponent<Enemy>();
            Enedex++;
        }
        for(p = 0; p < CPs.Length; p++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Obj.transform.position = PosCP[CPindex];
            Obj.tag = "CP";
            msh = Obj.GetComponent<MeshRenderer>();
            msh.material.color = new Color(1.0f, 0.5f, 0.0f);
            CPindex++;
        }
        for(w = 0; w < wumpacount; w++)
        {
            Obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Obj.transform.position = PosWumpa[Windex];
            Obj.name = "Fruit";
            Obj.tag = "WFruit";
            Obj.transform.localScale *= 0.5f;
            Windex++;

        }
        Obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Obj.transform.position = Poscrystal;
        Obj.name = "Crystal";
        Obj.tag = "Crystal";
        msh = Obj.GetComponent<MeshRenderer>();
        msh.material.color = new Color(1.0f, 0.0f, 0.8f);

        Obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Obj.transform.position = Posgem;
        Obj.transform.localScale = gemscale;
        Obj.name = "Gem";
        Obj.tag = "Gem";
        msh = Obj.GetComponent<MeshRenderer>();
        msh.material.color = Color.gray;
    }
    void DestroyCP()
    {
        for(int i = 0; i < CPs.Length; i++)
        {
            Destroy(CPs[i]);
        }
        if(GameObject.Find("Crystal") != null)
            Destroy(GameObject.Find("Crystal"));
        if (GameObject.Find("Crystal") != null)
            Destroy(GameObject.Find("Gem"));
        crystal = 0;
        gem = 0;
        destroyedcp = true;
    }
}
