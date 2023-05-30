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

    public int max_study;
    public int max_project;
    public int max_test;

    public float max_time;
    public float inc_time;
    public float inc_MH;

    public bool unlock_project;
    public bool unlock_test;

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

        max_study = 150;
        max_project = 60;
        max_test = 5;

        max_time = 6;
        inc_time = 1/60f;
        inc_MH = 0.5f;

        unlock_project = false;
        unlock_test = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time: " + inc_time + " MH: " + inc_MH);
    }
}
