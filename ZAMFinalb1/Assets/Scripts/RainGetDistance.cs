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
    public class RainGetDistance : RAINAction
    {
        public RainGetDistance()
        {
            actionName = "RainGetDistance";
        }

        public override ActionResult Execute(AI ai)
        {
            if (!RainEnemy._sDict.ContainsKey(ai))
            {
                Debug.LogError("Orphaned Enemy!");
                return ActionResult.FAILURE;
            }
            RainEnemy enemy = RainEnemy._sDict[ai];
            if (null == enemy)
            {
                Debug.LogError("Enemy is null!");
                return ActionResult.FAILURE;
            }
            GameObject target = ai.WorkingMemory.GetItem<GameObject>("varTarget");
            if (target)
            {
                ai.WorkingMemory.SetItem<float>("distance", Vector3.Distance(enemy.transform.position, target.transform.position));
                return ActionResult.SUCCESS;
            }
            else
            {
                return ActionResult.FAILURE;
            }
        }
    }
}

#endif
