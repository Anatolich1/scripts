using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public event Action OnHealthChangedEvent;
    public event Action OnDeathEvent;

    private int _currenthealth;
    public int MaxHealth => _maxHealth;  

    public int CurrentHealth
    {
        get => _currenthealth;

        private set
        {
            if (value > MaxHealth)
                value = MaxHealth;

            else if (value <= 0)
                value = 0;
            
            _currenthealth = value;

            if(value == 0)
                OnDeathEvent?.Invoke();
        }
    }
    
    public void TakeDamage(int damage)
    {
        _currenthealth -= damage;
    }

    private void ResetHealth()
    {
        _currenthealth = _maxHealth;
    }
}
