using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector3 currentLoc;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    public int dayNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        GlobalVariables.dayNumber = dayNumber;
        // Switch to 640 x 480 full-screen at 60 hz
        //Screen.SetResolution(1366, 768, true, 60);

        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        change.x = 0f;
        change.y = -1f;
        UpdateAnimationAndMove();

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.dayNumber == 3)
            animator.SetBool("longHair", true);
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();

    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero && GlobalVariables.canMove)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
public static class GlobalVariables
{
    public static bool doorActivated;
    public static bool canMove = true;
    public static bool spaceSuitOn;
    public static bool hasEaten;
    public static bool diaryWritten;
    public static string diaryText;
    public static bool powerOn = true;
    public static bool powerReturned = false;
    public static int points = 0;
    public static string plantName = "";
    public static bool hasTripped = false;
    public static int dayNumber = 1;
    public static bool ductTapeTaken = false;
    public static bool hasRolled = false;
    public static bool lastEntryRead = false;
    public static bool posterFixed = false;
    public static bool o2Fixed = false;
    public static bool screwdriverAdded = false;
    public static bool screwdriverTaken = false;
    public static bool canInteract = true;
    public static bool gameCompleted = false;
    public static bool gameCompletedAnnouncement = false;
    public static bool secretTapeTaken = false;
    public static bool secretTapePlayed = false;
    public static bool usedGenerator = false;
    public static Vector2 savedPosition = new Vector2(0f, 0f);
    public static bool vRGamePaused = false;
}