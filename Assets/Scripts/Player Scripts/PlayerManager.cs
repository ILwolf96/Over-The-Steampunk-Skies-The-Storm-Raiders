using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public EnemyEnergyShot shotManager;
    public float hp = 100;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy Shot"))
        {
            //I'm sorry. this had to be done to make sure the damage the player recieves is correct.
            shotManager = collision.gameObject.GetComponent<EnemyEnergyShot>(); 
            hp -= shotManager.damage; //Removes hp from the player.
            Debug.Log("HP Left: " + hp);
            Destroy(collision.gameObject);
        }
            
    }
}
