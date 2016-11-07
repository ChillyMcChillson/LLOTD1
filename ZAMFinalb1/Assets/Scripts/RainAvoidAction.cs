/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Action;
using RAIN.Core;
using UnityEngine;

namespace SetupForFuseCC
{
    [RAINAction]
    public class RainAvoidAction : RAINAction
    {
        public RainAvoidAction()
        {
            actionName = "RainAvoidAction";
        }

        const float AVOID_TIME = 0.5f;

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
            RainAvoidBehaviour behaviour = enemy.GetComponent<RainAvoidBehaviour>();
            if (null == behaviour)
            {
                behaviour = enemy.gameObject.AddComponent<RainAvoidBehaviour>();
                behaviour.StartAvoidance(AVOID_TIME);
                return ActionResult.RUNNING;
            }
            else
            {
                if (behaviour.enabled)
                {
                    return ActionResult.RUNNING;
                }
                else
                {
                    behaviour.RemoveBehaviour();
                    return ActionResult.SUCCESS;
                }
            }
        }
    }
}

#endif
