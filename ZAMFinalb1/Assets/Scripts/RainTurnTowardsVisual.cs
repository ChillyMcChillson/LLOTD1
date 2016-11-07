/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Action;
using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace SetupForFuseCC
{
    [RAINAction]
    public class RainTurnTowardsVisual : RAINAction
    {
        public RainTurnTowardsVisual()
        {
            actionName = "RainTurnTowardsVisual";
        }

        public override ActionResult Execute(AI ai)
        {
            if (RainEnemy._sDict.ContainsKey(ai))
            {
                RainEnemy self = RainEnemy._sDict[ai];
                GameObject target = ai.WorkingMemory.GetItem<GameObject>("varTarget");
                if (self &&
                    target)
                {
                    self.transform.LookAt(target.transform.position);
                    return ActionResult.SUCCESS;
                }
            }
            return ActionResult.FAILURE;
        }
    }
}

#endif