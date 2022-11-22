using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class FlyingMonster : Enemy
{
   
   [SerializeField] private GameObject _missle;
   [SerializeField] private Quaternion _offset;


   private bool isMovedUp;
   
   
   private void WaitingFly()
   {
      if (isMovedUp)
      {
         MoveUp();
      }
      else
      {
         MoveDown();
      }
   }

   private async void MoveUp()
   {
      transform.DOMoveY(0.2f,3f);
      await Task.Delay(500);
      isMovedUp = false;
   }

   private async void MoveDown()
   {
      transform.DOMoveY(-0.2f,3f);
      await Task.Delay(500);
      isMovedUp = true;
   }
   
   protected override void Move()
   {
      if (DistanceToPlayer() < _agrRange)
      {
         transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(_distanceAttack , _distanceAttack), _speed * Time.deltaTime);
         if (DistanceToPlayer() <= _distanceAttack + 1 && _time <= 0)
         {
            _time = _reload;
            // RangeAttack();
            
            _animator.SetBool("IsAttacking", true);
         }
         else
         {
            _time -= Time.deltaTime;
            _animator.SetBool("IsAttacking", false);
         }

      }
      else
         WaitingFly();
   }

   //method is calling from animation
   private void RangeAttack()
   {
      Vector3 difference = player.transform.position - gameObject.transform.position;
      float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg ;
      Quaternion _attackAngle = Quaternion.Euler(0.0f, 0.0f, rotationZ);
      
      Instantiate(_missle, transform.position, _attackAngle * _offset);
      
      
   }
}
