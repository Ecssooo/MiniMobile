using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _ammo;
    [SerializeField] private List<GameObject> _ennemiesList;

    [SerializeField] private float _shootDelay;
    private float t_shoot;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GameObject ennemy))
        {
            _ennemiesList.Add(ennemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GameObject ennemy))
        {
            foreach (var en in _ennemiesList)
            {
                if (ennemy == en)
                {
                    _ennemiesList.Remove(en);
                }
            }
        }
    }

    private void Update()
    {
        if (t_shoot >= _shootDelay)
        {
            Shoot();
            t_shoot = 0;
        }else
        {
            t_shoot += Time.deltaTime;
        }
        
    }
    
    void Shoot()
    {
        foreach (var ennemy in _ennemiesList)
        {
            var ammo = Instantiate(_ammo, transform);
            ammo.GetComponent<Ammo>().EnnemyAttach = ennemy;
        }
    }
}
