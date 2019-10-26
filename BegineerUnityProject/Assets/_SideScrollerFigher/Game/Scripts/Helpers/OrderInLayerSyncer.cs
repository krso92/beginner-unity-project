using System;
using UnityEngine;

public class OrderInLayerSyncer : MonoBehaviour
{
    [SerializeField]
    private string sortingLayerToBe;
    
    private void Awake()
    {
        var els = GetComponentsInChildren<SpriteRenderer>();
        foreach(var el in els)
        {
            el.sortingLayerName = sortingLayerToBe;
        }
    }

    public static void SetOrderInLayer(GameObject front, GameObject back)
    {
        throw new NotImplementedException();
    }
}
