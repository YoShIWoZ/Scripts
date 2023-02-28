using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public GameObject graphics;

    public string mobName; 

    public bool netInRange = false;
    public bool enemyKilled = false;

    private Transform target; //drag and stop player object in the inspector
    public float within_range;
    public float speed;
    public int destroyRange = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Body");
        target = player.transform;
    }

    public void Update()
    {
        //Animations

        #region Movement Control
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        //get the distance between the player and enemy (this object)
        float dist = Vector3.Distance(target.position, transform.position);
        //check if it is within the range you set
        if (dist <= within_range && !GlobalVariables.vRGamePaused)
        {
            Debug.Log("I am far away!" + gameObject.name);
            var distance = Vector3.Distance(transform.position, target.position);// no need to perform this operation twice.
            if (mobName == "Pumpking")
            {
                if (distance < 500 && distance > 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), speed);
                    graphics.GetComponent<Animator>().SetBool("moving", true);
                }
                else graphics.GetComponent<Animator>().SetBool("moving", false);
            }
            else if (mobName == "Gourd")
            {
                if (distance < 5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), -speed);
                    graphics.GetComponent<Animator>().SetBool("moving", true);
                }
                else
                    graphics.GetComponent<Animator>().SetBool("moving", false);
            }
            if (distance > destroyRange)
            {
                Destroy(gameObject);
                EnemySpawner.mobAmount -= 1;
            }

        }
        //else, if it is not in rage, it will not follow player
        #endregion
        if (Input.GetButtonDown("Submit") && netInRange)
        {
            if (mobName == "Gourd")
                GlobalVariables.points += 2;
            else if (mobName == "Pumpking")
                GlobalVariables.points += 3;
            else if (mobName == "Squash")
                GlobalVariables.points += 1;
            Destroy(gameObject);
            EnemySpawner.mobAmount -= 1;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {

            netInRange = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            netInRange = false;
        }
    }
}