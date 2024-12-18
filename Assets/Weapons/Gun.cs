using UnityEngine.Events;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int gunType;
    public UnityEvent OnGunShoot;
    public float FireRate;
    //Default is semi automatic
    public bool Automatic;
    private float CurrentFirerate;
    void Start()
    {
        CurrentFirerate = FireRate;
    }

    void Update(){
        if(Automatic){
            if(Input.GetMouseButton(0)){
                if(CurrentFirerate <= 0f){
                    OnGunShoot?.Invoke();
                    CurrentFirerate = FireRate;
                }
            }
        }
        else{
            if(Input.GetMouseButtonDown(0)){
                OnGunShoot?.Invoke();
            }
        }
        if(CurrentFirerate > 0){
            CurrentFirerate -= Time.deltaTime;
        }
    }
}
