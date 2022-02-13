using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerManager : MonoBehaviour
{
    public int HP = 3;
    public int Score = 0;
    public bool isDead = false;
    public bool isDefeat;

    public GameObject born;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI playerValueText;
    public GameObject isDefeatUI;

    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = Score.ToString();
        playerValueText.text = HP.ToString();
    }

    private void Recover()
    {
        if(HP <= 0)
        {
            isDead = true;
            isDefeat = true;

        }
        else
        {
            HP -= 1;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
}
