using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update

    private CountController countController;
    void Start()
    {
        countController = GameObject.Find("Menu").GetComponent<CountController>();
        Debug.Log("UpgradeManager start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(GameObject type)
    {
        if (type.name == "UpgradePermanent(Clone)")
        {
            countController.upgradePermanent(type);
        } else{
            countController.upgradeTemporary(type);
        }
        
        Destroy(gameObject);
        
    }
}