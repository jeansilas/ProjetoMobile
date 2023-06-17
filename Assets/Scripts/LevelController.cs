using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.youtube.com/watch?v=OTAYEZMUI-o&list=PLGSUBi8nI9v9I5uaSRe8ccSV02W2dyxGM&index=10

public class LevelController : MonoBehaviour
{
    public float price_study_MH;
    public float price_study_time;
    public float price_project_MH;
    public float price_project_time;
    public float price_test_MH;
    public float price_test_time;

    public float price_study_MH_og;
    public float price_study_time_og;
    public float price_project_MH_og;
    public float price_project_time_og;
    public float price_test_MH_og;
    public float price_test_time_og;

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

    public float inc_study;
    public float inc_project;
    public float inc_test;

    public bool unlock_project;
    public bool unlock_test;
    public bool offlineProgress = false;
    public bool offlineTime;

    public bool done_project = false;
    public bool done_test = false;

    public int level;
    private float timer;
    public OfflineController offlineController;

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
        
        /*
        level = 1;
        price_study_MH = 10;
        price_study_time = 0.5f;
        price_project_MH = 20;
        price_project_time = 1;
        price_test_MH = 30;
        price_test_time = 1.5f;

        price_study_MH_og = 10;
        price_study_time_og = 0.5f;
        price_project_MH_og = 20;
        price_project_time_og = 1;
        price_test_MH_og = 30;
        price_test_time_og = 1.5f;

        study = 0;
        project = 0;
        test = 0;
        MH = 10;
        time = 1;
        coin = 0;

        max_study = 150;
        max_project = 60;
        max_test = 30;

        max_time = 6;
        inc_time = 1/60f;
        inc_MH = 0.5f;

        inc_study = 1.07f;
        inc_project = 1.1f;
        inc_test = 1.15f;

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

        */
        
        
        Load();
        offlineController = GameObject.Find("Menu").GetComponent<OfflineController>();
        offlineController.LoadOfflineProduction();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            Save();
        }
    }

    public void Load()
    {
        level = PlayerPrefs.GetInt("level", 1);
        price_study_MH = PlayerPrefs.GetFloat("price_study_MH", 10);
        price_study_time = PlayerPrefs.GetFloat("price_study_time", 0.5f);
        price_project_MH = PlayerPrefs.GetFloat("price_project_MH", 20);
        price_project_time = PlayerPrefs.GetFloat("price_project_time", 1);
        price_test_MH = PlayerPrefs.GetFloat("price_test_MH", 30);
        price_test_time = PlayerPrefs.GetFloat("price_test_time", 1.5f);

        price_study_MH_og = PlayerPrefs.GetFloat("price_study_MH_og", 10);
        price_study_time_og = PlayerPrefs.GetFloat("price_study_time_og", 0.5f);
        price_project_MH_og = PlayerPrefs.GetFloat("price_project_MH_og", 20);
        price_project_time_og = PlayerPrefs.GetFloat("price_project_time_og", 1);
        price_test_MH_og = PlayerPrefs.GetFloat("price_test_MH_og", 30);
        price_test_time_og = PlayerPrefs.GetFloat("price_test_time_og", 1.5f);

        study = PlayerPrefs.GetInt("study", 0);
        project = PlayerPrefs.GetInt("project", 0);
        test = PlayerPrefs.GetInt("test", 0);
        time = PlayerPrefs.GetFloat("time", 1.0f);
        MH = PlayerPrefs.GetFloat("MH", 10.0f);
        coin = PlayerPrefs.GetInt("coin", 0);

        max_study = PlayerPrefs.GetInt("max_study", 150);
        max_project = PlayerPrefs.GetInt("max_project", 60);
        max_test = PlayerPrefs.GetInt("max_test", 5);

        max_time = PlayerPrefs.GetFloat("max_time", 6.0f);
        inc_time = PlayerPrefs.GetFloat("inc_time", 0.016666666667f);
        inc_MH = PlayerPrefs.GetFloat("inc_MH", 0.5f);

        inc_study = PlayerPrefs.GetFloat("inc_study", 1.07f);
        inc_project = PlayerPrefs.GetFloat("inc_project", 1.1f);
        inc_test = PlayerPrefs.GetFloat("inc_test", 1.15f);

        unlock_project = bool.Parse(PlayerPrefs.GetString("unlock_project", "False"));
        unlock_test = bool.Parse(PlayerPrefs.GetString("unlock_test", "False"));
        offlineProgress = bool.Parse(PlayerPrefs.GetString("offlineProgress", "False"));
        done_project = bool.Parse(PlayerPrefs.GetString("done_project", "False"));
        done_test = bool.Parse(PlayerPrefs.GetString("done_test", "False"));

        price_MH_upgrade_temporary_type1 = PlayerPrefs.GetInt("price_MH_upgrade_temporary_type1", 10);
        price_MH_upgrade_temporary_type2 = PlayerPrefs.GetInt("price_MH_upgrade_temporary_type2", 20);
        price_MH_upgrade_temporary_type3 = PlayerPrefs.GetInt("price_MH_upgrade_temporary_type3", 30);

        price_coin_upgrade_permanent_type1 = PlayerPrefs.GetInt("price_coin_upgrade_permanent_type1", 50);
        price_coin_upgrade_permanent_type2 = PlayerPrefs.GetInt("price_coin_upgrade_permanent_type2", 70);
        price_coin_upgrade_permanent_type3 = PlayerPrefs.GetInt("price_coin_upgrade_permanent_type3", 60);

        price_time_upgrade_temporary_type1 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type1", 3.0f);
        price_time_upgrade_temporary_type2 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type2", 2.5f);
        price_time_upgrade_temporary_type3 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type3", 3.5f);

    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetFloat("price_study_MH", price_study_MH);
        PlayerPrefs.SetFloat("price_study_time", price_study_time);
        PlayerPrefs.SetFloat("price_project_MH", price_project_MH);
        PlayerPrefs.SetFloat("price_project_time", price_project_time);
        PlayerPrefs.SetFloat("price_test_MH", price_test_MH);
        PlayerPrefs.SetFloat("price_test_time", price_test_time);

        PlayerPrefs.SetFloat("price_study_MH_og", price_study_MH_og);
        PlayerPrefs.SetFloat("price_study_time_og", price_study_time_og);
        PlayerPrefs.SetFloat("price_project_MH_og", price_project_MH_og);
        PlayerPrefs.SetFloat("price_project_time_og", price_project_time_og);
        PlayerPrefs.SetFloat("price_test_MH_og", price_test_MH_og);
        PlayerPrefs.SetFloat("price_test_time_og", price_test_time_og);

        PlayerPrefs.SetInt("study", study);
        PlayerPrefs.SetInt("project", project);
        PlayerPrefs.SetInt("test", test);
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.SetFloat("MH", MH);
        PlayerPrefs.SetInt("coin", (int)coin);

        PlayerPrefs.SetInt("max_study", max_study);
        PlayerPrefs.SetInt("max_project", max_project);
        PlayerPrefs.SetInt("max_test", max_test);

        PlayerPrefs.SetFloat("max_time", max_time);
        PlayerPrefs.SetFloat("inc_time", inc_time);
        PlayerPrefs.SetFloat("inc_MH", inc_MH);

        PlayerPrefs.SetFloat("inc_study", inc_study);
        PlayerPrefs.SetFloat("inc_project", inc_project);
        PlayerPrefs.SetFloat("inc_test", inc_test);

        PlayerPrefs.SetString("unlock_project", unlock_project.ToString());
        PlayerPrefs.SetString("unlock_test", unlock_test.ToString());
        PlayerPrefs.SetString("offlineProgress", "True");
        PlayerPrefs.SetString("offlineTime", DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetString("done_project", done_project.ToString());
        PlayerPrefs.SetString("done_test", done_test.ToString());

        PlayerPrefs.SetInt("price_MH_upgrade_temporary_type1", price_MH_upgrade_temporary_type1);
        PlayerPrefs.SetInt("price_MH_upgrade_temporary_type2", price_MH_upgrade_temporary_type2);
        PlayerPrefs.SetInt("price_MH_upgrade_temporary_type3", price_MH_upgrade_temporary_type3);

        PlayerPrefs.SetInt("price_coin_upgrade_permanent_type1", price_coin_upgrade_permanent_type1);
        PlayerPrefs.SetInt("price_coin_upgrade_permanent_type2", price_coin_upgrade_permanent_type2);
        PlayerPrefs.SetInt("price_coin_upgrade_permanent_type3", price_coin_upgrade_permanent_type3);

        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type1", price_time_upgrade_temporary_type1);
        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type2", price_time_upgrade_temporary_type2);
        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type3", price_time_upgrade_temporary_type3);

        PlayerPrefs.Save();

    }
}
