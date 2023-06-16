using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class CountController : MonoBehaviour
{
    private string tagTime = "CountTime";
    private string tagCoin = "CountCoin";
    private string tagMentalHealth = "CountMentalHealth";
    private string tagStudy = "CountStudy";
    private string tagProject = "CountProject";
    private string tagTest = "CountTest";

    private TextMeshProUGUI[] CountTime;
    private  TextMeshProUGUI[] CountCoin;
    private  TextMeshProUGUI[] CountMentalHealth;
    private  TextMeshProUGUI[] CountStudy;
    private  TextMeshProUGUI[] CountProject;
    private  TextMeshProUGUI[] CountTest;
    private GameObject price_study;
    private GameObject price_project;
    private GameObject price_test;
    private GameObject unlockProject;
    private GameObject unlockTest;

    private LevelController levelController;
    private BarController barController;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        CountTime = GameObject.FindGameObjectsWithTag(tagTime)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();
        
        CountStudy = GameObject.FindGameObjectsWithTag(tagStudy)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();

        CountProject = GameObject.FindGameObjectsWithTag(tagProject)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();
        
        CountTest = GameObject.FindGameObjectsWithTag(tagTest)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();
        
        CountCoin = GameObject.FindGameObjectsWithTag(tagCoin)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();

        CountMentalHealth = GameObject.FindGameObjectsWithTag(tagMentalHealth)
            .Select(go => go.GetComponent<TextMeshProUGUI>())
            .ToArray();

        levelController = GameObject.Find("Menu").GetComponent<LevelController>();
        barController = GameObject.Find("Menu").GetComponent<BarController>();
        price_study = GameObject.FindGameObjectWithTag("priceStudy");
        price_project = GameObject.FindGameObjectWithTag("priceProject");
        price_test = GameObject.FindGameObjectWithTag("priceTest");
        unlockProject = GameObject.FindGameObjectWithTag("unlockProject");
        unlockTest = GameObject.FindGameObjectWithTag("unlockTest");

        price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
        price_project.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_project_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
        price_study.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_study_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
        updateCountTime(0);
        updateCountMentalHealth(0);        
        updateCountStudy(0);    
        updateCountProject(0);
        updateCountTest(0);
        updateCountCoin(0);

        unlockProject.SetActive(!levelController.unlock_project);
        unlockTest.SetActive(!levelController.unlock_test);
        
    }

    void Update(){ 
        timer += Time.deltaTime;
        if (timer >= 1){
            var inc_MH = levelController.inc_MH + levelController.inc_MH * levelController.study*levelController.inc_study + levelController.inc_MH * levelController.project*levelController.inc_project + levelController.inc_MH * levelController.test*levelController.inc_test;
            var inc_time = levelController.inc_time + levelController.inc_time * levelController.study*levelController.inc_study + levelController.inc_time * levelController.project*levelController.inc_project + levelController.inc_time * levelController.test*levelController.inc_test;
            updateCountTime(inc_time);
            updateCountMentalHealth(inc_MH);
            timer = 0;
        }
    }

    public void buyStudy(){
        if (levelController.study < levelController.max_study){
            if (levelController.MH < levelController.price_study_MH || levelController.time < levelController.price_study_time){
                return;
            }
            updateCountStudy(1);
            updateCountMentalHealth(-levelController.price_study_MH);
            updateCountTime(-levelController.price_study_time);
            levelController.study += 1;

            levelController.price_study_MH = levelController.price_study_MH_og * (float) Math.Pow(levelController.inc_study, levelController.study);
            levelController.price_study_time = levelController.price_study_time_og * (float) Math.Pow(levelController.inc_study, levelController.study);

            if (levelController.price_study_time > levelController.max_time){
                levelController.price_study_time = levelController.max_time;
            }

            if (levelController.price_study_MH >= 1000){
                var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_study_MH)));
                var rounded = Math.Round((levelController.price_study_MH) / Math.Pow(10, exponent), 2);
                price_study.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
            } else {
                price_study.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_study_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
            }

            if (levelController.study == 10){
                levelController.unlock_project = true;
                unlockProject.SetActive(false);
            }
        } 
    }

    public void buyProject(){
        if (levelController.unlock_project && levelController.project < levelController.max_project){
            if (levelController.MH < levelController.price_project_MH || levelController.time < levelController.price_project_time){
                return;
            }
            updateCountProject(1);
            updateCountMentalHealth(-levelController.price_project_MH);
            updateCountTime(-levelController.price_project_time);
            levelController.project += 1;

            levelController.price_project_MH = levelController.price_project_MH_og * (float) Math.Pow(levelController.inc_project, levelController.project);
            levelController.price_project_time = levelController.price_project_time_og * (float) Math.Pow(levelController.inc_project, levelController.project);

            if (levelController.price_project_time > levelController.max_time){
                levelController.price_project_time = levelController.max_time;
            }

            if (levelController.price_project_MH >= 1000){
                var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_project_MH)));
                var rounded = Math.Round((levelController.price_project_MH) / Math.Pow(10, exponent), 2);
                price_project.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
            } else {
                price_project.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_project_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
            }

            if (levelController.project/levelController.max_project >= 0.5f){
                levelController.unlock_test = true;
                unlockTest.SetActive(false);
            }
        } else if (levelController.project == levelController.max_project){
            levelController.done_project = true;
        }
    }

    public void buyTest(){
        if (levelController.unlock_test && levelController.test < levelController.max_test){
            if (levelController.MH < levelController.price_test_MH || levelController.time < levelController.price_test_time){
                return;
            }
            updateCountTest(1);
            updateCountMentalHealth(-levelController.price_test_MH);
            updateCountTime(-levelController.price_test_time);
            levelController.test += 1;

            levelController.price_test_MH = levelController.price_test_MH_og * (float) Math.Pow(levelController.inc_test, levelController.test);
            levelController.price_test_time = levelController.price_test_time_og * (float) Math.Pow(levelController.inc_test, levelController.test);

            if (levelController.price_test_time > levelController.max_time){
                levelController.price_test_time = levelController.max_time;
            }

            if (levelController.price_test_MH >= 1000){
                var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_test_MH)));
                var rounded = Math.Round((levelController.price_test_MH) / Math.Pow(10, exponent), 2);
                price_test.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
            } else {
                price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
            }
            
            price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";

        } else if (levelController.test == levelController.max_test){
            levelController.done_test = true;
        }
    }

    public void updateCountMentalHealth(float discreetValue)
    {
        foreach (TextMeshProUGUI count in CountMentalHealth)
        {
            // Get the current text value
            string currentValue = levelController.MH.ToString();

            // Convert the text value to an integer using int.Parse()
            float intValue;
            if (float.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    if (intValue + discreetValue >= 1000){
                        var exponent = Math.Floor(Math.Log10(Math.Abs(intValue + discreetValue)));
                        var rounded = Math.Round((intValue + discreetValue) / Math.Pow(10, exponent), 2);
                        count.text = rounded.ToString("F2") + "e+" + exponent.ToString();
                    } else {
                        count.text = Math.Round(intValue + discreetValue, 2).ToString();
                    }
                    levelController.MH = intValue + discreetValue;
                } 
                
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }
    
    public void updateCountTime(float discreetValue)
    {
        foreach (TextMeshProUGUI count in CountTime)
        {
            // Get the current text value
            string currentValue = levelController.time.ToString();

            // Convert the text value to an integer using int.Parse()
            float intValue;
            if (float.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0 && intValue + discreetValue < levelController.max_time) {
                    count.text = Math.Round(intValue + discreetValue, 2).ToString();
                    levelController.time = intValue + discreetValue;
                    if (discreetValue > 0){
                        barController.updateBarTime(discreetValue);
                    }
                } else if (intValue + discreetValue >= levelController.max_time){
                    count.text = levelController.max_time.ToString();
                    levelController.time = intValue + discreetValue;
                    if (discreetValue > 0){
                        barController.updateBarTime(discreetValue);
                    }
                } else{
                    count.text = "0";
                    barController.updateBarTime(-levelController.time);
                    levelController.time = 0;
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void updateCountStudy(int discreetValue)
    {
        //Debug.Log("updateCountStudy");
        foreach (TextMeshProUGUI count in CountStudy)
        {
            // Get the current text value
            string currentValue = levelController.study.ToString();

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    if (discreetValue > 0){
                        barController.updateBarStudy(discreetValue);
                    }
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void updateCountProject(int discreetValue)
    {
        foreach (TextMeshProUGUI count in CountProject)
        {
            // Get the current text value
            string currentValue = levelController.project.ToString();

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    if (discreetValue > 0){
                        barController.updateBarProject(discreetValue);
                    }
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void updateCountTest(int discreetValue)
    {
        foreach (TextMeshProUGUI count in CountTest)
        {
            // Get the current text value
            string currentValue = levelController.test.ToString();

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    if (discreetValue > 0){
                        barController.updateBarTest(discreetValue);
                    }
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void updateCountCoin(int discreetValue)
    {
        foreach (TextMeshProUGUI count in CountCoin)
        {
            // Get the current text value
            string currentValue = levelController.coin.ToString();

            // Convert the text value to an integer using int.Parse()
            // Trocar pra float
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    
}
