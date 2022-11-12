using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class EnemyScipt : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _agrRange;
    [SerializeField] protected float _distanceAttack = 1.3f;
    [SerializeField] protected float _maxHealth;
    protected float _currentHealth;
    protected RaycastHit hit;
    [SerializeField] protected float _damage;
    [SerializeField] private float _reload = 1f;
    private float _time = 0;
    [SerializeField] protected Animator _animator;

    protected abstract void Move();


    protected void Start()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void Attack()
    {
        if (_time <= 0 && DistanceToPlayer() <= _distanceAttack)
        {
            _animator.SetTrigger("Attack");
            player.gameObject.GetComponent<Player>().GetDamage(_damage);
           
            _time = _reload;
        }
        else
        {
            _time -= Time.deltaTime;
        }
    }


    public void GetDamage(float damage)
    {
        _animator.SetTrigger("Hit");
        _currentHealth -= damage;
        print("curr health " + _currentHealth);
        if (_currentHealth <= 0)
        {
            
            Destroy(gameObject);

        }
    }

    

     protected int DirectionToPlayer()
     {
         if (player.transform.position.x < transform.position.x)
         {
             transform.localScale = new Vector3(-1, 1, 1);
             return -1;
         }
         if (player.transform.position.x >= transform.position.x)
         {
             transform.localScale = new Vector3(1, 1, 1);
             
         }
         return 1;
         
     }

     protected float DistanceToPlayer()
     {
         return Vector2.Distance(player.transform.position, gameObject.transform.position);
     }

     
     
     
    
    
    
    
    
    
}
