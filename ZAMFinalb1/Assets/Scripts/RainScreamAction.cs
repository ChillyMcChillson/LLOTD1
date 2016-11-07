/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Action;
using RAIN.Core;
using System.Collections.Generic;
using UnityEngine;

namespace SetupForFuseCC
{
    [RAINAction]
    public class RainScreamAction : RainAttackAction
    {
        public RainScreamAction()
        {
            actionName = "RainScreamAction";
        }

        protected override string GetMechanimState()
        {
            return "zombie_scream";
        }

        protected override float GetDuration()
        {
            return 2.3f;
        }
    }
}

#endif
