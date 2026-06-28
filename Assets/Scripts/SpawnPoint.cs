using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private float _spawnDelay = 0f;
    [SerializeField] private Vector3 _spawnRotationOffset = Vector3.zero;

    private void Awake()
    {
        EffectsObjects[] allItems = FindObjectsOfType<EffectsObjects>();
        foreach (EffectsObjects item in allItems)
        {
            Destroy(item.gameObject);
        }
    }

    private void Start()
    {
        if (_itemPrefab != null)
        {
            Invoke(nameof(Spawn), _spawnDelay);
        }
    }

    private void Spawn()
    {
        if (_itemPrefab != null)
        {
            Instantiate(_itemPrefab, transform.position, Quaternion.Euler(_spawnRotationOffset));
        }
    }
}