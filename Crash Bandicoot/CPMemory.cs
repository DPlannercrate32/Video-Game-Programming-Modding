using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPMemory : MonoBehaviour {
    public Crash_CPHY crash2;
    public GameObject CC;
    public bool dtimer, dtimer2;
    public GameObject[] crategr, questiongr, cagedgr, arrowgr, tntgr, nitrogr, cpgr, fruity;
    public Vector3[] PosNormcrates, PosArrowcrate, PosCagedcrate, PosQuestionmarkcrate, PosTNTs, PosNitros, PosEnemy, Enemyscale;
    public GameObject[] Normcrates, Arrowcrate, Cagedcrate, Questionmarkcrate, TNTs, Nitros, Enemy;
    public int normcount, arrowcount, cagedcount, questioncount, tntcount, nitrocount, CCindex, Normindex, QMindex, Arrindex, TNdex, Ndex, Enedex;
    public int normdes, arrowdes, cageddes, questiondes, tntdes, nitrodes, enedes, i, j, k, l, m, n, o, destroyedcrates;
    // Use this for initialization
    void Start () {
        crash2 = GameObject.Find("Crash").GetComponent<Crash_CPHY>();
        normdes = 0; arrowdes = 0; cageddes = 0; questiondes = 0; tntdes = 0; nitrodes = 0; destroyedcrates = 0;
        Cagedcrate = GameObject.FindGameObjectsWithTag("CagedC");
        PosCagedcrate = new Vector3[Cagedcrate.Length];
        Normcrates = GameObject.FindGameObjectsWithTag("crate");
        PosNormcrates = new Vector3[Normcrates.Length];
        Questionmarkcrate = GameObject.FindGameObjectsWithTag("question");
        PosQuestionmarkcrate = new Vector3[Questionmarkcrate.Length];
        Arrowcrate = GameObject.FindGameObjectsWithTag("Arrow");
        PosArrowcrate = new Vector3[Arrowcrate.Length];
        TNTs = GameObject.FindGameObjectsWithTag("Tnt");
        PosTNTs = new Vector3[TNTs.Length];
        Nitros = GameObject.FindGameObjectsWithTag("nitros");
        PosNitros = new Vector3[Nitros.Length];
        Enemy = GameObject.FindGameObjectsWithTag("enemy");
        PosEnemy = new Vector3[Enemy.Length];
        Enemyscale = new Vector3[Enemy.Length];
        zeroed();
        dtimer = false;
        crategr = GameObject.FindGameObjectsWithTag("crate");
        questiongr = GameObject.FindGameObjectsWithTag("question");
        cagedgr = GameObject.FindGameObjectsWithTag("CagedC");
        arrowgr = GameObject.FindGameObjectsWithTag("Arrow");
        tntgr = GameObject.FindGameObjectsWithTag("Tnt");
        nitrogr = GameObject.FindGameObjectsWithTag("nitros");
        cpgr = GameObject.FindGameObjectsWithTag("CP");
        
    }
	
	// Update is called once per frame
	void Update () {
		if (dtimer == true && crash2.deathtimer < 2.0f)
        {
            dtimer = false;
            resetindex();
            CrCrate();
          
        }

        if(crash2.deathtimer < 0.1f || crash2.CP == true)
        {
            crash2.cratecounter -= destroyedcrates;
            zeroed();
        }
	}

    void CrCrate()
    {
        if (cageddes > 0) {
            for (; i < cageddes; i++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosCagedcrate[CCindex];
                CC.name = "CagedCrate";
                CC.tag = "CagedC";
                CC.AddComponent<CagedCrate>();
                CCindex++;
            }
            
        }
        if (normdes > 0)
        {
            for (; j < normdes; j++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosNormcrates[Normindex];
                CC.name = "Crate";
                CC.tag = "crate";
                CC.AddComponent<NormalCrate>();
                Normindex++;
            }
            
        }
        if (questiondes > 0)
        {
            for (; k < questiondes; k++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosQuestionmarkcrate[QMindex];
                CC.name = "Question";
                CC.tag = "question";
                CC.AddComponent<QuestionCrate>();
                QMindex++;
            }
            
        }
        if (arrowdes > 0)
        {
            for (; l < arrowdes; l++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosArrowcrate[Arrindex];
                CC.name = "ArrowCrate";
                CC.tag = "Arrow";
                CC.AddComponent<ArrowCrate>();
                Arrindex++;
            }
            
        }
        if (tntdes > 0)
        {
            for (; m < tntdes; m++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosTNTs[TNdex];
                CC.name = "Tnt";
                CC.tag = "Tnt";
                CC.AddComponent<TNT>();
                TNdex++;
            }
            
        }
        if (nitrodes > 0)
        {
            for (; n < nitrodes; n++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosNitros[Ndex];
                CC.name = "Nitro";
                CC.tag = "nitros";
                CC.AddComponent<Nitros>();
                Ndex++;
            }
            
        }
        if (enedes > 0)
        {
            for (; o < enedes; o++)
            {
                CC = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CC.transform.position = PosEnemy[Enedex];
                CC.transform.localScale = Enemyscale[Enedex];
                CC.name = "Enemy";
                CC.tag = "enemy";
                CC.AddComponent<Enemy>();
                Enedex++;
            }

        }

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
    }
    public void zeroed()
    {
        CCindex = 0; Normindex = 0; QMindex = 0; Arrindex = 0; TNdex = 0; Ndex = 0; Enedex = 0;  i = 0; j = 0; k = 0; l = 0; m = 0; n = 0; o = 0 ;
        cageddes = 0; normdes = 0; questiondes = 0; arrowdes = 0; tntdes = 0; nitrodes = 0; enedes = 0; destroyedcrates = 0;
        PosCagedcrate = new Vector3[Cagedcrate.Length];
        PosNormcrates = new Vector3[Normcrates.Length];
        PosQuestionmarkcrate = new Vector3[Questionmarkcrate.Length];
        PosArrowcrate = new Vector3[Arrowcrate.Length];
        PosTNTs = new Vector3[TNTs.Length];
        PosNitros = new Vector3[Nitros.Length];
        PosEnemy = new Vector3[Enemy.Length];
        Enemyscale = new Vector3[Enemy.Length];
    }
}
