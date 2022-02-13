using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite BrokenBase;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        sr.sprite = BrokenBase;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        PlayerManager.Instance.isDead = true;
    }
}
