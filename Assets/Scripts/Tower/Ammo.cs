using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Enemy _ennemyAttach;
    public Enemy EnnemyAttach { get => _ennemyAttach; set => _ennemyAttach = value; }

    private Vector3 direction;

    // -------------- AJOUT --------------
    [Header("Audio")]
    [SerializeField] private AudioSource _hitAudioSource;
    // -----------------------------------
    
    private void Start()
    {
        direction = (_ennemyAttach.transform.position - this.transform.position);
        double angle = Math.Atan2(direction.y,direction.x) * (180/Math.PI);
        
        transform.rotation = Quaternion.Euler(0,0,(float)angle);
    }

    private void Update()
    {
        Move();
        
    }

    private void Move()
    {
        Vector3 velocity = direction.normalized * _speed * Time.deltaTime;
        _rb.MovePosition(transform.position + velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy en))
        {
            en.TakeDamage(_damage);

            // -------------- AJOUT --------------
            if (_hitAudioSource)
            {
                _hitAudioSource.Play();
            }
            // -----------------------------------

            Destroy(this.gameObject);
        }
        
        if(other.CompareTag("bounds")){Destroy(this.gameObject);}
    }
}
