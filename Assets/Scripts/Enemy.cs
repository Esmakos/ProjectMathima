using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 8f;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * Random.Range(minSpeed, maxSpeed));

        if(transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            if (playerController != null)
            {
                playerController.AddScore();
            }
        }

        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.transform.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.Damage();
                Destroy(this.gameObject);
            }

        }
    }

    


}
