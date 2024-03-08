using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Player
    public GameObject playerObject;
    public CharacterController player;

    // Speeds
    public float moveSpeed = 10f;
    public float leftRightSpeed = 2;
    public float jumpingSpeed = 10;
    public float slidingSpeed = 0.5f;

    // Times
    private float elapsedTime = 0f;
    private float increaseSpeedTime = 30f;

    // Bool properties
    static public bool canMove = false;
    private bool canMoveAfterLaneChange = true;
    public bool isJumping = false;
    public bool isSliding = false;
    public bool comingDown = false;
    public bool comingUp = false;

    private int currentLane = 1; // 0: Left, 1: Middle, 2: Right

    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        IncreaseValues();
        PlayerController();
        JumpFunctions();
        SlideFunctions();
    }

    public void IncreaseValues()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 15f)
        {
            moveSpeed++;
            leftRightSpeed++;
            elapsedTime = 0f;
        }

        if (moveSpeed >= 25f)
        {
            moveSpeed = 25f;
        }

        if (leftRightSpeed >= 6)
        {
            leftRightSpeed = 6;
        }
    }

    public void PlayerController()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        if (canMove == true && canMoveAfterLaneChange == true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLane(-1); // Move left
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveLane(1); // Move right
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                if (isJumping == false)
                {
                    isJumping = true;
                    playerObject.GetComponent<Animator>().Play("Jump");
                    StartCoroutine(JumpSequence());
                }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (isSliding == false)
                {
                    isSliding = true;
                    playerObject.GetComponent<Animator>().Play("Running Slide");
                    StartCoroutine(SlideSequence());
                }
            }
        }
    }

    private void MoveLane(int direction)
    {
        if (!canMoveAfterLaneChange)
            return;

        int newLane = Mathf.Clamp(currentLane + direction, 0, 2);

        float targetX = (newLane - 1) * 1.33f; // Her şerit 4 birim genişliğinde
        StartCoroutine(MoveToLane(targetX, 1f)); // Geçiş süresi 0.5 saniye

        currentLane = newLane;
        canMoveAfterLaneChange = false;
        StartCoroutine(EnableMovementAfterDelay(0)); // Yarım saniye sonra hareketi etkinleştir
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMoveAfterLaneChange = true;
    }

    private IEnumerator MoveToLane(float targetX, float duration)
    {
        float startTime = Time.time * Time.deltaTime;

        while (Time.time < startTime + duration)
        {
            float t = ((Time.time - startTime) * Time.deltaTime) / duration;
            float currentX = Mathf.Lerp(transform.position.x, targetX, t);
            transform.position = new Vector3(currentX, transform.position.y, transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    public void JumpFunctions()
    {
        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * jumpingSpeed, Space.World);
            }

            if (comingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -jumpingSpeed, Space.World);
            }
        }
    }

    public void SlideFunctions()
    {
        if (isSliding == true)
        {
            if (comingUp == false)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 0.5f);
            }
        }
    }

    // Numerators
    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }

    IEnumerator SlideSequence()
    {
        yield return new WaitForSeconds(0.7f);
        comingUp = true;
        yield return new WaitForSeconds(0.7f);
        isSliding = false;
        comingUp = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }
}
