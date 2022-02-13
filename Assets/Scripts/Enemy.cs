using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Vector3 bulletEularAngles;
    private float timeVal;
    private float turnTimeVal;
    private float v = -1;
    private float h;

    private SpriteRenderer sr;
    public Sprite[] tankSprite;//clockwise
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;


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



        if (timeVal >= 3.0f)//attack CD
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
        //checkLock();
        Move();//tank moves



    }
    private void Move()
    {
        if (turnTimeVal >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {

                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            turnTimeVal = 0;
        }
        else
        {
            turnTimeVal += Time.fixedDeltaTime;
        }
        float verti = v;
        float hori = h;
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

    private void Attack()
    {

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
        timeVal = 0;

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
        PlayerManager.Instance.Score++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Destroy(gameObject);//die
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyTank")
        {
            turnTimeVal = 4;
        }
    }
}
