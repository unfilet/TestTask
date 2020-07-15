using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRacer
{
    bool IsAlive();
    void Update(float deltaTime);

    bool IsCollidable();
    bool CollidesWith(IRacer racer);
    void Destroy();
}
