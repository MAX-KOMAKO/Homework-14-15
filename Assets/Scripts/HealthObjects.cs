using UnityEngine;

public class HealthObjects : EffectsObjects
{
    [SerializeField] private float _healthAmount = 20f;
    private Health _cachedHealth;

    public override void PickUp(Transform holdPoint)
    {
        base.PickUp(holdPoint);
        _cachedHealth = _playerTransform?.GetComponent<Health>();
    }

    public override void Use(Transform spawnPoint)
    {
        if (_cachedHealth != null)
        {
            _cachedHealth.AddHealth(_healthAmount);
        }
        base.Use(spawnPoint);
    }
}