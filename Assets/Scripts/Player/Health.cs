using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHp = 100;
    private int _hp;
    private bool deadStatus = false;
    public bool enemyStatus = true;

    public int MaxHp => _maxHp;
    public int Hp
    {
        get => _hp;
        private set
        {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, min: 0, _maxHp);
            if (isDamage)
            {
                Damaged?.Invoke(_hp);
            }
            else
            {
                Healed?.Invoke(_hp);
            }
            if(_hp <= 0)
            {
                if(enemyStatus == true && deadStatus == false)
                {
                    deadStatus = true;
                    ScoreScript.scoreValue += 10;
                }
                else if(enemyStatus == false)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                Died?.Invoke(0);

            }
        }
    }
    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Died;
    private void Awake() => _hp = _maxHp;
    public void Damage(int amount) => Hp -= amount;
    public void Heal(int amount) => Hp += amount;
    public void HealFull() => Hp = _maxHp;
    public void Kill() => Hp = 0;
    public void Adjust(int value) => Hp = value;

}