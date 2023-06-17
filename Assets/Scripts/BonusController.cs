using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusController : MonoBehaviour
{
    private LevelController levelController;
    private RewardedAdsButton rewardedAdsButton;
    private GameObject bonusPanel;
    private BarController barController;
    private CountController countController;

    private float timer;
    private bool show;
    private int rand;

    void Start()
    {
        levelController = GameObject.Find("Menu").GetComponent<LevelController>();
        rewardedAdsButton = GameObject.Find("Menu").GetComponent<RewardedAdsButton>();
        barController = GameObject.Find("Menu").GetComponent<BarController>();
        countController = GameObject.Find("Menu").GetComponent<CountController>();
        bonusPanel = GameObject.FindGameObjectWithTag("bonus");
        show = false;
        bonusPanel.SetActive(false);

    }

    void Update(){
        timer += Time.deltaTime;
        //12000
        if (show == false && timer >= 20){
            show = true;
            rand = Random.Range(0, 10);

            if (rand >= 7){
                bonusPanel.transform.Find("Bonus").GetComponent<TextMeshProUGUI>().text = "15 minutes boost";
            } else {
                bonusPanel.transform.Find("Bonus").GetComponent<TextMeshProUGUI>().text = "10 coins";
            }
        
            bonusPanel.SetActive(true);
            timer = 0;
        }

        //3600
        if (show && timer >= 3600){
            bonusPanel.SetActive(false);
            show = false;
            timer = 0;
        }
    }

    public void Bonus()
    {
        rewardedAdsButton.ShowAd();
        if (rand >= 7){
            levelController.MH += 900*levelController.inc_MH;
            levelController.time += 900*levelController.inc_time;
            if (levelController.time >= levelController.max_time){
                levelController.time = levelController.max_time;
            }
            barController.updateBarTime(levelController.time);
        } else {
            countController.updateCountCoin(10);
        }

        bonusPanel.SetActive(false);
        show = false;
        timer = 0;
    }
}