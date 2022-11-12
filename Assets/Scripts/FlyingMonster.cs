using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingMonster : EnemyScipt
{
   private Vector2 StartPosition;
   private float StayFlyRange = 0.1f;
   [SerializeField] private float WaitingFlySpeed = 1.5f;

   

   private new void Start()
   {
      StartPosition = transform.position;
   }

   private void Update()
   {
      Move();
   }

   private void WaitingFly()
   {
     
   }
   
   protected override void Move()
   {
      WaitingFly();
   }
}
