using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMonster : EnemyScipt
{
    
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
            
        }

        if (DistanceToPlayer()>=_distanceAttack)
        {
            _animator.SetBool("Idling", true);
        }
        else
        {
            _animator.SetBool("Idling", false);
        }
    }
    
    
    
}
