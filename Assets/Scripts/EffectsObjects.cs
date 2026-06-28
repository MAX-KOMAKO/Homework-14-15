using UnityEngine;

public abstract class EffectsObjects : MonoBehaviour
{
    [SerializeField] protected GameObject _useParticlePrefab;
    [SerializeField] private float _idleRotationSpeed = 30f;
    protected Transform _playerTransform;

    private void Update()
    {
        if (transform.parent == null)
        {
            transform.Rotate(Vector3.up, _idleRotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public virtual void PickUp(Transform holdPoint)
    {
        _playerTransform = holdPoint.parent;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;
    }

    public virtual void Use(Transform spawnPoint)
    {
        Debug.Log("Объект использован");
        DestroySelfWithEffect(spawnPoint);
    }

    protected void DestroySelfWithEffect(Transform spawnPoint)
    {
        if (_useParticlePrefab != null && spawnPoint != null)
        {
            GameObject particleInstance = Instantiate(_useParticlePrefab, spawnPoint.position, spawnPoint.rotation);
            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                float duration = ps.main.duration;
                Destroy(particleInstance, duration + 0.5f);
            }
            else
            {
                Destroy(particleInstance, 2f);
            }
        }
        Destroy(gameObject);
    }
}