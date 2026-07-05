using UnityEngine;

public abstract class EffectObject : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _useParticleEffect;
    [SerializeField] private float _idleRotationSpeed = 30f;

    private Collider _collider;
    private Transform _playerTransform;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError($"EffectObject {name} не имеет Collider. Это может привести к ошибкам.");
        }
    }

    private void Update()
    {
        if (transform.parent == null)
        {
            transform.Rotate(Vector3.up, _idleRotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public virtual bool CanUse(GameObject user)
    {
        return true;
    }

    public virtual void PickUp(Transform holdPoint)
    {
        _playerTransform = holdPoint.parent;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (_collider != null)
            _collider.enabled = false;
    }

    public virtual void Use(GameObject user, Transform spawnPoint)
    {
        Debug.Log("Объект использован");
        DestroySelfWithEffect(spawnPoint);
    }

    protected void DestroySelfWithEffect(Transform spawnPoint)
    {
        if (_useParticleEffect != null && spawnPoint != null)
        {
            ParticleSystem particleInstance = Instantiate(_useParticleEffect, spawnPoint.position, spawnPoint.rotation);
            particleInstance.Play();
            float duration = particleInstance.main.duration;
            Destroy(particleInstance.gameObject, duration + 0.5f);
        }
        Destroy(gameObject);
    }
}