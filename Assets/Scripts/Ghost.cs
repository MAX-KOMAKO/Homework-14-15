using UnityEngine;

public class Ghost : MonoBehaviour
{
    private const KeyCode USE_KEY = KeyCode.F;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Transform _holdPoint;

    private EffectObject _currentItem;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (_health == null)
        {
            Debug.LogError("Компонент Health отсутствует на объекте Ghost");
        }
        if (_holdPoint == null)
        {
            Debug.LogError("HoldPoint не назначен в инспекторе");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleItemUse();
    }

    private Vector3 GetInputDirection()
    {
        float horizontal = -Input.GetAxisRaw("Horizontal");
        float vertical = -Input.GetAxisRaw("Vertical");
        return new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void HandleMovement()
    {
        Vector3 direction = GetInputDirection();
        if (direction.magnitude > 0.1f)
        {
            transform.Translate(direction * _moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void HandleRotation()
    {
        Vector3 direction = GetInputDirection();
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleItemUse()
    {
        if (Input.GetKeyDown(USE_KEY))
        {
            if (_currentItem != null)
            {
                _currentItem.Use(gameObject, _holdPoint);
                _currentItem = null;
            }
            else
            {
                Debug.Log("Нет предмета для использования");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EffectObject item = other.GetComponent<EffectObject>();
        if (item != null && item.CanUse(gameObject))
        {
            PickUpItem(item);
        }
    }

    public void PickUpItem(EffectObject item)
    {
        if (_currentItem == null)
        {
            _currentItem = item;
            item.PickUp(_holdPoint != null ? _holdPoint : transform);
        }
        else
        {
            Debug.Log("У персонажа уже есть предмет");
        }
    }

    public void IncreaseSpeed(float amount)
    {
        _moveSpeed += amount;
        Debug.Log($"Скорость увеличена: {_moveSpeed}");
    }
}