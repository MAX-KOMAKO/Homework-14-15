using UnityEngine;

public class Ghost : MonoBehaviour
{
    private const KeyCode USE_KEY = KeyCode.F;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    private EffectsObjects _currentItem;
    private Health _health;
    private Transform _holdPoint;

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (_health == null)
        {
            Debug.LogError("Компонент Health отсутствует на объекте Ghost");
        }

        _holdPoint = transform.Find("HoldPoint");
        if (_holdPoint == null)
        {
            Debug.LogError("Дочерний объект 'HoldPoint' не найден. Создайте пустой дочерний объект с именем 'HoldPoint'.");
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
                _currentItem.Use(_holdPoint);
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
        EffectsObjects item = other.GetComponent<EffectsObjects>();
        if (item != null)
        {
            PickUpItem(item);
        }
    }

    public void PickUpItem(EffectsObjects item)
    {
        if (_currentItem == null)
        {
            _currentItem = item;
            if (_holdPoint != null)
                item.PickUp(_holdPoint);
            else
                item.PickUp(transform);
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