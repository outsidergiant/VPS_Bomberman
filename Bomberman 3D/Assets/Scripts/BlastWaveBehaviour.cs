using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class BlastWaveBehaviour : MonoBehaviour {

    public Text scoreText;

    private PlayerController playerController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            KillCharacter(other);
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.player.points += Enemy.GetPointNumberByName(other.gameObject.name);
            SetScoreText("Score: " + playerController.player.points.ToString());


        }
        if (other.gameObject.CompareTag("Player")) {
            KillCharacter(other);
        }
        if (other.gameObject.CompareTag("Brick"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Wave"))
        {
            Destroy(other.gameObject);
        }
    }

    private void SetScoreText(string text)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("ScoreText");
        scoreText = canvas.GetComponentInChildren<Text>();
        scoreText.text = text;
    }

    private void KillCharacter(Collider other)
    {
        Animator animator = other.gameObject.GetComponentInParent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Killed");
            other.enabled = false;
        } else
        {
            Destroy(other.gameObject);
        }
    }
}
