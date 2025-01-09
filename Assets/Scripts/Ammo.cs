using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;

    private GameObject _ennemyAttach;
    public GameObject EnnemyAttach { get => _ennemyAttach; set => _ennemyAttach = value; }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        Vector3 direction = (_ennemyAttach.transform.position - transform.position).normalized;
        Vector3 velocity = direction * _speed * Time.deltaTime;
        _rb.MovePosition(transform.position + velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Transform en))
        {
            if (en == _ennemyAttach.transform)
            {
                Destroy(this);
            }
        }
    }
}
