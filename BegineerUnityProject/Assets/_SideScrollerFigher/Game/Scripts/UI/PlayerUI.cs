using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text health;
    
    private Custom2dCharacter player;

    public Custom2dCharacter Player
    {
        get => player;
        set => player = value;
    }

    public void UpdateHud()
    {
        health.text = player.CurrentHealth.ToString();
    }

    private void Update()
    {
        // this can be updated only when property changed
        UpdateHud();
    }
}
