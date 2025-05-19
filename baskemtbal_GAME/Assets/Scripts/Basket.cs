using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public GameObject score; //reference to the ScoreText gameobject, set in editor

    public AudioClip brick; //reference to the miss sound

    public AudioClip basket; //reference to the basket sound

    void OnCollisionEnter(Collision collision) //if ball hits board
    {
        AudioSource.PlayClipAtPoint(brick, transform.position); //plays the hit board sound
    }

    void OnTriggerEnter(Collider other) //if ball hits basket collider
    {
        TextMeshProUGUI scoreText = score.GetComponent<TextMeshProUGUI>();
        int currentScore = int.Parse(scoreText.text) + 1; //add 1 to the score
        scoreText.text = currentScore.ToString();
        if (scoreText.text.Equals("5"))
        {
            AudioSource.PlayClipAtPoint(basket, transform.position); //play basket sound
        }
    }
}