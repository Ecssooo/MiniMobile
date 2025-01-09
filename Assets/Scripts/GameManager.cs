using System;
using UnityEngine;

public enum GameStates
{
    StartScreen,
    Setup,
    Battle
}

public class GameManager : MonoBehaviour
{
    
    #region Instance

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    
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

    [SerializeField] private Money _moneyController;
    public Money MoneyController { get => _moneyController; set => _moneyController = value; }
}
