
public class WormMonster : Enemy
{
    
    

    
    
    
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
