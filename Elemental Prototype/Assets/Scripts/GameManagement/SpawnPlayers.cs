using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayers : MonoBehaviour
{
    public GameManager GameManager;
    public GameManagerUI GameUI;

    public PlayerInputManager InputManager;
    public CinemachineTargetGroup TargetGroup;

    public GameObject prefab1;
    public GameObject prefab2;

    public Transform[] spawnPoints;


    private void Awake()
    {
        Spawn(prefab1 , prefab2);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(GameObject player1Prefab, GameObject player2Prefab)
    {
        /*InputManager.playerPrefab = player1Prefab;
        InputManager.JoinPlayer(0, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
        InputManager.playerPrefab = player2Prefab;
        InputManager.JoinPlayer(1, controlScheme: "Keyboard&Mouse", pairWithDevice: Keyboard.current);*/

        PlayerInput player1 = PlayerInput.Instantiate(
            prefab: player1Prefab,
            playerIndex: 0
            /*controlScheme: "Gamepad",
            pairWithDevice: Gamepad.all[0]*/
        );

        PlayerInput player2 = PlayerInput.Instantiate(
            prefab: player2Prefab,
            playerIndex: 1,
            controlScheme: "Keyboard&Mouse",
            pairWithDevice: Keyboard.current
        );

        player1.transform.position = spawnPoints[0].transform.position;
        player2.transform.position = spawnPoints[1].transform.position;

        TargetGroup.AddMember(player1.transform.Find("CameraTargetAnchor"),1,2.8f);
        TargetGroup.AddMember(player2.transform.Find("CameraTargetAnchor"), 1, 2.8f);

        GameManager.AddPlayers(player1.gameObject, player2.gameObject);
        GameUI.AddPlayers(player1.gameObject, player2.gameObject);
        GameManager.enabled = true;
        GameUI.enabled = true;
    }


}