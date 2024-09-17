using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    //Reference to player rigid body for its velocity
    [SerializeField] private Rigidbody _player;

    //The text component
    TMP_Text speedometerText;

    void Start(){
        speedometerText = GetComponent<TMP_Text>();
    }

    //Updates the text to the player's current speed
    private void LateUpdate()
    {
        if(_player.velocity.magnitude < 0.001f)
            speedometerText.text = "Speed: 0.00";
        else
            speedometerText.text = "Speed: " + _player.velocity.magnitude.ToString("0.00");
    }
}
