using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private void Start()
    {
        var health = Character.Instance.GetComponent<Health>();
        health.OnDeathEvent += CharacterDie;
    }

    private void CharacterDie()
    {
        
    }
}
