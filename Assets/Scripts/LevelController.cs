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

    public float price_time_upgrade_temporary_type1;
    public float price_time_upgrade_temporary_type2;
    public float price_time_upgrade_temporary_type3;

    public float upgradeTemporaryCoefficient;

    public int upgradeTemporaryType1Amount;
    public int upgradeTemporaryType2Amount;
    public int upgradeTemporaryType3Amount;

    public int upgradePermanentType1Amount;

    public List<Dictionary<string, string>> upgradeTemporaryType1;
    public List<Dictionary<string, string>> upgradeTemporaryType2;
    public List<Dictionary<string, string>> upgradeTemporaryType3;

    public List<Dictionary<string, string>> upgradePermanentType1;

    public List<int> boughtUpgradeTemporaryType1;
    public List<int> boughtUpgradeTemporaryType2;
    public List<int> boughtUpgradeTemporaryType3;

    public List<int> boughtUpgradePermanentType1;

    // Start is called before the first frame update
    void Start()
    {
        Load();
        offlineController = GameObject.Find("Menu").GetComponent<OfflineController>();
        offlineController.LoadOfflineProduction();

        upgradeTemporaryType1 = new List<Dictionary<string, string>>();
        upgradeTemporaryType2 = new List<Dictionary<string, string>>();
        upgradeTemporaryType3 = new List<Dictionary<string, string>>();

        upgradePermanentType1 = new List<Dictionary<string, string>>();

        Dictionary<string, string> upgradeTemporaryType1_1 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType1_2 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_1 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_2 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_3 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_4 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_5 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_6 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType2_7 = new Dictionary<string, string>();

        Dictionary<string, string> upgradeTemporaryType3_1 = new Dictionary<string, string>();
        Dictionary<string, string> upgradeTemporaryType3_2 = new Dictionary<string, string>();

        Dictionary<string, string> upgradePermanentType1_1 = new Dictionary<string, string>();
        Dictionary<string, string> upgradePermanentType1_2 = new Dictionary<string, string>();

        upgradeTemporaryType1_1.Add("Title", "Energetic");
        upgradeTemporaryType1_1.Add("ContentText", "You are feeling lazy, drink some RedBull and fly away!");
        upgradeTemporaryType1_1.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");
        
        upgradeTemporaryType1_2.Add("Title", "a");
        upgradeTemporaryType1_2.Add("ContentText", "a");
        upgradeTemporaryType1_2.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_1.Add("Title", "Tasty Cookie to give you that uplifting feeling");
        upgradeTemporaryType2_1.Add("ContentText", "You were scrolling through the internet and found a recipe for a cookie that gives you a lot of serotonin. You decide to make it and eat it.");
        upgradeTemporaryType2_1.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_2.Add("Title", "Break Time");
        upgradeTemporaryType2_2.Add("ContentText", "As the Pomodoro technique says: Sometimes, a break is better than pushing too hard with your studies.");
        upgradeTemporaryType2_2.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_3.Add("Title", "Plant");
        upgradeTemporaryType2_3.Add("ContentText", "A plant is a nice friend to have, it makes you feel better and it is a nice decoration for your room.");
        upgradeTemporaryType2_3.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_4.Add("Title", "Man's Best Friend");
        upgradeTemporaryType2_4.Add("ContentText", "Your friend passed by your house and brought along his cute puppy, play a bit with it and feel better.");
        upgradeTemporaryType2_4.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_5.Add("Title", "Cutest thing in the world");
        upgradeTemporaryType2_5.Add("ContentText", "You have the opportunity to have a cat in your lap, and you take it. You pet it and feel better.");
        upgradeTemporaryType2_5.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_6.Add("Title", "a");
        upgradeTemporaryType2_6.Add("ContentText", "a");
        upgradeTemporaryType2_6.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType2_7.Add("Title", "a");
        upgradeTemporaryType2_7.Add("ContentText", "a");
        upgradeTemporaryType2_7.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType3_1.Add("Title", "Study Group");
        upgradeTemporaryType3_1.Add("ContentText", "You are feeling lonely, call your clever friends and study together.");
        upgradeTemporaryType3_1.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType3_2.Add("Title", "a");
        upgradeTemporaryType3_2.Add("ContentText", "a");
        upgradeTemporaryType3_2.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradePermanentType1_1.Add("Title", "Wake up early");
        upgradePermanentType1_1.Add("ContentText", "You decide to change your life and wake up early.");
        upgradePermanentType1_1.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradePermanentType1_2.Add("Title", "Sleep Late");
        upgradePermanentType1_2.Add("ContentText", "You decide to change your life and sleep late.");
        upgradePermanentType1_2.Add("Icon", "Assets/Resources/Icons/gatinho_pixel.png");

        upgradeTemporaryType1.Add(upgradeTemporaryType1_1);
        upgradeTemporaryType1.Add(upgradeTemporaryType1_2);

        upgradeTemporaryType2.Add(upgradeTemporaryType2_1);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_2);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_3);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_4);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_5);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_6);
        upgradeTemporaryType2.Add(upgradeTemporaryType2_7);

        upgradeTemporaryType3.Add(upgradeTemporaryType3_1);
        upgradeTemporaryType3.Add(upgradeTemporaryType3_2);

        upgradePermanentType1.Add(upgradePermanentType1_1);
        upgradePermanentType1.Add(upgradePermanentType1_2);

        upgradePermanentType1Amount = 2;
        price_coin_upgrade_permanent_type1 = 50;

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

        price_time_upgrade_temporary_type1 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type1", 3.0f);
        price_time_upgrade_temporary_type2 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type2", 0f);
        price_time_upgrade_temporary_type3 = PlayerPrefs.GetFloat("price_time_upgrade_temporary_type3", 3.5f);

        upgradeTemporaryCoefficient = PlayerPrefs.GetFloat("upgradeTemporaryCoefficient", 3.77f);

        upgradeTemporaryType1Amount = PlayerPrefs.GetInt("upgradeTemporaryType1Amount", 1);
        upgradeTemporaryType2Amount = PlayerPrefs.GetInt("upgradeTemporaryType2Amount", 5);
        upgradeTemporaryType3Amount = PlayerPrefs.GetInt("upgradeTemporaryType3Amount", 1);

        upgradePermanentType1Amount = PlayerPrefs.GetInt("upgradePermanentType1Amount", 2);

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

        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type1", price_time_upgrade_temporary_type1);
        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type2", price_time_upgrade_temporary_type2);
        PlayerPrefs.SetFloat("price_time_upgrade_temporary_type3", price_time_upgrade_temporary_type3);

        PlayerPrefs.SetFloat("upgradeTemporaryCoefficient", upgradeTemporaryCoefficient);

        PlayerPrefs.SetInt("upgradeTemporaryType1Amount", upgradeTemporaryType1Amount-boughtUpgradeTemporaryType1.Count);
        PlayerPrefs.SetInt("upgradeTemporaryType2Amount", upgradeTemporaryType2Amount-boughtUpgradeTemporaryType2.Count);
        PlayerPrefs.SetInt("upgradeTemporaryType3Amount", upgradeTemporaryType3Amount-boughtUpgradeTemporaryType3.Count);

        PlayerPrefs.SetInt("upgradePermanentType1Amount", upgradePermanentType1Amount - boughtUpgradePermanentType1.Count);

        PlayerPrefs.Save();

    }
}
