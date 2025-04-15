using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelMenu : MonoBehaviour
{

    TurretUpgradeManager turretUpgradeManager;
    CurrencyManager currencyManager;
    


    private int upgradeOneCost;
    private int upgradeTwoCost;

    //private float upgradeCost;
    private void Start()
    {
        turretUpgradeManager = FindObjectOfType<TurretUpgradeManager>();
        currencyManager = FindObjectOfType<CurrencyManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        checkTowerType();
    }
    public void upgradeTwo()
    {
        

        GameObject currentTower = turretUpgradeManager.selectedTower;

        if (currentTower.TryGetComponent<UpgradeTowerInterface>(out var upgradeInterface))
        {
            if (currencyManager.CurrentCurrency >= upgradeTwoCost && upgradeInterface.UpgradeTwoDone == false)
            {
                currencyManager.SpendMoney(upgradeTwoCost);
                upgradeInterface.UpgradeTwo();
            }
        }
        else if (currentTower.GetComponentInChildren<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceChild)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeTwoCost && upgradeInterfaceChild.UpgradeTwoDone == false)
            {
                currencyManager.SpendMoney(upgradeTwoCost);
                upgradeInterfaceChild.UpgradeTwo();
            }
        }
        else if (currentTower.GetComponentInParent<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceParent)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeTwoCost && upgradeInterfaceParent.UpgradeTwoDone == false)
            {
                currencyManager.SpendMoney(upgradeTwoCost);
                upgradeInterfaceParent.UpgradeTwo();
            }

        }


    }
    public void upgradeOne()
    {
      

        GameObject currentTower = turretUpgradeManager.selectedTower;

        if (currentTower.TryGetComponent<UpgradeTowerInterface>(out var upgradeInterface))
        {


            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeOneCost && upgradeInterface.UpgradeOneDone == false)
            {
                currencyManager.SpendMoney(upgradeOneCost);
                upgradeInterface.UpgradeOne();


            }

        }
        else if (currentTower.GetComponentInChildren<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceChild)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeOneCost && upgradeInterfaceChild.UpgradeOneDone == false)
            {
                currencyManager.SpendMoney(upgradeOneCost);
                upgradeInterfaceChild.UpgradeTwo();
            }
        }
        else if (currentTower.GetComponentInParent<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceParent)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeOneCost && upgradeInterfaceParent.UpgradeOneDone == false)
            {
                currencyManager.SpendMoney(upgradeOneCost);
                upgradeInterfaceParent.UpgradeTwo();
            }

        }

        }
    void checkTowerType()
    { 
    // LAZER 0 , ZT 1 , MISSLE 2 , SLOW 3
     

        switch (turretUpgradeManager.turretTypeIndicator)
        {
            case 1:

                upgradeOneCost = 150;
                upgradeTwoCost = 200;

            break;

            case 2:

                upgradeOneCost = 200;
                upgradeTwoCost = 300;

                break;
            case 3:

                upgradeOneCost = 400;
                upgradeTwoCost = 500;

                break;
            case 4:

                upgradeOneCost = 300;
                upgradeTwoCost = 600;

                break;


        }
    }

}
