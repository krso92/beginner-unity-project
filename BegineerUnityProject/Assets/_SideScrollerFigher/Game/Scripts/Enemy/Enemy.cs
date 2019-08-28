using UnityEngine;

public interface Enemy
{
    void Attack();

    void Inspect();

    void Move(Vector3 to);
}
