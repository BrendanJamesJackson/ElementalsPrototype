using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{
    [Header("Player 1 Components")]
    public GameObject player1;
    public Image healthBarP1;
    public ParticleSystem healthParticlesP1;
    public Image manaBarP1;
    public Image elementalBarP1;

    [Header("Player 2 Components")]
    public GameObject player2;
    public Image healthBarP2;
    public ParticleSystem healthParticlesP2;
    public Image manaBarP2;
    public Image elementalBarP2;

    [Header("Constants for display")]
    const float maxHealth = 100f;

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
        healthBarP1.fillAmount = (health / maxHealth);
        healthParticlesP1.Play();
    }

    private void UpdateHealthP2(float health)
    {
        healthBarP2.fillAmount = (health / maxHealth);
        healthParticlesP2.Play();
    }

    public void AddPlayers(GameObject p1, GameObject p2)
    {
        player1 = p1;
        player2 = p2;
    }

}
