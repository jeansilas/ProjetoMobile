using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
    }

    
    public void updateCountTime(int discreetValue)
    {
        foreach (TextMeshProUGUI count in CountTime)
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

    public void updateCountMentalHealth(int discreetValue)
    {
        foreach (TextMeshProUGUI count in CountMentalHealth)
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
