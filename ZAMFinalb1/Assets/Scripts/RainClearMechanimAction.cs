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
    public class RainClearMechanimAction : RAINAction
    {
        public RainClearMechanimAction()
        {
            actionName = "RainClearMechanimAction";
        }

        public override ActionResult Execute(AI ai)
        {
            if (!RainEnemy._sDict.ContainsKey(ai))
            {
                return ActionResult.SUCCESS;
            }
            RainEnemy self = RainEnemy._sDict[ai];
            RainClearMechanimStates.Clear(self);
            return ActionResult.SUCCESS;
        }
    }
}

#endif
