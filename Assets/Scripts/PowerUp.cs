using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField]
    private int powerUpId;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            PlayerController playerController = other.transform.GetComponent<PlayerController>();

            if (playerController != null)
            {
                if (powerUpId == 1)
                {
                    if (playerController.tripleShotActive)
                    {
                        // If triple shot is already active, upgrade to five shot
                        playerController.ActivateFiveShot();
                    }
                    else
                    {
                        // Otherwise, activate triple shot
                        playerController.ActivateTripleShot();
                    }
                }
                else if (powerUpId == 2)
                {
                    playerController.ActivateShield();
                }
            }
        }
    }
}




