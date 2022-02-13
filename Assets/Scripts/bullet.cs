using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    public bool isPlayerBullet = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(transform.up * Time.deltaTime * bulletSpeed, Space.World);


    }

/*    public void SetisPlayerBullet(bool tf)
    {
        isPlayerBullet = tf;
    }
*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (isPlayerBullet == false)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
               
                break;
            case "Basement":
                Destroy(gameObject);
                collision.SendMessage("GameOver");
                break;
            case "EnemyTank":
                if (isPlayerBullet)
                {
                    Destroy(gameObject);
                    collision.SendMessage("Die");
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
