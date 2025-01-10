using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int baseHP = 100;

    public void TakeDamage(int damage)
    {
        baseHP -= damage;
        if (baseHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
