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
    public class RainAttackAction : RAINAction
    {
        public RainAttackAction()
        {
            actionName = "RainAttackAction";
        }

        protected virtual string GetMechanimState()
        {
            return "zombie_attack";
        }

        protected virtual float GetDuration()
        {
            return 2.1f;
        }

        public override ActionResult Execute(AI ai)
        {
            if (!RainEnemy._sDict.ContainsKey(ai))
            {
                return ActionResult.SUCCESS;
            }

            RainEnemy self = RainEnemy._sDict[ai];
            if (null == self)
            {
                return ActionResult.SUCCESS;
            }

            RainSetMechanimState.Set(self, GetMechanimState());

            GameObject target;
            int damageState = ai.WorkingMemory.GetItem<int>("damageState");
            switch (damageState)
            {
                case 0:
                    target = ai.WorkingMemory.GetItem<GameObject>("varTarget");
                    if (target)
                    {
                        RainDamageBehaviour behaviour = target.gameObject.AddComponent<RainDamageBehaviour>();
                        behaviour.StartDamage(ai, self, target, "damageState", 0.1f, GetDuration());
                        ai.WorkingMemory.SetItem<int>("damageState", 1);
                        return ActionResult.RUNNING;
                    }
                    else
                    {
                        ai.WorkingMemory.SetItem<int>("damageState", 0);
                        return ActionResult.SUCCESS;
                    }
                case 1:
                    return ActionResult.RUNNING;
                case 2:
                    ai.WorkingMemory.SetItem<int>("damageState", 0);
                    return ActionResult.SUCCESS;
                default:
                    return ActionResult.SUCCESS;

            }
        }
    }
}

#endif
