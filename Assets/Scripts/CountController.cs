using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

// Reference for scientific notation: https://www.youtube.com/watch?v=DXjgMOee1Eg&list=PLGSUBi8nI9v9I5uaSRe8ccSV02W2dyxGM&index=7
// Reference for formulas: https://blog.kongregate.com/the-math-of-idle-games-part-i/

public class CountController : MonoBehaviour
{
    private string tagTime = "CountTime";
    private string tagCoin = "CountCoin";
    private string tagMentalHealth = "CountMentalHealth";
    private string tagStudy = "CountStudy";
    private string tagProject = "CountProject";
    private string tagTest = "CountTest";
    private string tagUpgradeTemporary = "UpgradeTemporary";
    private string tagPopUp = "Settings";

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
    private GameObject[] PanelPopUps;

    public List<GameObject> upgradesTemporary;
    public List<GameObject> upgradesPermanent;

    private LevelController levelController;
    private BarController barController;
    private float timer;
    // Start is called before the first frame update
    void Start(){

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
        PanelPopUps = GameObject.FindGameObjectsWithTag(tagPopUp);
        upgradesTemporary = new List<GameObject>();

        if (levelController.price_study_MH >= 1000 && levelController.study != levelController.max_study){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_study_MH)));
            var rounded = Math.Round((levelController.price_study_MH) / Math.Pow(10, exponent), 2);
            price_study.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
        } else if (levelController.study == levelController.max_study){
            price_study.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Study").GetComponent<Button>().interactable = false;
        } else{
            price_study.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_study_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
        }

        if (levelController.price_project_MH >= 1000 && levelController.project != levelController.max_project){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_project_MH)));
            var rounded = Math.Round((levelController.price_project_MH) / Math.Pow(10, exponent), 2);
            price_project.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
        } else if (levelController.project == levelController.max_project){
            price_project.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Project").GetComponent<Button>().interactable = false;
        } else{
            price_project.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_project_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
        }
        
        if (levelController.price_test_MH >= 1000 && levelController.test != levelController.max_test){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_test_MH)));
            var rounded = Math.Round((levelController.price_test_MH) / Math.Pow(10, exponent), 2);
            price_test.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
        } else if (levelController.test == levelController.max_test){
            price_test.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Test").GetComponent<Button>().interactable = false;
        } else{
            price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
        }
        
        //updateCountTime(0);
        updateCountMentalHealth(0);        
        updateCountStudy(0);    
        updateCountProject(0);
        updateCountTest(0);
        updateCountCoin(0);

        if (unlockProject != null){
            unlockProject.SetActive(!levelController.unlock_project);
        }
        if (unlockTest != null){
            unlockTest.SetActive(!levelController.unlock_test);
        }
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

        if (levelController.study == levelController.max_study){
            price_study.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Study").GetComponent<Button>().interactable = false;
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

            if (levelController.project >= levelController.max_project * 0.5){
                levelController.unlock_test = true;
                unlockTest.SetActive(false);
            }
        }

        if (levelController.project == levelController.max_project){
            price_project.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Project").GetComponent<Button>().interactable = false;
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

        }

        if (levelController.test == levelController.max_test){
            price_test.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Test").GetComponent<Button>().interactable = false;
        }
        
    }

    public void updateCountMentalHealth(float discreetValue){
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
    
    public void updateCountTime(float discreetValue){
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
                    barController.updateBarTime(discreetValue);
                } else if (intValue + discreetValue >= levelController.max_time){
                    count.text = levelController.max_time.ToString();
                    levelController.time = levelController.max_time;
                    barController.updateBarTime(discreetValue);
                    
                } else{
                    //count.text = "0";
                    //barController.updateBarTime(-levelController.time);
                    //levelController.time = 0;
                    return;
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void updateCountStudy(int discreetValue){
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

    public void updateCountProject(int discreetValue){
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

    public void updateCountTest(int discreetValue){
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

    public void updateCountCoin(int discreetValue){
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
                    levelController.coin = intValue + discreetValue;
                    Debug.Log("Coins: " + levelController.coin);
                }
            }
            else
            {
                // Conversion failed
                Debug.Log("Invalid integer value");
            }
        }
    }

    public void upgradeTemporary(GameObject upgrade) {

        string type = upgrade.tag;
        GameObject Title = upgrade.transform.Find("Title").gameObject;
        float MH = 0;
        float time = 0;
        
        if (type == "UpgradeTemporary1") { // type1: Reduce MH costs
            for (int i = 0; i < levelController.upgradeTemporaryType1.Count; i++) {
                if (levelController.upgradeTemporaryType1[i]["Title"] == Title.GetComponent<TextMeshProUGUI>().text) {
                    MH = int.Parse(levelController.upgradeTemporaryType1[i]["MH"]);
                    time = float.Parse(levelController.upgradeTemporaryType1[i]["time"]);
                    break;
                }
            }
            if (levelController.MH < MH || levelController.time < time){
                return;
            } else {
                updateCountMentalHealth(-MH);
                updateCountTime(-time);
                levelController.price_study_MH_og = levelController.price_study_MH_og * 0.9f;
                levelController.price_project_MH_og = levelController.price_project_MH_og * 0.9f;
                levelController.price_test_MH_og = levelController.price_test_MH_og * 0.9f;

                updateCountStudy(0);
                updateCountProject(0);
                updateCountTest(0);
        
            }
        } else if (type == "UpgradeTemporary2") { // type2: Aumenta a taxa de ganho de Mental Health por tempo. Ao invés de ganhar 0.5 MH por minuto, ganha 1 MH por minuto   (Taxa/percentual)
            for (int i = 0; i < levelController.upgradeTemporaryType2.Count; i++) {
                if (levelController.upgradeTemporaryType2[i]["Title"] == Title.GetComponent<TextMeshProUGUI>().text) {
                   MH = int.Parse(levelController.upgradeTemporaryType1[i]["MH"]);
                   time = float.Parse(levelController.upgradeTemporaryType1[i]["time"]);
                    break;
                    }
                }
            if (levelController.MH < MH || levelController.time < time){
                return;
            }

            else {
                updateCountMentalHealth(-MH);
                updateCountTime(-time);
                levelController.inc_MH = levelController.inc_MH*1.5f;
            }

        }

        else {  // type3: Reduce Time costs

            for (int i = 0; i < levelController.upgradeTemporaryType3.Count; i++) {
                if (levelController.upgradeTemporaryType3[i]["Title"] == Title.GetComponent<TextMeshProUGUI>().text) {
                    MH = int.Parse(levelController.upgradeTemporaryType1[i]["MH"]);
                    time = float.Parse(levelController.upgradeTemporaryType1[i]["time"]);
                    break;
                }
                if (levelController.MH < MH || levelController.time < time){
                return;
                }

                else {
                    updateCountMentalHealth(-MH);
                    updateCountTime(-time);
                    levelController.price_study_time_og = levelController.price_study_time_og * 0.9f;
                    levelController.price_project_time_og = levelController.price_project_time_og * 0.9f;
                    levelController.price_test_time_og = levelController.price_test_time_og * 0.9f;

                    updateCountStudy(0);
                    updateCountProject(0);
                    updateCountTest(0);
                }
            }
        }

        if (levelController.price_study_MH >= 1000 && levelController.study != levelController.max_study){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_study_MH)));
            var rounded = Math.Round((levelController.price_study_MH) / Math.Pow(10, exponent), 2);
            price_study.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
        } else if (levelController.study == levelController.max_study){
            price_study.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Study").GetComponent<Button>().interactable = false;
        } else{
            price_study.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_study_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_study_time, 2).ToString() + " Time";
        }

        if (levelController.price_project_MH >= 1000 && levelController.project != levelController.max_project){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_project_MH)));
            var rounded = Math.Round((levelController.price_project_MH) / Math.Pow(10, exponent), 2);
            price_project.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
        } else if (levelController.project == levelController.max_project){
            price_project.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Project").GetComponent<Button>().interactable = false;
        } else{
            price_project.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_project_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_project_time, 2).ToString() + " Time";
        }
        
        if (levelController.price_test_MH >= 1000 && levelController.test != levelController.max_test){
            var exponent = Math.Floor(Math.Log10(Math.Abs(levelController.price_test_MH)));
            var rounded = Math.Round((levelController.price_test_MH) / Math.Pow(10, exponent), 2);
            price_test.transform.GetComponent<TextMeshProUGUI>().text = rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
        } else if (levelController.test == levelController.max_test){
            price_test.transform.GetComponent<TextMeshProUGUI>().text = "MAX";
            GameObject.Find("Test").GetComponent<Button>().interactable = false;
        } else{
            price_test.transform.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_test_MH,0).ToString() + " Mental Health / " + Math.Round(levelController.price_test_time, 2).ToString() + " Time";
        }
    }

    public void upgradePermanent(int type) {

        
        if (type == 1) { // type1: Aumenta a quantidade de tempo exemplo:  vai de 3h para 6h (Absoluto)
            int coin = levelController.price_coin_upgrade_permanent_type1;
            if (levelController.coin < coin){
            return;
            }

            else {
                updateCountCoin(-coin);
                levelController.max_time += 1 ;
            }
        }


    }

    public void InstantiateUpgradeTemporary() {
        int j = 0;
        GameObject upgradePanel = GameObject.Find("UpgradeTemporaryPanel");
        GameObject item = (GameObject)Resources.Load("Templates/UpgradeTemporary", typeof(GameObject));
        Transform Title;
        Transform ContentText;
        Transform Icon;
        Transform TimeCount;
        Transform MentalHealthCount;

        for (int i = 0; i < levelController.upgradeTemporaryType1Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType1.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
                upgrade.tag = "UpgradeTemporary1";
                upgradesTemporary.Add(upgrade);

                Title = upgrade.transform.Find("Title");
                Title.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType1[i]["Title"];

                ContentText = upgrade.transform.Find("ContentText");
                ContentText.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType1[i]["ContentText"];

                Icon = upgrade.transform.Find("Icon");
                byte[] bytes = System.IO.File.ReadAllBytes(levelController.upgradeTemporaryType1[i]["Icon"]);
                Texture2D texture = new Texture2D(70, 60);
                texture.LoadImage(bytes);

                Icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));

                RectTransform rectTransform = Icon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 60);

                TimeCount = upgrade.transform.Find("Cost").transform.Find("Time").transform.Find("TimeCount");

                MentalHealthCount = upgrade.transform.Find("Cost").transform.Find("MentalHealth").transform.Find("MentalHealthCount");

                float MH = levelController.price_MH_upgrade_temporary_type1;
                levelController.upgradeTemporaryType1[i].Add("MH", MH.ToString());
                float time = levelController.price_time_upgrade_temporary_type1;
                levelController.upgradeTemporaryType1[i].Add("Time", time.ToString());

                TimeCount.GetComponent<TextMeshProUGUI>().text = Math.Round(time, 0).ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH,0).ToString();
            }
        }

        for (int i = 0; i < levelController.upgradeTemporaryType2Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType2.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
                upgrade.tag = "UpgradeTemporaryType2";
                upgradesTemporary.Add(upgrade);

                Title = upgrade.transform.Find("Title");
                Title.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType2[i]["Title"];

                ContentText = upgrade.transform.Find("ContentText");
                ContentText.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType2[i]["ContentText"];

                Icon = upgrade.transform.Find("Icon");
                byte[] bytes = System.IO.File.ReadAllBytes(levelController.upgradeTemporaryType2[i]["Icon"]);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);
    
                Icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));

                RectTransform rectTransform = Icon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 60);

                TimeCount = upgrade.transform.Find("Cost").transform.Find("Time").transform.Find("TimeCount");

                MentalHealthCount = upgrade.transform.Find("Cost").transform.Find("MentalHealth").transform.Find("MentalHealthCount");

                

                var t = 0;
                var MH = levelController.price_MH_upgrade_temporary_type2*Math.Pow(levelController.upgradeTemporaryCoefficient, i);

                levelController.upgradeTemporaryType2[i].Add("Price", MH.ToString());
                levelController.upgradeTemporaryType2[i].Add("Time", t.ToString());

                TimeCount.GetComponent<TextMeshProUGUI>().text = t.ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH,0).ToString();
            }
        }

        for (int i = 0; i < levelController.upgradeTemporaryType3Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType3.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
                upgrade.tag = "UpgradeTemporaryType3";
                upgradesTemporary.Add(upgrade);

                Title = upgrade.transform.Find("Title");
                Title.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType3[i]["Title"];

                ContentText = upgrade.transform.Find("ContentText");
                ContentText.GetComponent<TextMeshProUGUI>().text = levelController.upgradeTemporaryType3[i]["ContentText"];

                Icon = upgrade.transform.Find("Icon");
                byte[] bytes = System.IO.File.ReadAllBytes(levelController.upgradeTemporaryType3[i]["Icon"]);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);

                Icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));

                RectTransform rectTransform = Icon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 60);

                TimeCount = upgrade.transform.Find("Cost").transform.Find("Time").transform.Find("TimeCount");

                MentalHealthCount = upgrade.transform.Find("Cost").transform.Find("MentalHealth").transform.Find("MentalHealthCount");

                float MH = levelController.price_MH_upgrade_temporary_type3;

                levelController.upgradeTemporaryType3[i].Add("MH", MH.ToString());
                

                float time = levelController.price_time_upgrade_temporary_type3;

                levelController.upgradeTemporaryType3[i].Add("Time", time.ToString());


                TimeCount.GetComponent<TextMeshProUGUI>().text = Math.Round(time, 0).ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH, 0).ToString();
            }
        }

    }

    public void InstantiateUpgradePermanent() {
        int j = 0;
        GameObject upgradePanel = GameObject.Find("UpgradePermanentPanel");
        GameObject item = (GameObject)Resources.Load("Templates/UpgradePermanent", typeof(GameObject));
        Transform Title;
        Transform ContentText;
        Transform Icon;
        Transform MoneyCount;

        for (int i = 0; i < levelController.upgradePermanentType1Amount; i++){
            if (!levelController.boughtUpgradePermanentType1.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
                upgradesPermanent.Add(upgrade);

                Title = upgrade.transform.Find("Title");
                Title.GetComponent<TextMeshProUGUI>().text = levelController.upgradePermanentType1[i]["Title"];

                ContentText = upgrade.transform.Find("ContentText");
                ContentText.GetComponent<TextMeshProUGUI>().text = levelController.upgradePermanentType1[i]["ContentText"];

                Icon = upgrade.transform.Find("Icon");
                byte[] bytes = System.IO.File.ReadAllBytes(levelController.upgradePermanentType1[i]["Icon"]);
                Texture2D texture = new Texture2D(70, 60);
                texture.LoadImage(bytes);

                Icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));

                RectTransform rectTransform = Icon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 60);

                MoneyCount = upgrade.transform.Find("Money").transform.Find("MoneyCount");


                int Coin = levelController.price_coin_upgrade_permanent_type1;

                MoneyCount.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_time_upgrade_temporary_type1, 0).ToString();
            }
        }

       
        

    }

    

    

    
}