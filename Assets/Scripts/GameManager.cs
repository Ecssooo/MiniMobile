using System;
using UnityEngine;

public enum GameStates
{
    StartScreen,
    Setup,
    Battle,
    End
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
    [SerializeField] private EnemyManager _enemyController;

    private bool _baseAlive;

    public bool BaseAlive { get => _baseAlive; set => _baseAlive = value; }

    private GameStates s_gameState;

    public GameStates GameState => s_gameState;

    private void Start()
    {
        s_gameState = GameStates.StartScreen;
        _baseAlive = true;
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    private void Update()
    {
        Debug.Log(s_gameState);
        switch (s_gameState)
        {
            case(GameStates.StartScreen):
                UIController.Instance.DisableAllUI();
                UIController.Instance.UpdateStartScreen(true);
                _moneyController.AddMoney(_moneyController.MoneyAtStart);
                break;
            case(GameStates.Setup):
                UIController.Instance.DisableAllUI();
                UIController.Instance.UpdateShop(true);
                UIController.Instance.BlockShop(false);
                _enemyController.ResetWave();
                TowerController.Instance.ResetTower();
                break;
            case(GameStates.Battle):
                UIController.Instance.BlockShop(true);
                break;
            case(GameStates.End):
                break;
        }
    }

    public void SetupState() { s_gameState = GameStates.Setup; }
    public void StartState() { s_gameState = GameStates.StartScreen; }
    public void BattleState() { s_gameState = GameStates.Battle; }

    public void EndState()
    {
        s_gameState = GameStates.End;
        _enemyController.ResetWave();
        TowerController.Instance.ResetTower();
        TowerController.Instance.DeleteAllTower();
        if (!_baseAlive)
        {
            UIController.Instance.DisableAllUI();
            UIController.Instance.UpdateDefeatScreen(true);
        }
        else
        {
            UIController.Instance.DisableAllUI();
            UIController.Instance.UpdateWinScreen(true);
        }
    }
}
