using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanelMenu : MonoBehaviour
{

    TurretUpgradeManager turretUpgradeManager;
    CurrencyManager currencyManager;

    public TextMeshProUGUI costOneText;
    public TextMeshProUGUI costTwoText;

    public TextMeshProUGUI UpgradeTypeOneText;
    public TextMeshProUGUI UpgradeTypeTwoText;

    public GameObject moneyWarning;


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
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

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
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

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
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

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
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

            }

        }
        else if (currentTower.GetComponentInChildren<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceChild)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeOneCost && upgradeInterfaceChild.UpgradeOneDone == false)
            {
                currencyManager.SpendMoney(upgradeOneCost);
                upgradeInterfaceChild.UpgradeOne();
            }
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

            }
        }
        else if (currentTower.GetComponentInParent<UpgradeTowerInterface>() is UpgradeTowerInterface upgradeInterfaceParent)
        {
            Debug.Log("1 Hello");
            if (currencyManager.CurrentCurrency >= upgradeOneCost && upgradeInterfaceParent.UpgradeOneDone == false)
            {
                currencyManager.SpendMoney(upgradeOneCost);
                upgradeInterfaceParent.UpgradeOne();
            }
            else
            {
                moneyWarning.SetActive(true);
                StartCoroutine(moneyWarn());

            }

        }
        

    }
    IEnumerator moneyWarn()
    {

        yield return new WaitForSeconds(0.7f);
        moneyWarning.SetActive(false);
    }

void checkTowerType()
    { 
    // LAZER 0 , ZT 1 , MISSLE 2 , SLOW 3
     

        switch (turretUpgradeManager.turretTypeIndicator)
        {
            case 0:

                upgradeOneCost = 150;
                upgradeTwoCost = 200;

                costOneText.text = upgradeOneCost.ToString();
                costTwoText.text = upgradeTwoCost.ToString();

                UpgradeTypeOneText.text = "";
                UpgradeTypeTwoText.text = "";


                break;

            case 1:

                upgradeOneCost = 200;
                upgradeTwoCost = 300;

                costOneText.text = upgradeOneCost.ToString();
                costTwoText.text = upgradeTwoCost.ToString();

                UpgradeTypeOneText.text = "Faster";
                UpgradeTypeTwoText.text = "Damage";


                break;
            case 2:

                upgradeOneCost = 400;
                upgradeTwoCost = 500;

                costOneText.text = upgradeOneCost.ToString();
                costTwoText.text = upgradeTwoCost.ToString();

                UpgradeTypeOneText.text = "Fire Speed";
                UpgradeTypeTwoText.text = "Damage";

                break;
            case 3:

                upgradeOneCost = 300;
                upgradeTwoCost = 600;

                costOneText.text = upgradeOneCost.ToString();
                costTwoText.text = upgradeTwoCost.ToString();

                UpgradeTypeOneText.text = "Range";
                UpgradeTypeTwoText.text = "Slowness";


                break;


        }
    }

}
