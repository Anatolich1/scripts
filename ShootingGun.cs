using System.Collections;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _laser;

    [SerializeField] private float _timeToDeleteEffect;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _range;

    private Animator _animator;
    private AudioSource _shootSound;

    private const int _maxAmmo = 30;
    private const string _reloading = "Reloading";

    private float _nextTimeToFire = 0f;
    private int _currentAmmo;
    private bool _isReloading;

    private void Start()
    {
        _shootSound = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();            

        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if(_isReloading) return;

        if(GetInput() && FireRate())
        {
            Shoot();
            _shootSound.Play(); 
            _muzzleFlash.Play(); // starts shootEffect
        }
    }

    private bool GetInput()
    {
        if (Input.GetButton("Fire1"))
        {
            return true;
        }
        else 
            return false;
    }

    private void Shoot()
    {
        //gets the current ammo and start reloading;
        if (GetCurrentAmmo() == 0)
        {
            StartCoroutine(Reload());
            return;
        }

        RaycastHit hit;
        var transform1 = _mainCamera.transform;

        if (Physics.Raycast(transform1.position, transform1.forward, out hit, _range))
        {
            //spawn hit effect on objects;
            GameObject laser = Instantiate(_laser, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(laser, _timeToDeleteEffect);
        }
    }

    IEnumerator Reload()
    {
        _isReloading = true; 

        _animator.SetBool(_reloading, true);
        yield return new WaitForSeconds(_reloadTime);
        _animator.SetBool(_reloading, false);

        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }

    private bool FireRate()
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f/_fireRate;
            return true;
        }
        
        return false;
    }

    private int GetCurrentAmmo()
    {
        return _currentAmmo--;
    }
}
