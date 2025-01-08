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
    
    [SerializeField] private int _money;
    public int Money { get => _money; }

    public void AddMoney(int value)
    {
        #if UNITY_EDITOR
        if (value < 0) throw new ArgumentException("Value must be positive", "value"); 
        #endif
        if (value < 0) return;
        _money += value;
        UIController.Instance.UIUpdateMoney(_money);
    }

    public void SubMoney(int value)
    {
        #if UNITY_EDITOR
        if (value < 0) throw new ArgumentException("Value must be positive", "value"); 
        #endif
        if (value < 0) return;
        if (_money - value < 0) {
            _money = 0;
        }else {
            _money -= value;
        }
        UIController.Instance.UIUpdateMoney(_money);
    }
}
