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
    }

    
    public void updateBarTime(int fillValue)
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
