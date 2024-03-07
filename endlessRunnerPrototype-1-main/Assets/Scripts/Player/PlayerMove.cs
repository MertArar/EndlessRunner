using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float leftRightSpeed = 2;
    public float jumpingSpeed = 3;
    public float slidingSpeed = 3;
    static public bool canMove = false;
    public bool isJumping = false;
    public bool isSliding = false;
    public bool comingDown = false;
    public bool comingUp = false;
    public GameObject playerObject;
    
    private float elapsedTime = 0f;
    private float increaseSpeedTime = 30f;
    
    
    private void Update()
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
        
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {  
                if (this.gameObject.transform.position.x > LevelBoundry.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                    //buraya zamanlıyıcı ve salisede 0.1 artsın selim agam anlar

                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < LevelBoundry.rightSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
                    //buraya zamanlıyıcı ve salisede 0.1 artsın selim agam anlar
                }
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

        if (isSliding == true)
        {
            if (comingUp == false)
            {
                transform.Translate(Vector3.down * Time.deltaTime * slidingSpeed, Space.World);
            }

            if (comingUp == true)
            {
                transform.Translate(Vector3.down * Time.deltaTime * -slidingSpeed, Space.World);
            }
        }
    }

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