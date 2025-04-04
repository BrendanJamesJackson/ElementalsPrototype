using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int player1Health;
    private int player2Health;

    [Header("Player Objects")]
    public GameObject player1;
    public GameObject player2;


    private void OnEnable()
    {
        player1.GetComponent<PlayerManager>().OnHealthChanged += UpdateHealthP1;
    }

    private void UpdateHealthP1(float health)
    {

    }

}
