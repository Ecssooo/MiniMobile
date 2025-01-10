using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int baseHP = 100;

    public void TakeDamage(int damage)
    {
        baseHP -= damage;
        if (baseHP <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.EndState();
            GameManager.Instance.BaseAlive = false;
            
        }
    }
}
