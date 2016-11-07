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
    public class RainNeckAction : RainAttackAction
    {
        public RainNeckAction()
        {
            actionName = "RainNeckAction";
        }

        protected override string GetMechanimState()
        {
            return "zombie_neck_bite";
        }

        protected override float GetDuration()
        {
            return 3.8f;
        }
    }
}

#endif
