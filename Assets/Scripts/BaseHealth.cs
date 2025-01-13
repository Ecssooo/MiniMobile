using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int baseHP = 100;
    [SerializeField] private Transform healthBar;
    private int currentHP;
    public void Start()
    {
        currentHP = baseHP;
        float ratio = (float)currentHP / baseHP;
        if (healthBar != null)
        {
            healthBar.localScale = new Vector3(Mathf.Clamp01(ratio), healthBar.localScale.y, healthBar.localScale.z);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHP >= 0)
            currentHP -= damage;
        float ratio = Mathf.Clamp01((float)currentHP / baseHP);
        if (healthBar != null)
        {
            healthBar.localScale = new Vector3(ratio, healthBar.localScale.y, healthBar.localScale.z);
        }
        if (currentHP <= 0)
        {
            GameManager.Instance.BaseAlive = false;
            GameManager.Instance.EndState();
        }
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameStates.StartScreen)
        {
            currentHP = baseHP;
        }
    }

}
