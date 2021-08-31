using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out RaycastHit hit))
            ApplyDamage(collision.gameObject);
    }

    private void ApplyDamage(GameObject gameobject)
    {
        if(gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damageValue);
        }
    }
}
