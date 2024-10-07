using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Hits : MonoBehaviour
{
    public float Damage;
    public float BulletRange;
    private Transform PlayerCamera;
    public Transform GunLocation;
    AudioSource ShootingSound;
    void Start()
    {
        PlayerCamera = Camera.main.transform;
        GunLocation = Camera.main.transform;
        ShootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void shoot()
    {
        Ray gunRay = new Ray(GunLocation.position,GunLocation.forward);
        ShootingSound.Play();
        if(Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange)){
            if(hitInfo.collider.gameObject.TryGetComponent(out Health enemy)){
                enemy.Damage((int) Damage);
            }
        }
    }
}
