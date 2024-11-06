using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float _maxAlphaVal;
    [SerializeField] private Image _fillMask;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _barBack;

    private PlayerMovement _playerMovement;
    private float alphaVal = 0;

    private Color fillColor;
    private Color backColor;

    void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        fillColor = _fillImage.color;
        backColor = _barBack.color;

        fillColor.a = 0;
        backColor.a = 0;

        _fillImage.color = fillColor;
        _barBack.color = backColor;
    }

    public void Update()
    {
        float fillAmount = _fillMask.fillAmount;

        _fillMask.fillAmount = _playerMovement.GetStamina()/_playerMovement.GetMaxStamina();

        if(fillAmount < 1){
            alphaVal = _maxAlphaVal;
            
            fillColor.a = alphaVal;
            _fillImage.color = fillColor;

            
            backColor.a = alphaVal;
            _barBack.color = backColor;
        }
        else if(alphaVal > 0){
            alphaVal -= 1 * Time.deltaTime;
            fillColor.a = alphaVal;
            _fillImage.color = fillColor;

            backColor.a = alphaVal;
            _barBack.color = backColor;
        }
    }
}
