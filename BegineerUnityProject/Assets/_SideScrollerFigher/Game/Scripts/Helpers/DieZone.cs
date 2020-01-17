using UnityEngine;
using UnityEngine.SceneManagement;

public class DieZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
