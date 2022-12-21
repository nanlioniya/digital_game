using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;

    [SerializeField]
    private float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.47f, maxBound_Y = -2.74f;

    private Vector3 tempPos;
    private float xAxis, yAxis;

    private playerAnimation playerAnimation;

    [SerializeField]
    private float shootWaitTime = 0.5f;
    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f;

    private float waitBeforeMoving;

    private bool canMove = true;

    private PlayerShootingManager playerShootingManager;

    private bool playerDied;
    private void Awake()
    {
        playerAnimation = GetComponent<playerAnimation>();

        playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDied) return;

        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();

        HandleShooting();
        CheckIfCanMove();
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!canMove) return;

        tempPos = transform.position;

        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if(tempPos.x < minBound_X)
        {
            tempPos.x = minBound_X;
        }
        if (tempPos.x > maxBound_X)
        {
            tempPos.x = maxBound_X;
        }
        if (tempPos.y < minBound_Y)
        {
            tempPos.y = minBound_Y;
        }
        if (tempPos.y > maxBound_Y)
        {
            tempPos.y = maxBound_Y;
        }

        transform.position = tempPos;
    }

    void HandleAnimation()
    {
        if (!canMove) return;

        if(Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
        else
        {
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

    void HandleFacingDirection()
    {
        if (xAxis > 0) playerAnimation.SetFacingDirection(true);
        else if (xAxis < 0) playerAnimation.SetFacingDirection(false);
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }

    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement();

        playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        playerShootingManager.Shoot(transform.localScale.x); 
    }

    void CheckIfCanMove()
    {
        if (Time.time > waitBeforeMoving) canMove = true;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.time > waitBeforeShooting) Shoot();
        }
    }

    public void PlayerDied()
    {
        playerDied = true;
        playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyPlayerAfterDelay", 1f);
        SceneManager.LoadScene(3);

    }
    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }
}
