using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public void Activating(GameObject obj)
    {
        obj.SetActive(true);

        if (obj.name == "UpgradeTemporaryPanel"){
            GameObject.Find("Menu").GetComponent<CountController>().InstantiateUpgradeTemporary();
        }
        
    }

     public void Deactivating(GameObject obj)
    {
        
        obj.SetActive(false);
        
    }
}