using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GraplingGun : MonoBehaviour
{
    [SerializeField] private Transform _gunTip;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _whatIsGrapple;
    [SerializeField] private float _maxGrappleDistance;

    private LineRenderer _lineRender;
    private Vector3 _grapplePoint;
    private SpringJoint _joint;
    private Transform _camera;

    private void Start()
    {
        _lineRender = GetComponent<LineRenderer>();
        _camera = Camera.main.transform;
    }

    private void Update() 
    {
        DrawRope(); 

        if (Input.GetMouseButtonDown(0)) //сделать паттерн State
        {
            
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(_camera.position, _camera.forward, out hit ,_maxGrappleDistance, _whatIsGrapple))
        {
            _grapplePoint = hit.point;
            _joint = _player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _grapplePoint;

            float distanceFromPoint = Vector3.Distance(_player.position, _grapplePoint);

            _joint.maxDistance = distanceFromPoint * 0.8f; 
            _joint.minDistance = distanceFromPoint * 0.25f; 

            _joint.spring = 4.5f;
            _joint.damper = 7f; 
            _joint.massScale = 4.5f; 

            _lineRender.positionCount = 2;
        }
    }

    private void StopGrapple()
    {
        _lineRender.positionCount = 0;
        Destroy(_joint);
    }

    private void DrawRope()
    {
        if (!_joint) return;

        _lineRender.SetPosition(0, _gunTip.position);
        _lineRender.SetPosition(1 , _grapplePoint);
    }

    public bool IsGrappling()
    {
        return _joint != null; 
    }

    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }
}
