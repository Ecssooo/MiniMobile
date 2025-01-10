using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Enemy _ennemyAttach;
    public Enemy EnnemyAttach { get => _ennemyAttach; set => _ennemyAttach = value; }

    private Vector2 direction;
    
    private void Start()
    {
        direction = (_ennemyAttach.transform.position - this.transform.position).normalized;
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        Vector3 velocity = direction * _speed * Time.deltaTime;
        _rb.MovePosition(transform.position + velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy en))
        {
            if (en == _ennemyAttach)
            {
                en.TakeDamage(_damage);
                Destroy(this.gameObject);
            }
        }
    }
}
