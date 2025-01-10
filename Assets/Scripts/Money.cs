using UnityEngine;
using UnityEngine.Serialization;

public class Money : MonoBehaviour
{
    [SerializeField] private int _moneyAtStart;
    public int MoneyAtStart {get => _moneyAtStart; }
    
    [SerializeField] private int moneyBanq;
    public int MoneyBanq { get => moneyBanq; }

    public int AddMoney(int value)
    {
        if (value < 0) return 0;
        moneyBanq += value;
        UIController.Instance.UIUpdateMoney(moneyBanq);
        return moneyBanq;
    }

    public int SubMoney(int value)
    {
        if (value < 0) return 0;
        if (moneyBanq - value < 0) {
            moneyBanq = 0;
        }else {
            moneyBanq -= value;
        }
        UIController.Instance.UIUpdateMoney(moneyBanq);
        return moneyBanq;
    }
}
