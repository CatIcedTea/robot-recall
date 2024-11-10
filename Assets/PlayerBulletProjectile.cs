using System.Collections;
using UnityEngine;

public class PlayerBulletProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _damage;

    private bool _isHit = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Count down the lifetime
        _lifetime -= Time.deltaTime;

        //Destroys the bullet if the lifetime is over
        if (_lifetime <= 0)
        {
            StartCoroutine(DestroyBullet());
        }
        //Move object straight
        if (!_isHit)
            transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
    }

    //Detect collision trigger and destroy
    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(DestroyBullet());
        //Check if the collision object is the player
        if (collision.gameObject.tag == "Enemy" && collision.TryGetComponent(out Health enemy))
        {
            Debug.Log("Enemy Hit");
            //Damage the player
            enemy.Damage((int)_damage);
        }

    }

    //Wait for trail to finish and then destroy the bullet
    private IEnumerator DestroyBullet()
    {
        GetComponent<MeshRenderer>().enabled = false;
        _isHit = true;
        yield return new WaitForSeconds(GetComponent<TrailRenderer>().time);
        Destroy(gameObject);
    }
}