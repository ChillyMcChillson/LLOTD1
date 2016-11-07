/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using UnityEngine;

namespace SetupForFuseCC
{
    public class RainSetMechanimState
    {
        public static void Set(RainEnemy self, string mechanimParameter)
        {
            if (self)
            {
                Animator animator = self.GetComponent<Animator>();
                if (animator)
                {
                    animator.SetBool(mechanimParameter, true);
                }
            }
        }
    }
}

#endif
