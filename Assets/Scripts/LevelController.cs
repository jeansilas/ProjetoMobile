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
    public float coin;

    public int max_study;
    public int max_project;
    public int max_test;

    public float max_time;
    public float inc_time;
    public float inc_MH;

    public bool unlock_project;
    public bool unlock_test;
    public bool offlineProgress;

    public int level;
    private float timer;

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

        study = 0;
        project = 0;
        test = 0;
        MH = 10;
        time = 1;
        coin = 0;

        max_study = 150;
        max_project = 60;
        max_test = 30;

        max_time = 60;
        inc_time = 1/60f;
        inc_MH = 0.5f;

        unlock_project = false;
        unlock_test = false;
        */
        
        Load();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time: " + inc_time + " MH: " + inc_MH);

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

        study = PlayerPrefs.GetInt("study", 0);
        project = PlayerPrefs.GetInt("project", 0);
        test = PlayerPrefs.GetInt("test", 0);
        time = PlayerPrefs.GetFloat("time", 1);
        MH = PlayerPrefs.GetFloat("MH", 10);
        coin = PlayerPrefs.GetInt("coin", 0);

        max_study = PlayerPrefs.GetInt("max_study", 150);
        max_project = PlayerPrefs.GetInt("max_project", 60);
        max_test = PlayerPrefs.GetInt("max_test", 5);

        max_time = PlayerPrefs.GetFloat("max_time", 6);
        inc_time = PlayerPrefs.GetFloat("inc_time", 0.016666666667f);
        inc_MH = PlayerPrefs.GetFloat("inc_MH", 0.5f);

        unlock_project = bool.Parse(PlayerPrefs.GetString("unlock_project", "False"));
        unlock_test = bool.Parse(PlayerPrefs.GetString("unlock_test", "False"));


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

        PlayerPrefs.SetString("unlock_project", unlock_project.ToString());
        PlayerPrefs.SetString("unlock_test", unlock_test.ToString());

        PlayerPrefs.Save();
        //Debug.Log("Saved");

    }
}
