using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 1f;
    public float fowardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    GameManager gameManager;

    public int life = 2;
    public Image[] lifeImages;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();


        if (animator == null)
        {
            Debug.Log("animator null");
        }

        if (_rigidbody == null)
        {
            Debug.Log("_rigidbody null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // 게임 재시작
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            // 게임 재시작
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = fowardSpeed;

        if (isFlap)
        {
            velocity.y = flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        if (GameManager.Instance.isLv1Clear) return;
        if (GameManager.Instance.isLv2Clear) return;
        if (GameManager.Instance.isLv3Clear) return;

        if (life > 1)
        {
            life--;
           // lifeImages[life].gameObject.SetActive(false);
            lifeImages[life].sprite = LifeManager.instance.LifeSprites[1];
            Debug.Log("Life : " + life);
            animator.SetBool("IsHit", true);
            godMode = true;
            Invoke("SetfalseIsHit", 1f);
        }
        else
        {
            lifeImages[0].sprite = LifeManager.instance.LifeSprites[1];
            isDead = true;
            deathCooldown = 1f;

            animator.SetBool("IsDie", true);
            gameManager.GameOver();
        }
            
    }

    private void SetfalseIsHit()
    {
        animator.SetBool("IsHit", false);
        godMode = false;
    }

}
