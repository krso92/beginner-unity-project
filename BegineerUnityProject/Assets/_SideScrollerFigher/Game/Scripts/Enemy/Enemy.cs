using UnityEngine;

public interface Enemy
{
    /*
        This is 2d enemy
     */
    void Attack();

    void Inspect();

    void Move(float move);
}
