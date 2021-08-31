using UnityEngine;

public class IsGround : MonoBehaviour
{
    public static bool IsGrounded { get; private set; }
    
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _checker;
    [SerializeField] private float _groundDistance;
    
    private void Update()
    {
        GroundCheck();
    }
    private bool GroundCheck()
    {
        return IsGrounded = Physics.CheckSphere(_checker.transform.position, _groundDistance, _mask);
    }
}
