using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfflineController : MonoBehaviour
{
    public GameObject offlinePanel;
    public TextMeshProUGUI timeAwayText;
    public TextMeshProUGUI MHgains;
    public TextMeshProUGUI timeGains;

    public LevelController levelController;
    private float MHgainsOffline;
    private float timeGainsOffline;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("Menu").GetComponent<LevelController>();
        offlinePanel = GameObject.FindGameObjectWithTag("offline");
        timeAwayText = offlinePanel.transform.Find("timeAwayText")?.GetComponent<TextMeshProUGUI>();
        MHgains = offlinePanel.transform.Find("MHGains")?.GetComponent<TextMeshProUGUI>();
        timeGains = offlinePanel.transform.Find("timeGains")?.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOfflineProduction()
    {
        if (levelController.offlineProgress){
            offlinePanel.SetActive(true);
            var tempOff = Convert.ToInt64(PlayerPrefs.GetString("offlineTime"));
            var oldTime = DateTime.FromBinary(tempOff);
            var currentTime = DateTime.Now;

            var timeAway = currentTime.Subtract(oldTime);
            var timeAwayInSeconds = (float)timeAway.TotalSeconds;

            var offlineTime = timeAwayInSeconds / 10;

    	    TimeSpan timeSpan = TimeSpan.FromSeconds(offlineTime);
            timeAwayText.text = "You were away for " + timeSpan.ToString(@"hh\:mm\:ss") + " hours";

            MHgainsOffline = offlineTime * levelController.inc_MH;
            timeGainsOffline = offlineTime * levelController.inc_time;
            
            if (MHgainsOffline >= 1000){
                var exponent = Math.Floor(Math.Log10(Math.Abs(MHgainsOffline)));
                var rounded = Math.Round((MHgainsOffline) / Math.Pow(10, exponent), 2);
                MHgains.text = "You gained " + rounded.ToString("F2") + "e+" + exponent.ToString() + " Mental Health";
            } else {
                MHgains.text = "You gained " + MHgainsOffline.ToString("F0") + " Mental Health";
            }

            if (timeGainsOffline >= levelController.max_time){
                timeGains.text = "You maxed out your time";
            } else {
                timeGains.text = "You gained " + timeGainsOffline.ToString("F0") + " time";
            }

            PlayerPrefs.SetString("offlineTime", DateTime.Now.ToBinary().ToString());

        }
    }

    public void CloseOfflinePanel()
    {
        levelController.MH += MHgainsOffline;

        if(levelController.time > levelController.max_time)
        {
            levelController.time = levelController.max_time;
        } else{
            levelController.time += timeGainsOffline;
        }

        offlinePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void DoubleGains(){
        levelController.MH += MHgainsOffline*2;

        if(levelController.time > levelController.max_time)
        {
            levelController.time = levelController.max_time;
        } else{
            levelController.time += timeGainsOffline+2;
        }

        offlinePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
