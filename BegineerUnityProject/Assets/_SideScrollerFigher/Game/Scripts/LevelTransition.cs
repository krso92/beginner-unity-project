using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    /*
        Expected to have trigger collider on self
     */
    
    [SerializeField]
    private string goToLevel;
    [SerializeField]
    private string triggerWithTag = "Player";

    // TODO -- set layer matrix
    private void OnTriggerEnter2D(Collider2D other)        
    {
        if (other.tag.Equals(triggerWithTag))
        {
            Debug.Log("Maybe deactivate player input...");
            GameManager.instance.LoadLevel(goToLevel);
        }
    }
}
