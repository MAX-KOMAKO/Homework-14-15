using UnityEngine;

public class GunPower : MonoBehaviour
{
    private float _speed;
    private float _lifetime;
    private float _lifeTimer;

    public void Initialize(float speed, float lifetime)
    {
        _speed = speed;
        _lifetime = lifetime;
        _lifeTimer = 0f;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= _lifetime)
        {
            Destroy(gameObject);
        }
    }
}