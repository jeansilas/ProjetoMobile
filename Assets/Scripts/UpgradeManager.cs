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
        Debug.Log("UpgradeManager: " + type.name);
        countController.upgradeTemporary(type);
        Destroy(gameObject);
        
    }
}