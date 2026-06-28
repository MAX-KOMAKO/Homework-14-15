using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        Debug.Log($"Жизни: {_currentHealth}");
    }

    public void AddHealth(float amount)
    {
        _currentHealth += amount;
        Debug.Log($"Жизни: {_currentHealth}");
    }

    public float GetCurrentHealth() => _currentHealth;
}