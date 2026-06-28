using UnityEngine;

public class ProjectileObjects : EffectsObjects
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifetime = 3f;

    public override void Use(Transform spawnPoint)
    {
        if (_playerTransform != null && _projectilePrefab != null)
        {
            Vector3 spawnPosition = _playerTransform.position + _playerTransform.forward * 1.5f;
            Quaternion spawnRotation = _playerTransform.rotation;
            GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, spawnRotation);
            GunPower gunPower = projectile.GetComponent<GunPower>();
            if (gunPower != null)
            {
                gunPower.Initialize(_projectileSpeed, _projectileLifetime);
            }
            Debug.Log("Объект выстрелил");
        }
        base.Use(spawnPoint);
    }
}