using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;

    public int DamageAmount => damageAmount;

    /*
        This script will work same if implemented like this:


        public int DamageAmount;


     */
}
