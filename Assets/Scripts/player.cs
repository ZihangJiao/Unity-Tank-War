using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Vector3 bulletEularAngles;
    private float timeVal;
    private float timeInvincibleVal = 3.0f;
    private bool invincible = true;

    private SpriteRenderer sr;
    public Sprite[] tankSprite;//clockwise
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject invinciblePrefab;

    public bool Movekey;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)//invincible check
        {
            invinciblePrefab.SetActive(true);
            timeInvincibleVal -= Time.deltaTime;
            if (timeInvincibleVal <= 0)
            {
                invincible = false;
            }
        }
        else
        {
            invinciblePrefab.SetActive(false);
        }


        if (timeVal >= 0.4f)//attack CD
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }


    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            PlayerManager.Instance.HP = 0;
        }

        checkLock();
        Move();//tank moves
       


    }
    private void Move()
    {
        float verti = Input.GetAxisRaw("Vertical");
        float hori = Input.GetAxisRaw("Horizontal");
        if (verti * hori == 0)//no conflictions
        {
            if (verti != 0)
            {
                moveV(verti);
            }
            else
            {
                moveH(hori);
            }
        }
        else if (Movekey)
        {
            moveH(hori);
        }
        else
        {
            moveV(verti);
        }
    }

    private void print()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
            timeVal = 0;
        }
    }
    private void checkLock()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            Movekey = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            Movekey = true;
        }
    }

    private void moveV(float verti)
    {
        transform.Translate(Vector3.up * verti * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (verti < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3(0, 0, -180);

        }
        else if (verti > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3(0, 0, 0);
        }

        if (verti != 0)
        {
            return;
        }
    }

    private void moveH(float hori)
    {
        transform.Translate(Vector3.right * hori * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (hori < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3(0, 0, 90);
        }
        else if (hori > 0)
        {

            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3(0, 0, -90);
        }
    }

    private void Die()
    {
        if (invincible)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Destroy(gameObject);//die
    }

}

