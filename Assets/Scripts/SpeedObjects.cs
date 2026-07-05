using UnityEngine;

public class SpeedObjects : EffectObject
{
    [SerializeField] private float _speedIncrease = 2f;

    public override bool CanUse(GameObject user)
    {
        return user.GetComponent<Ghost>() != null;
    }

    public override void Use(GameObject user, Transform spawnPoint)
    {
        Ghost ghost = user.GetComponent<Ghost>();
        if (ghost == null)
        {
            Debug.LogError("SpeedObjects: пользователь не имеет компонента Ghost. Использование невозможно.");
            return;
        }
        ghost.IncreaseSpeed(_speedIncrease);
        base.Use(user, spawnPoint);
    }
}