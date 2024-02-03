using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;

    [SerializeField] 
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShot;
    [SerializeField]
    private GameObject shieldObject;
    [SerializeField]
    private GameObject fiveShot;

    public bool tripleShotActive = false;

    public float fireRate = 0.3f;
    public float lastFired = -0.3f;

    private int score = 0;
    private UiManager uiManager;

    private bool shieldActive = false;

    private SpawnManager spawnManager;

    private bool fiveShotActive = false;

    public int startingLives = 3;
    public int currentLives;

    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("SPAWN MANAGER IS NULL");
        }

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();

        currentLives = startingLives;
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerFire();

    }

    void playerMovement()
    {
    float horizontalIput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    transform.Translate(Vector3.up* verticalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.right* speed * horizontalIput * Time.deltaTime);

        if (transform.position.x >= 9.1f)
        {
            transform.position = new Vector3(9.1f, transform.position.y, 0);
}
        else if (transform.position.x <= -9.1f)
{
    transform.position = new Vector3(-9.1f, transform.position.y, 0);
}

if (transform.position.y >= 5.8f)
{
    transform.position = new Vector3(transform.position.x, 5.8f, 0);
}
else if (transform.position.y <= -3.75f)
{
    transform.position = new Vector3(transform.position.x, -3.75f, 0);
}
    }

    void playerFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > lastFired + fireRate)
        {
            if (tripleShotActive == true)
            {
                Instantiate(tripleShot, transform.position, Quaternion.identity);
            }
            else if (fiveShotActive == true)
            {
                Instantiate(fiveShot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position, Quaternion.identity);
            }

            lastFired = Time.time;
        }

        

    }

    public void Damage()
    {
        if (shieldActive == true)
        {
            shieldObject.SetActive(false);
            shieldActive = false;
            return;
        }

        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            Destroy(this.gameObject);
            spawnManager.PlayerDied();
        }

    }

    public void ActivateTripleShot()
    {
        tripleShotActive = true;

        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(500f);
        tripleShotActive = false;

    }

    public void ActivateShield()
    {
        Debug.Log("Activating shield...");
        shieldActive = true;
        shieldObject.SetActive(true);
        StartCoroutine(ActivateShieldPowerUp());
    }

    IEnumerator ActivateShieldPowerUp()
    {
        Debug.Log("Starting ActivateShieldPowerUp coroutine");
        yield return new WaitForSeconds(5f);
        shieldActive = false;
        shieldObject.SetActive(false);
    }

    public void AddScore()
    {
        score++;

        if(uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < uiManager.livesImage.Length; i++)
        {
            if (i < currentLives)
            {
                uiManager.livesImage[i].enabled = true; // Show the heart
            }
            else
            {
                uiManager.livesImage[i].enabled = false; // Hide the heart
            }
        }
    }
    
    public void ActivateFiveShot()
    {
        tripleShotActive = false;
        fiveShotActive = true;
        StartCoroutine(DeactivateFiveShot());
    }

    IEnumerator DeactivateFiveShot()
    {
        yield return new WaitForSeconds(30f);
        fiveShotActive = false;
    }
}
