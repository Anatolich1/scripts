
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public static Character Instance {get; private set;}

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    private void Die()
    {

    }
}
