using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTom : MonoBehaviour
{

    public static MoveTom instance { get; private set; }
    private int score;
    private int lives;

    AudioClip clip1; 
    AudioClip clip2; 
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    private AudioSource source;
    private float movementSpeed = 2f;
    float horizontal;
    float vertical;
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    public GameObject projectilePrefab;
    public GameObject vignettePrefab;

    public GameObject coffeePrefab;

    private GameObject vignette;

    public InputAction launchAction;

    private bool powerupPresent = false;

    private float coffeeStarted = 0;
    
    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); // Only if you want the instance to persist across scenes
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        launchAction.Enable();
        launchAction.performed += Launch;
        score = 0;
        lives = 3;

        audio3.loop = true;
        audio3.Play();
    }

    public void MakeDrunk(){
        if (vignette == null){
            vignette = Instantiate(vignettePrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {    
        horizontal = 0.0f;
        vertical = 0.0f;

        if (Keyboard.current.wKey.isPressed) {
            vertical = 1.0f;
        } else if (Keyboard.current.sKey.isPressed) {
            vertical = -1.0f;
        }

        if (Keyboard.current.aKey.isPressed) {
            horizontal = -1.0f;
        } else if (Keyboard.current.dKey.isPressed) {
            horizontal = 1.0f;
        }

        transform.position = transform.position + new Vector3(horizontal * movementSpeed * Time.deltaTime, vertical * movementSpeed * Time.deltaTime, 0);

        if (Keyboard.current.qKey.isPressed)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Keyboard.current.eKey.isPressed)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (Time.realtimeSinceStartup-coffeeStarted > 10) {
                ResetMovementSpeed();
                powerupPresent = false;
        }

        GeneratePowerup();
    }

    void Launch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = transform.up;
            float force = 300;
            GameObject projectile = Instantiate(projectilePrefab, transform.position + (Vector3)direction * 0.5f, Quaternion.identity);
            projectile.transform.rotation = transform.rotation;
            projectile.GetComponent<Rigidbody2D>().AddForce(direction * force);
            audio1.Play();
        }

       
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag) {
            case "Sharonn":
                DecreaseLives();
                break;
            case "MarsBar":
            case "FloorCone":
                DecreaseLives();
                Destroy(other.gameObject);
                break;
            case "Coffee":
                SpeedUpTom();
                Destroy(other.gameObject);
                break;
        }

        if (other.gameObject.tag == "Cocktail") 
        {
            MoveTom.instance.MakeDrunk();
            Destroy(other.gameObject);
        }
    }

    public void DecreaseLives()
    {
        audio2.Play();
        lives--;
        UIHandler.instance.UpdateLives(lives);

        if (lives == 0)
        {
            Debug.Log("Player is out of lives!");
            audio3.Stop();
            Destroy(gameObject);
        }
}

    public void IncreaseScore(int amount)
    {
        score += amount;
        UIHandler.instance.UpdateScore(score);
    }

    public void OnDisable()
    {
        launchAction.performed -= Launch;
        if (GameOverUI.instance != null) {
            GameOverUI.instance.GameOver(score);
        }
    }

public void SpeedUpTom() {
    Debug.Log("Speeding up Tom");
        movementSpeed = 4f;
    }


    public void ResetMovementSpeed() {
        movementSpeed = 2f;
    }

    public void GeneratePowerup() {
        if (!powerupPresent) {
            // decide which powerup to put out
            System.Random random = new System.Random();
            int number = random.Next(0, 50);

            if (number == 20) {
                // we're gonna do a pineapple
            } else if (number < 5) {
                // we're gonna do a coffee
                
                int xSpawnCoords;
                int ySpawnCoords;
                
                Debug.Log("Generating co-ordinates");
                if (random.Next() > 0) {
                    xSpawnCoords = random.Next(-14, 9);
                    ySpawnCoords = random.Next(-1, 3);
                } else {
                    xSpawnCoords = random.Next(-6, 0);
                    ySpawnCoords = random.Next(-8, -2);
                }
                Debug.Log("Rendering Object");
                GameObject powerUp = Instantiate(coffeePrefab, new Vector3(xSpawnCoords, ySpawnCoords), Quaternion.identity);
                coffeeStarted = Time.realtimeSinceStartup;
                powerupPresent = true;
            }


        }
    }


}
