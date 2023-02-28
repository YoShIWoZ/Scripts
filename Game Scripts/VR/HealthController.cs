using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject heart1Full;
    public GameObject heart1Empty;
    public GameObject heart2Full;
    public GameObject heart2Empty;
    public GameObject heart3Full;
    public GameObject heart3Empty;

    private int health;
    private bool enemyDamage;
    private bool damageTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        heart1Full.SetActive(true);
        heart2Full.SetActive(true);
        heart3Full.SetActive(true);

        heart1Empty.SetActive(false);
        heart2Empty.SetActive(false);
        heart3Empty.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 3)
        {
            heart1Full.SetActive(true);
            heart2Full.SetActive(true);
            heart3Full.SetActive(true);
            heart1Empty.SetActive(false);
            heart2Empty.SetActive(false);
            heart3Empty.SetActive(false);
        }
        else if (health == 2)
        {
            heart1Full.SetActive(true);
            heart2Full.SetActive(true);
            heart3Full.SetActive(false);
            heart1Empty.SetActive(false);
            heart2Empty.SetActive(false);
            heart3Empty.SetActive(true);
        }
        else if (health == 1)
        {
            heart1Full.SetActive(true);
            heart2Full.SetActive(false);
            heart3Full.SetActive(false);
            heart1Empty.SetActive(false);
            heart2Empty.SetActive(true);
            heart3Empty.SetActive(true);
        }
        else
        {
            heart1Full.SetActive(false);
            heart2Full.SetActive(false);
            heart3Full.SetActive(false);
            heart1Empty.SetActive(true);
            heart2Empty.SetActive(true);
            heart3Empty.SetActive(true);
            Debug.Log("You ded");
        }
        if (enemyDamage)
        {
            health -= 1;
            enemyDamage = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("PumpkingInRange") && collision.isTrigger)
        {
            if (!damageTaken)
            {
                enemyDamage = true;
                damageTaken = true;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("PumpkingInRange") && collision.isTrigger)
        {
            if (damageTaken)
            {
                damageTaken = false;
            }
        }
    }
}
