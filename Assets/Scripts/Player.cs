using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    [SerializeField] protected float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private float _reloadTime;
    private float _timer;
    [SerializeField] private float _attackDistance = 3f;
    [SerializeField] private Transform _point;
    private Transform _transform;
    private LayerMask _ground;
    [SerializeField] private LayerMask _enemyMask;
    private bool _isGrounded;
    private float _direction;
    [SerializeField] private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private HealthBar helthBar;
    


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = gameObject.transform;
        helthBar.SetMaxHealth(_hp);
        
    }

    private void Update()
    {
        Grounded();
        Move();
        Attack();
    }


    private void Move()
    {
        

        if (Input.GetKeyDown("space") && _isGrounded)
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce*10f), ForceMode2D.Force);
            
            
        }
        float direction = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * direction * _speed * Time.deltaTime;
        
        
        if (direction < -.1f)
        {
            _transform.localScale =  new Vector3(-1, 1, 1);
           
        }
        if (direction > .1f)
        {
            _transform.localScale =  new Vector3(1, 1, 1);
            
        }

        if (direction != 0 && _isGrounded)
        {
            _animator.SetFloat("Speed", 1f);
        }

        if (!_isGrounded)
        {
            _animator.SetBool("IsJumping", true);
        }
        else
        {
            _animator.SetBool("IsJumping", false);
        }

        if (direction == 0)
        {
            _animator.SetFloat("Speed", 0);
        }

        
    }



    private void Grounded()
    {
        RaycastHit2D RayHit = Physics2D.Raycast(_point.position, Vector2.right, _ground);
        if (RayHit.collider != null)
        {
            _isGrounded = true;
            Debug.DrawRay(_point.position, Vector2.right, Color.green);
            
            
        }
        else
        {
            
            _isGrounded = false;
            Debug.DrawRay(_point.position, Vector2.right, Color.red);

            
        }
        
    }

    public void GetDamage(float damage)
    {
        _hp -= damage;
        helthBar.SetHealth(_hp);
        print(_hp);
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0 ) && _timer<=0)
        {
    
            StartCoroutine(attackAnimation());
            try
            {
                CheckEnemy().GetComponent<EnemyScipt>().GetDamage(_damage);
            }
            catch (NullReferenceException)
            {
                Debug.Log("have't enemy");
            }

            _timer = _reloadTime;

        }
        else
            _timer -= Time.deltaTime;
        
        
        
      
    }
    
    private GameObject CheckEnemy()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider2D.bounds.center + transform.right * _attackDistance * transform.localScale.x,
            _boxCollider2D.bounds.size * new Vector2(_rangeAttack,1), 0, Vector2.right*_direction, 0, _enemyMask);
        
        
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                return hit.collider.gameObject;
            }
        

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center + transform.right * _attackDistance * transform.localScale.x, _boxCollider2D.bounds.size * new Vector2(_rangeAttack,1));
    }

    IEnumerator attackAnimation()
    {
        _animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.3f);
        _animator.SetBool("IsAttacking", false);
    }



}    



    

  