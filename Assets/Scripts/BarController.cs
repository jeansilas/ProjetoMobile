using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarController : MonoBehaviour
{
    private string tagTime = "BarTime";
    private string tagStudy = "BarStudy";
    private string tagProject = "BarProject";
    private string tagTest = "BarTest";

    private Slider[] barTime;
    private Slider[] barStudy;
    private Slider[] barProject;
    private Slider[] barTest;

    private LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        barTime = GameObject.FindGameObjectsWithTag(tagTime)
            .Select(go => go.GetComponent<Slider>())
            .ToArray();
        
        barStudy = GameObject.FindGameObjectsWithTag(tagStudy)
            .Select(go => go.GetComponent<Slider>())
            .ToArray();

        barProject = GameObject.FindGameObjectsWithTag(tagProject)
            .Select(go => go.GetComponent<Slider>())
            .ToArray();
        
        barTest = GameObject.FindGameObjectsWithTag(tagTest)
            .Select(go => go.GetComponent<Slider>())
            .ToArray();

        levelController = GameObject.Find("Menu").GetComponent<LevelController>();

        Debug.Log(levelController.max_time);
        
        foreach (Slider slider in barTime)
        {
            slider.maxValue = levelController.max_time;
            slider.value = levelController.time;
        }

        foreach (Slider slider in barStudy)
        {
            slider.maxValue = levelController.max_study;
            slider.value = levelController.study;
        }

        foreach (Slider slider in barProject)
        {
            slider.maxValue = levelController.max_project;
            slider.value = levelController.project;
        }

        foreach (Slider slider in barTest)
        {
            slider.maxValue = levelController.max_test;
            slider.value = levelController.test;
        }

    }

    
    public void updateBarTime(float fillValue)
    {
        foreach (Slider slider in barTime)
        {
            slider.value += fillValue;
        }
    }

    public void updateBarStudy(int fillValue)
    {
        foreach (Slider slider in barStudy)
        {
            slider.value += fillValue;
        }
    }

    public void updateBarProject(int fillValue)
    {
        foreach (Slider slider in barProject)
        {
            slider.value += fillValue;
        }
    }

    public void updateBarTest(int fillValue)
    {
        foreach (Slider slider in barTest)
        {
            slider.value += fillValue;
        }
    }
}