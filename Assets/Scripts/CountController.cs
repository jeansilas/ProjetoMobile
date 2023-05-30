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
    }

    void Update(){ 
        timer += Time.deltaTime;
        if (timer >= 1){
            updateCountTime(levelController.inc_time);
            updateCountMentalHealth(levelController.inc_MH);
            timer = 0;
        }
    }

    public void buyStudy(){
        if (levelController.MH < levelController.price_study_MH || levelController.time < levelController.price_study_time){
            return;
        }
        updateCountStudy(1);
        updateCountMentalHealth(-levelController.price_study_MH);
        updateCountTime(-levelController.price_study_time);
        levelController.study += 1;

        levelController.inc_time *= 1.1f;
        levelController.inc_MH *= 1.1f;

        levelController.price_study_MH += levelController.price_study_MH*0.1f;
        levelController.price_study_time += levelController.price_study_time*0.1f;
        price_study.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_study_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";

        if (levelController.study == 10){
            levelController.unlock_project = true;
            unlockProject.SetActive(false);
        }
    }

    public void buyProject(){
        if (levelController.unlock_project){
            if (levelController.MH < levelController.price_project_MH || levelController.time < levelController.price_project_time){
                return;
            }
            updateCountProject(1);
            updateCountMentalHealth(-levelController.price_project_MH);
            updateCountTime(-levelController.price_project_time);
            levelController.project += 1;

            levelController.inc_time *= 1.5f;
            levelController.inc_MH *= 1.5f;

            levelController.price_project_MH += levelController.price_project_MH*0.1f;
            levelController.price_project_time += levelController.price_project_time*0.1f;
            price_project.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_project_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
            
            if (levelController.project/levelController.max_project >= 0.5f){
                levelController.unlock_test = true;
                unlockTest.SetActive(false);
            }
        }
    }

    public void buyTest(){
        if (levelController.unlock_test){
            if (levelController.MH < levelController.price_test_MH || levelController.time < levelController.price_test_time){
                return;
            }
            updateCountTest(1);
            updateCountMentalHealth(-levelController.price_test_MH);
            updateCountTime(-levelController.price_test_time);
            levelController.test += 1;

            levelController.inc_time *= 1.8f;
            levelController.inc_MH *= 1.8f;

            levelController.price_test_MH += levelController.price_test_MH*0.2f;
            levelController.price_test_time += levelController.price_test_time*0.2f;
            price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";

        }
    }

    public void updateCountMentalHealth(float discreetValue)
    {
        foreach (TextMeshProUGUI count in CountMentalHealth)
        {
            // Get the current text value
            string currentValue = count.text;

            // Convert the text value to an integer using int.Parse()
            float intValue;
            if (float.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = Math.Round(intValue + discreetValue, 2).ToString();
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
            string currentValue = count.text;

            // Convert the text value to an integer using int.Parse()
            float intValue;
            if (float.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0 && intValue + discreetValue < levelController.max_time) {
                    count.text = Math.Round(intValue + discreetValue, 2).ToString();
                    levelController.time = intValue + discreetValue;
                    barController.updateBarTime(discreetValue);
                } else if (intValue + discreetValue >= levelController.max_time){
                    count.text = levelController.max_time.ToString();
                    levelController.time = intValue + discreetValue;
                    barController.updateBarTime(discreetValue);
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
        foreach (TextMeshProUGUI count in CountStudy)
        {
            // Get the current text value
            string currentValue = count.text;

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    barController.updateBarStudy(discreetValue);
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
            string currentValue = count.text;

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    barController.updateBarProject(discreetValue);
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
            string currentValue = count.text;

            // Convert the text value to an integer using int.Parse()
            int intValue;
            if (int.TryParse(currentValue, out intValue))
            {
                // Conversion successful
                if (intValue + discreetValue >= 0) {
                    count.text = (intValue + discreetValue).ToString();
                    barController.updateBarTest(discreetValue);
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
            string currentValue = count.text;

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
