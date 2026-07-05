using UnityEngine;

public class HealthObjects : EffectObject
{
    [SerializeField] private float _healthAmount = 20f;

    public override bool CanUse(GameObject user)
    {
        return user.GetComponent<Health>() != null;
    }

    public override void Use(GameObject user, Transform spawnPoint)
    {
        Health health = user.GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("HealthObjects: пользователь не имеет компонента Health. Использование невозможно.");
            return;
        }
        health.AddHealth(_healthAmount);
        base.Use(user, spawnPoint);
    }
}