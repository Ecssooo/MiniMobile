using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Instance

    private static UIController _instance;
    public static UIController Instance { get => _instance; }
    
    public virtual void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    #endregion
    
    
    [SerializeField] private TextMeshProUGUI _moneyTXT;


    private void Start()
    {
        SetupAllUI();
    }

    public void UIUpdateMoney(int value)
    {
        _moneyTXT.text = value.ToString();
    }
    
    private void SetupAllUI()
    {
        UIUpdateMoney(GameManager.Instance.MoneyController.MoneyBanq);
    }
}
