using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantScript : MonoBehaviour
{
    private Health _health;
    private GameObject player;
    private int scoreVal;
    public CapsuleCollider storeFront;
    public GameObject storeIntro;
    private bool inStore = false;
    private bool shopping = false;
    public GameObject storeMenu;
    //public GameObject standardMenu;
    //public GameObject speedIndicator;
    //public GameObject gun;
    //public GameObject gunAssociate;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {

        storeMenu.SetActive(false);
    }
    private void openShop()
    {
        storeIntro.SetActive(false);
        storeMenu.SetActive(true);
        //standardMenu.SetActive(false);
        //speedIndicator.SetActive(false);
        //gun.SetActive(false);
        //gunAssociate.SetActive(false);
    }
    private void closeShop()
    {

        //standardMenu.SetActive(true);
        storeMenu.SetActive(false);
        //speedIndicator.SetActive(true);
        //gun.SetActive(true);
        //gunAssociate?.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && other.TryGetComponent(out Health player)) //We check for a tag so we know we aren't colliding with a wall. Can be removed if necessary, but should
                                   //just add a Player tag to the player game objects.
        {
            _health = player;
            storeIntro.SetActive(true);
            inStore = true;
            //Here we can get all the data we want from the collision
            //For example: You have a script that stores the player's name and/or ID.
            //You now just GetComponent from other and get the script you want and what values you want from that script
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" && other.TryGetComponent(out Health player)) //We check for a tag so we know we aren't colliding with a wall. Can be removed if necessary, but should
                                   //just add a Player tag to the player game objects.
        {
            _health = player;
            storeIntro.SetActive(false);
            inStore = false;
            shopping = false;
            closeShop();
            //Here we can get all the data we want from the collision
            //For example: You have a script that stores the player's name and/or ID.
            //You now just GetComponent from other and get the script you want and what values you want from that script
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && (inStore == true) && shopping == false)
        {
            shopping = true;
            openShop();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && (inStore == true) && shopping == true)
        {
            shopping = false;
            closeShop();
            storeIntro.SetActive(true);
        }
        if (shopping == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && ScoreScript.scoreValue >= 30 && _health.Hp < 100)
            {
                _health.Heal(100);
                ScoreScript.scoreValue -= 30;
            }
        }
        //If the player overlaps the Shop Collision then give options to buy
        //health with points, or switch weapons

    }
}
