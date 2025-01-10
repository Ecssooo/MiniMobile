using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Instance

    private static TowerController _instance;
    public static TowerController Instance { get => _instance; }
    
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

    [SerializeField] private List<TowerAttack> _towerList = new List<TowerAttack>();
    public List<TowerAttack> TowerList { get => _towerList; }

    public void ResetTower()
    {
        foreach(var tower in _towerList)
        {
            for (int j = 0; j < tower.AmmoList.Count; j++)
            {
                if(tower.AmmoList[j] != null) Destroy(tower.AmmoList[j].transform.parent.gameObject);
            }
            tower.EnemiesList.Clear();
            tower.AmmoList.Clear();
        }
    }

    public void DeleteAllTower()
    {
        for (int j = 0; j < _towerList.Count; j++)
        {
            if(_towerList[j] != null) Destroy(_towerList[j].transform.parent);
        }
        _towerList.Clear();
    }
}
