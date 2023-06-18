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
            
            if(GameObject.Find("Menu").GetComponent<CountController>().int_temp == false){
                GameObject.Find("Menu").GetComponent<CountController>().InstantiateUpgradeTemporary();
                GameObject.Find("Menu").GetComponent<CountController>().int_temp = true;
            }
        }

        Debug.Log(obj.name);
        if (obj.name == "UpgradePermanentPanel"){
            if(GameObject.Find("Menu").GetComponent<CountController>().int_perm == false){
                GameObject.Find("Menu").GetComponent<CountController>().InstantiateUpgradePermanent();
                GameObject.Find("Menu").GetComponent<CountController>().int_perm = true;
            }
        }
        
    }

     public void Deactivating(GameObject obj)
    {
        
        obj.SetActive(false);
        
    }
}