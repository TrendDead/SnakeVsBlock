using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedSpringiness;
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;

    private SnakeInput _snakeInput;
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;

    private SpriteRenderer _sprite;

    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _snakeInput = GetComponent<SnakeInput>();
        _tailGenerator = GetComponent<TailGenerator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();

        _tail = _tailGenerator.Generate(_tailSize);
    }

    private void Start()
    {
        SizeUpdated?.Invoke(_tail.Count);   
    }
    private void OnEnable()
    {
        _head.BlocCollider += OnBlockCollided;
        _head.BonusCollected += OnBonuseCollected;
    }

    private void OnDisable()
    {
        _head.BlocCollider -= OnBlockCollided;   
        _head.BonusCollected -= OnBonuseCollected;
    }

    private void FixedUpdate()
    {
        if (_tail.Count > 0)
        {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _snakeInput.GetDirectionToClic(_head.transform.position);
        } else
        {
            SizeUpdated?.Invoke(_tail.Count);

            _sprite.sprite = null;
        }
    }

    private void Move(Vector3 nexPos)
    {
        Vector3 previousPos = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector3 tempPos = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPos, _speedSpringiness);
            previousPos = tempPos;
        }

        _head.Move(nexPos);
    }

    private void OnBlockCollided()
    {
        Segment delitedSegment = _tail[_tail.Count - 1];
        _tail.Remove(delitedSegment);
        Destroy(delitedSegment.gameObject);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonuseCollected(int bonuseSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonuseSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
