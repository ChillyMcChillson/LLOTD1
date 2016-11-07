/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Action;

namespace SetupForFuseCC
{
    [RAINAction]
    public class RainBiteAction : RainAttackAction
    {
        public RainBiteAction()
        {
            actionName = "RainBiteAction";
        }

        protected override string GetMechanimState()
        {
            return "zombie_biting_1";
        }

        protected override float GetDuration()
        {
            return 2f;
        }
    }
}

#endif
