using System;
using System.Collections.Generic;
using UnityEngine;

public class CarsLifecycleResolver : MonoBehaviour
{
    void UpdateRacers(float deltaTimeS, List<IRacer> racers)
    {
        HashSet<IRacer> racersNeedingRemoved = new HashSet<IRacer>();

        // Updates the racers that are alive
        for (int index = 0; index < racers.Count; index++)
        {
            IRacer racer = racers[index];
            if (racer.IsAlive())
            {
                //Racer update takes milliseconds
                racer.Update(deltaTimeS * 1000);
            }
        }

        // Collides
        for (int racerIndex1 = 0; racerIndex1 < racers.Count; racerIndex1++)
        {
            IRacer racer1 = racers[racerIndex1];
            if (!racer1.IsCollidable()) continue;

            for (int racerIndex2 = racerIndex1 + 1; racerIndex2 < racers.Count; racerIndex2++)
            {
                IRacer racer2 = racers[racerIndex2];
                if (!racer1.IsCollidable()) continue;

                if (racer2.CollidesWith(racer1))
                {
                    if (racersNeedingRemoved.Add(racer1))
                        OnRacerExplodes(racer1);
                    if (racersNeedingRemoved.Add(racer2))
                        OnRacerExplodes(racer2);
                }
            }
        }

        foreach (IRacer racer in racersNeedingRemoved)
        {
            racer.Destroy();
            racers.Remove(racer);
        }
    }

    private void OnRacerExplodes(IRacer racer1)
    {
        throw new NotImplementedException();
    }
}
