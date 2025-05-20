using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class Shoot : MonoBehaviour
{
    public GameObject ball; //reference to the ball prefab, set in editor 

    private Vector3 throwSpeed = new Vector3(0, 8, 5); //This value is a sure basket, we'll modify this using the forcemeter 

    public Vector3 ballPos; //starting ball position 

    private bool thrown = false; //if ball has been thrown, prevents 2 or more balls 

    public GameObject availableShotsGO; //ScoreText game object reference 

    private int availableShots = 5;

    public GameObject meter; //references to the force meter 

    public GameObject arrow;

    private float arrowSpeed = 3.3f; //Difficulty, higher value = faster arrow movement 

    private bool right = true; //used to revers arrow movement 

    public GameObject gameOver; //game over text

    void Start()
    {
        /* Increase Gravity */
        Physics.gravity = new Vector3(0, -20, 0);
    }

    void FixedUpdate()
    {
        /* Move Meter Arrow */

        if (arrow.transform.position.x < 1090 && right)
        {
            arrow.transform.position += new Vector3(arrowSpeed, 0, 0);
        }
        if (arrow.transform.position.x >= 1090)
        {
            right = false;
        }
        if (right == false)
        {
            arrow.transform.position -= new Vector3(arrowSpeed, 0, 0);
        }
        if (arrow.transform.position.x <= 845)
        {
            right = true;
        }
        /* Shoot ball on Tap */
        if (Input.GetButton("Fire1") && !thrown && availableShots > 0)
        {
            thrown = true;
            availableShots--;
            availableShotsGO.GetComponent<TextMeshProUGUI>().text = availableShots.ToString();
            throwSpeed.y += arrow.transform.position.x / 100;
            ball.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
            Debug.Log($"y = {ball.transform.position.y} x = {ball.transform.position.x} throwSpeed = {throwSpeed}");
            throwSpeed.y = 8;
            GetComponent<AudioSource>().Play(); //play shoot sound 
        }
        /* Remove Ball when it hits the floor */
        if (ball.transform.position.y <= -5)
        {
            ball.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            ball.transform.position = new Vector3(0, -4, -13.1f);
            thrown = false;
            throwSpeed = new Vector3(0, 8, 5); //Reset perfect shot variable
            /* Check if out of shots */

            if (availableShots == 0)
            {
                Thread.Sleep(2000);
                Invoke(nameof(Restart), 2);
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
