using UnityEngine;

public class GrabAndReplaceItems : MonoBehaviour
{
    public static bool IsGrabbed { get; private set; }
    
    [SerializeField] private Transform _grabPosition;
    [SerializeField] private float _distance;
    [SerializeField] private float _grabSpeed = 10f;
    
    private RaycastHit _hit;
    private GameObject _grabbedObject;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance) && _hit.transform.TryGetComponent(out SelectedItems items))
        {
            _grabbedObject = _hit.transform.gameObject; 
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            _grabbedObject = null;
            IsGrabbed = false;
            _grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (_grabbedObject)
        {
            _grabbedObject.GetComponent<Rigidbody>().velocity = _grabSpeed * (_grabPosition.position - _grabbedObject.transform.position);
            _grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            IsGrabbed = true;
        }
    }
}
