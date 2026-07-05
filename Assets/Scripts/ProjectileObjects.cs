using UnityEngine;

public class ProjectileObjects : EffectObject
{
    [SerializeField] private GunPower _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifetime = 3f;

    public override void Use(GameObject user, Transform spawnPoint)
    {
        if (_projectilePrefab != null && user != null)
        {
            Vector3 spawnPosition = user.transform.position + user.transform.forward * 1.5f;
            Quaternion spawnRotation = user.transform.rotation;
            GunPower projectile = Instantiate(_projectilePrefab, spawnPosition, spawnRotation);
            projectile.Initialize(_projectileSpeed, _projectileLifetime);
            Debug.Log("Объект выстрелил");
        }
        base.Use(user, spawnPoint);
    }
}