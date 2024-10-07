using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;

    //Instantiate the bullet at the bullet spawn point and rotated to the player
    public void FireGun(){
        GameObject _firedProjectile = Instantiate(_projectile);
        _firedProjectile.transform.position = _spawnPoint.position;
        _firedProjectile.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    //Return the bullet spawn point
    public Transform GetSpawnPoint(){
        return _spawnPoint.transform;
    }
}
