using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2;

    private Vector3 tempScale;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    private playerAnimation enemyAnimation;

    [SerializeField]
    private float attackWaitTime = 2.5f;
    private float attackTimer;

    [SerializeField]
    private float attackFinishedWaitTime = 0.5f;
    private float attackFinishedTimer;

    [SerializeField]
    private EnemyDamageArea enemyDamageArea;

    private bool enemyDied;

    [SerializeField]
    private RectTransform healthBarTransform;
    private Vector3 healthBarTempScale;

    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;

        enemyAnimation = GetComponent<playerAnimation>();
    }

    private void Update()
    {
        if (enemyDied) return;

        SearchForPlayer();
    }
    void SearchForPlayer()
    {
        if (!playerTarget) return;

        if(Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);

            Vector3 tempPos = transform.position;
           
            if (tempPos.y < -2.9f)
            {
                tempPos.y = -2.9f;
            }
            if (tempPos.y > -2.7f)
            {
                tempPos.y = -2.7f;
            }
            transform.position = tempPos;
            //

            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);

            HandleFacingDirection();
        }
        else
        {
            CheckIfAttackFinished();
            Attack();
        }
    }

    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (transform.position.x < playerTarget.position.x) tempScale.x = Mathf.Abs(tempScale.x);
        else tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;

        // health bar scale
        healthBarTempScale = healthBarTransform.localScale;
        if (transform.localScale.x > 0f) healthBarTempScale.x = Mathf.Abs(healthBarTempScale.x);
        else healthBarTempScale.x = -Mathf.Abs(healthBarTempScale.x);

        healthBarTransform.localScale = healthBarTempScale;
    }

    void CheckIfAttackFinished()
    {
        if(Time.time > attackFinishedTimer)
        {
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }
    void Attack()
    {
        if(Time.time > attackTimer)
        {
            attackFinishedTimer = Time.time + attackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);
        }
    }

    void EnemyAttacked()
    {
        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }

    public void EnemyDied()
    {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay", 1f);
    }

    void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);
    }
}
