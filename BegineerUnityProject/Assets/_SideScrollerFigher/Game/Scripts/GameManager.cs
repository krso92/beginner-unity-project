using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject playerPrefab;
    private Vector3 spawnPosition;

    // Unity msgs

    private void Awake()
    {
        instance = this;
        spawnPosition = GameObject.Find("SpawnPosition").transform.position;        
    }

    void Start()
    {
        // TODO -- here is place to check for player skin, current progress and stuff like that
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    // Use this to move trough game

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
