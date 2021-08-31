using UnityEngine;

public class CharacterLook : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _mouseSensitivity;

    private Rigidbody _playerBody;
    
    private const float _maxRotateAngleY = 90f;
    private const float _minRotateAngleY = -90f;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        
        RotateCamera(mouseX, mouseY);
    }

    private void RotateCamera(float mouseX, float mouseY)
    {
        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, _minRotateAngleY, _maxRotateAngleY);

        RotateCharacter(mouseX);
        _mainCamera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
    }
    private void RotateCharacter(float mouseX)
    {
        _playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
