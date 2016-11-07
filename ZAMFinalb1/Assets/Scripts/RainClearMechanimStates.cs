/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using UnityEngine;

namespace SetupForFuseCC
{
    public class RainClearMechanimStates
    {
        public static void Clear(RainEnemy self)
        {
            if (self)
            {
                Animator animator = self.GetComponent<Animator>();
                if (animator)
                {
                    for (int i = 0; i < animator.parameterCount; ++i)
                    {
                        string param = animator.GetParameter(i).name;
                        animator.SetBool(param, false);
                    }
                }
            }
        }
    }
}

#endif
