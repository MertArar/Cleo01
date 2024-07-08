using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator playerAnimator;
    
    int currentPos = 0;

    public float sideSpeed;
    public float jumpForce;
    public float slidingForce;

    bool isGameStarted = false;
    
    
    void Start()
    {
        isGameStarted = false;
        currentPos = 0;
    }
  
    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Game is started");
                isGameStarted = true;
                playerAnimator.SetInteger("isRunning", 1);
                playerAnimator.speed = 1.2f;
            }
        }
        
        if (isGameStarted)
        {
            if (currentPos == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentPos = 1;
                }
        
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentPos = 2;
                }
            }
        
            else if (currentPos == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                { 
                    currentPos = 0;
                }
            }
        
            else if (currentPos == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentPos = 0;
                }
            }

            if (currentPos == 0)
            {
                if (Vector3.Distance(transform.position, new Vector3(centerPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(centerPos.position.x, transform.position.y, transform.position.z) - transform.position ;
                    transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World);
                }
            }
            else if (currentPos == 1)
            {
                if (Vector3.Distance(transform.position, new Vector3(leftPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(leftPos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World);
                }
            }
            else if (currentPos == 2)
            {
                if (Vector3.Distance(transform.position, new Vector3(rightPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(rightPos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World); 
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetInteger("isJump", 1);
                rb.velocity = Vector3.up * jumpForce;
                StartCoroutine(Jump());
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerAnimator.SetInteger("isSliding", 1);
                rb.MovePosition(rb.position + new Vector3(0,0,2.2f) * playerAnimator.deltaPosition.magnitude * slidingForce);             
                StartCoroutine(Slide());
            }
        }
    }

    IEnumerator Jump()
    {
        playerAnimator.SetInteger("isJump", 1);
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetInteger("isJump", 0);
    }

    IEnumerator Slide()
    {
        playerAnimator.SetInteger("isSliding", 1);
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetInteger("isSliding", 0);
    }
}
