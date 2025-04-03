using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [SerializeField]
    public int CurrentCurrency { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
            CurrentCurrency = 150;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        Debug.Log($"[MoneyManager] AddMoney CALLED! Amount: {amount}, Before Adding: {CurrentCurrency}");
        CurrentCurrency += amount;
        Debug.Log($"[MoneyManager] New CurrentMoney: {CurrentCurrency}");
    }


    public bool SpendMoney(int amount)
    {
        if (CurrentCurrency >= amount)
        {
            CurrentCurrency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RewardWaveCompletion(int waveNumber)
    {
        int waveBonus = 250;
        Debug.Log($"[WaveBonus] RewardWaveCompletion CALLED! Wave Number: {waveNumber}, Calculated Bonus: {waveBonus}");
        AddMoney(waveBonus);
        Debug.Log($"[WaveBonus] New CurrentMoney after wave bonus: {CurrentCurrency}");
    }
}
