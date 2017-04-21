using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour {

    //private Player player;
    private PlayerController playerController;
    //private Player player;

    // Use this for initialization
    void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //player = playerController.player;
        //player = new Player(GameObject.FindGameObjectWithTag("Player"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerController.player.gameObject.GetComponent<Animator>().SetTrigger("Killed");
            //this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.SetActive(false);
            if (other.gameObject.name.Contains("Wallpass"))
            {
                playerController.player.abilities.Add(PowerUp.Wallpass);
            }
            else if (other.gameObject.name.Contains("Bombs"))
            {
                playerController.player.abilities.Add(PowerUp.Bombs);
                playerController.player.bombNumber++;
            }
            else if (other.gameObject.name.Contains("Speed"))
            {
                playerController.player.abilities.Add(PowerUp.Speed);
                playerController.player.speed++;
                //Debug.Log(player.speed);
            }
            else if (other.gameObject.name.Contains("Flames"))
            {
                playerController.player.abilities.Add(PowerUp.Flames);
                playerController.player.explosionRadius++;
            }
            else if (other.gameObject.name.Contains("Detonator"))
            {
                playerController.player.abilities.Add(PowerUp.Detonator);
            }

        }
        //} else if (other.gameObject.CompareTag("Brick")) {
        //    if (player.abilities.Contains(PowerUp.Wallpass)) {
        //        GoSmooth()
        //    }
        //}
    }

}
