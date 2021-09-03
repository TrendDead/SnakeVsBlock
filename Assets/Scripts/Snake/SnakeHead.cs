using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeHead : MonoBehaviour
{
    private Snake _snake;

    private Rigidbody2D _rb;

    public event UnityAction BlocCollider;
    public event UnityAction<int> BonusCollected;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 newPos)
    {
        _rb.MovePosition(newPos);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
        {
            BlocCollider?.Invoke();
            block.Fill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bonus bonus))
        {
            BonusCollected?.Invoke(bonus.Collect());
        }
    }

    private void Awake()
    {
        _snake = GetComponentInParent<Snake>();
    }
    
    private void OnEnable()
    {
        _snake.SizeUpdated += SnakeDeath;
    }

    private void OnDisable()
    {
        _snake.SizeUpdated += SnakeDeath;
    }

    private void SnakeDeath(int size)
    {
        if (size <= 0)
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
