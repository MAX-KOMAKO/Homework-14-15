using UnityEngine;

public class SpeedObjects : EffectsObjects
{
    [SerializeField] private float _speedIncrease = 2f;
    private Ghost _cachedGhost;

    public override void PickUp(Transform holdPoint)
    {
        base.PickUp(holdPoint);
        _cachedGhost = _playerTransform?.GetComponent<Ghost>();
    }

    public override void Use(Transform spawnPoint)
    {
        if (_cachedGhost != null)
        {
            _cachedGhost.IncreaseSpeed(_speedIncrease);
        }
        base.Use(spawnPoint);
    }
}