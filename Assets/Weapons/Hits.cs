using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Hits : MonoBehaviour
{
    public float Damage;

    public float BulletRange;
    private float hitDist = 300.0f;
    private Transform PlayerCamera;
    public Transform GunLocation;
    AudioSource ShootingSound;

    [SerializeField] private GameObject wholeGun;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform directionPoint;

    void Start()
    {
        PlayerCamera = Camera.main.transform;
        GunLocation = Camera.main.transform;
        ShootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void shoot()
    {
        GameObject _firedProjectile = Instantiate(_projectile);
        _firedProjectile.transform.position = _spawnPoint.position;
        _firedProjectile.transform.LookAt(directionPoint.position);
        //wholeGun.transform.LookAt(Camera.main.transform);
        Ray gunRay = new Ray(GunLocation.position,GunLocation.forward);
        ShootingSound.Play();
        //if(Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange)){
            //if(hitInfo.collider.gameObject.TryGetComponent(out Health enemy)){
                //enemy.Damage((int) Damage);
            //}
        //}
    }
}
