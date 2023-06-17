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
        
        updateCountTime(0);
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

    public void upgradeTemporary(int type) {

        
        if (type == 1) { // type1: Aumenta a quantidade de tempo exemplo:  vai de 3h para 6h (Absoluto)
            int MH = levelController.price_MH_upgrade_temporary_type1;
            float time = levelController.price_time_upgrade_temporary_type1;
            if (levelController.MH < MH || levelController.time < time){
            return;
            }

            else {
                updateCountMentalHealth(-MH);
                updateCountTime(-time);
                levelController.time = levelController.time*2;
            }
        }

        else if (type == 2) { // type2: Aumenta a taxa de ganho de Mental Health por tempo. Ao invés de ganhar 0.5 MH por minuto, ganha 1 MH por minuto   (Taxa/percentual)
           int MH = levelController.price_MH_upgrade_temporary_type2;
            float time = levelController.price_time_upgrade_temporary_type2;
            if (levelController.MH < MH || levelController.time < time){
                return;
            }

            else {
                updateCountMentalHealth(-MH);
                updateCountTime(-time);
                levelController.inc_MH = levelController.inc_MH*2;
            }

        }

        else {  // type3: Aumenta a taxa de ganho de Mental Health por tempo. Ao invés de ganhar 0.5 MH por minuto, ganha 1 MH por minuto   (Taxa/percentual)
            int MH = levelController.price_MH_upgrade_temporary_type3;
            float time = levelController.price_time_upgrade_temporary_type3;
            if (levelController.MH < MH || levelController.time < time){
            return;
            }

            else {
                updateCountMentalHealth(-MH);
                updateCountTime(-time);
                levelController.inc_MH = levelController.inc_MH*2;
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

        levelController.boughtUpgradeTemporaryType1.Add(0);

        for (int i = 0; i < levelController.upgradeTemporaryType1Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType1.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
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

                TimeCount.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_time_upgrade_temporary_type1, 0).ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH,0).ToString();
            }
        }

        for (int i = 0; i < levelController.upgradeTemporaryType2Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType2.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
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

                TimeCount.GetComponent<TextMeshProUGUI>().text = t.ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH,0).ToString();
            }
        }

        for (int i = 0; i < levelController.upgradeTemporaryType3Amount; i++){
            if (!levelController.boughtUpgradeTemporaryType3.Contains(i)){
                GameObject upgrade = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                upgrade.transform.SetParent(upgradePanel.transform, false);
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

                TimeCount.GetComponent<TextMeshProUGUI>().text = Math.Round(levelController.price_time_upgrade_temporary_type3, 0).ToString();
                MentalHealthCount.GetComponent<TextMeshProUGUI>().text = Math.Round(MH, 0).ToString();
            }
        }

    }   
}