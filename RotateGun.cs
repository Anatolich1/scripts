using UnityEngine;

public class RotateGun : MonoBehaviour
{
   [SerializeField] private GraplingGun _gun;
   private Quaternion _diseredRotation;
   private float _rotationSpeed;

   private void Update()
   {
       if (!_gun.IsGrappling())
       {
            _diseredRotation = transform.parent.rotation;
       }
       else
       {
           _diseredRotation = Quaternion.LookRotation(_gun.GetGrapplePoint() - transform.position);
       }

       transform.rotation = Quaternion.Lerp(transform.rotation , _diseredRotation, Time.deltaTime * _rotationSpeed);
   }
}
