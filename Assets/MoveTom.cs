using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTom : MonoBehaviour
{

    public static MoveTom instance { get; private set; }
    private int score;
    private int lives;

    private float movementSpeed = 2f;
    float horizontal;
    float vertical;
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    public GameObject projectilePrefab;
    public GameObject vignettePrefab;

    private GameObject vignette;

    public InputAction launchAction;
    
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

        if (Keyboard.current.vKey.isPressed)
        {
            MakeDrunk();
        }

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

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
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
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Sharonn" || other.gameObject.tag == "MarsBar")
        {
            if (lives > 0) {
                DescraseLives();
            } else {
                Destroy(gameObject);
            }
        }
        
        if (other.gameObject.tag == "FloorCone") 
        {
            DescraseLives(); 
            Destroy(other.gameObject);
        }
    }

    public void DescraseLives()
    {
        lives--;
        UIHandler.instance.UpdateLives(lives);

        if (lives == 0)
        {
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
        if (UIHandler.instance != null) {
            UIHandler.instance.GameOver(score);
        }
    }
}
