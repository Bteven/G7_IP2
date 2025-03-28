using UnityEngine;
using UnityEngine.UI;

public class UpgradesPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradesPanel; //Reference to the upgrades panel GameObject

    [SerializeField] private int upgradeCost = 100; //The cost required for an upgrade
    [SerializeField] private Button button; //Reference to the UI button that will trigger the upgrade

    //Applies the upgrade by spending currency.
    // Checks if the player has enough money using the CurrencyManager.
    public void ApplyUpgrade()
    {
        if (CurrencyManager.Instance.SpendMoney(upgradeCost))
        {
            Debug.Log("Upgrade is successful!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void ToggleUpgrade()
    {
        upgradesPanel.SetActive(!upgradesPanel.activeSelf);
    }
}