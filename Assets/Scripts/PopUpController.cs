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

        if (obj.name == "UpgradePermanentPanel"){
            GameObject.Find("Menu").GetComponent<CountController>().InstantiateUpgradePermanent();
        }
        
    }

     public void Deactivating(GameObject obj)
    {
        
        obj.SetActive(false);
        
    }
}