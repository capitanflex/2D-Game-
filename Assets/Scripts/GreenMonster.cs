using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GreenMonster : EnemyScipt
{
    private GameObject _playerScript;

    protected new void Start()
    {

        _currentHealth = _maxHealth;
    }

    void Update()
    {
        Move();
        
    }

    protected override void Move()
    {
        if (DistanceToPlayer() < _agrRange)
        {
            Attack();
            _animator.SetBool("IsRunning", true);
            
            if (DistanceToPlayer() >= _distanceAttack)
            {
                transform.position += Vector3.right * DirectionToPlayer() * _speed * Time.deltaTime;
            }
            
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }
    
    
    

   

    
    
    
}
