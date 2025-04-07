using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float player1Health;
    private float player2Health;

    [Header("Player Objects")]
    public GameObject player1;
    public GameObject player2;


    private void OnEnable()
    {
        player1.GetComponent<PlayerManager>().OnHealthChanged += UpdateHealthP1;
        player2.GetComponent<PlayerManager>().OnHealthChanged += UpdateHealthP2;
    }

    private void OnDisable()
    {
        player1.GetComponent<PlayerManager>().OnHealthChanged -= UpdateHealthP1;
        player2.GetComponent<PlayerManager>().OnHealthChanged -= UpdateHealthP2;
    }

    private void UpdateHealthP1(float health)
    {
        player1Health = health;
    }

    private void UpdateHealthP2(float health)
    {
        player2Health = health;
    }

    public void AddPlayers(GameObject p1, GameObject p2)
    {
        player1 = p1;
        player2 = p2;
    }

}
