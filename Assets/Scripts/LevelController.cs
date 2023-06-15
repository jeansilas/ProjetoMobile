using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float price_study_MH;
    public float price_study_time;
    public float price_project_MH;
    public float price_project_time;
    public float price_test_MH;
    public float price_test_time;

    public int study;
    public int project;
    public int test;

    public float time;
    public float MH;
    public int coin;

    public int max_study;
    public int max_project;
    public int max_test;

    public float max_time;
    public float inc_time;
    public float inc_MH;

    public bool unlock_project;
    public bool unlock_test;

    public int price_MH_upgrade_temporary_type1;
    public int price_MH_upgrade_temporary_type2;
    public int price_MH_upgrade_temporary_type3;

    public int price_coin_upgrade_permanent_type1;
    public int price_coin_upgrade_permanent_type2;
    public int price_coin_upgrade_permanent_type3;

    public float price_time_upgrade_temporary_type1;
    public float price_time_upgrade_temporary_type2;
    public float price_time_upgrade_temporary_type3;

    // Start is called before the first frame update
    void Start()
    {
        price_study_MH = 10;
        price_study_time = 0.5f;
        price_project_MH = 20;
        price_project_time = 1;
        price_test_MH = 30;
        price_test_time = 1.5f;

        study = 0;
        project = 0;
        test = 0;
        time = 0;
        MH = 0;
        coin = 5;

        max_study = 150;
        max_project = 60;
        max_test = 5;

        max_time = 6;
        inc_time = 1/60f;
        inc_MH = 0.5f;

        unlock_project = false;
        unlock_test = false;

        price_MH_upgrade_temporary_type1 = 10;
        price_MH_upgrade_temporary_type2 = 20;
        price_MH_upgrade_temporary_type3 = 30;

        price_coin_upgrade_permanent_type1 = 50;
        price_coin_upgrade_permanent_type2 = 70;
        price_coin_upgrade_permanent_type3 = 60;

        price_time_upgrade_temporary_type1 = 3.0f;
        price_time_upgrade_temporary_type2 = 2.5f;
        price_time_upgrade_temporary_type3 = 3.5f;

        // type1: Aumenta a quantidade de tempo exemplo:  vai de 3h para 6h (Absoluto)
        // type2: Aumenta a taxa de crescimento do "time do jogo". por exemplo ele vai ganhar 1 time a cada 10 min ao invés de 20 min (Taxa/percentual)
        // type3: Aumenta a taxa de ganho de Mental Health por tempo. Ao invés de ganhar 0.5 MH por minuto, ganha 1 MH por minuto   (Taxa/percentual)

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time: " + inc_time + " MH: " + inc_MH);
    }


}
