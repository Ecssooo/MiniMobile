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

    // -------------- AJOUT --------------
    [Header("Gestion Musique")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _startClip;
    [SerializeField] private AudioClip _setupClip;
    [SerializeField] private AudioClip _battleClip;
    [SerializeField] private AudioClip _endClip;
    // ------------------------------------

    private void Start()
    {
        s_gameState = GameStates.StartScreen;
        _baseAlive = true;
        Screen.orientation = ScreenOrientation.LandscapeRight;

        // -------------- AJOUT --------------
        PlayMusicForCurrentState();
        // -----------------------------------
    }

    private void Update()
    {

        // -------------- AJOUT --------------
        PlayMusicForCurrentState();
        // -----------------------------------

        switch (s_gameState)
        {
            case GameStates.StartScreen:
                UIController.Instance.DisableAllUI();
                UIController.Instance.UpdateStartScreen(true);
                _baseAlive = true;
                break;
            case GameStates.Setup:
                UIController.Instance.DisableAllUI();
                UIController.Instance.UIUpdateMoney(_moneyController.MoneyBanq);
                UIController.Instance.UpdateShop(true);
                UIController.Instance.BlockShop(false);
                _enemyController.ResetWave();
                TowerController.Instance.ResetTower();
                break;
            case GameStates.Battle:
                UIController.Instance.BlockShop(true);
                break;
            case GameStates.End:
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

    // -------------- AJOUT --------------
    private void PlayMusicForCurrentState()
    {
        if (!_audioSource) return;

        AudioClip clipToPlay = null;
        switch (s_gameState)
        {
            case GameStates.StartScreen: clipToPlay = _startClip; break;
            case GameStates.Setup: clipToPlay = _setupClip; break;
            case GameStates.Battle: clipToPlay = _battleClip; break;
            case GameStates.End: clipToPlay = _endClip; break;
        }

        if (clipToPlay && _audioSource.clip != clipToPlay)
        {
            _audioSource.clip = clipToPlay;
            _audioSource.Play();
        }
    }
    // -----------------------------------
}
