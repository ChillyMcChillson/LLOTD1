/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SetupForFuseCC
{
    public class RainDamageBehaviour : MonoBehaviour
    {
        public DateTime _mTimer = DateTime.MinValue;

        private AI _mAI = null;

        private string _mState = string.Empty;

        public void StartDamage(AI ai, RainEnemy self, GameObject target, string state, float damage, float duration)
        {
            _mAI = ai;
            _mState = state;
            _mTimer = DateTime.Now + TimeSpan.FromSeconds(duration);
            target.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }        

        void Update()
        {
            if (_mTimer < DateTime.Now)
            {
                _mAI.WorkingMemory.SetItem<int>(_mState, 2);
                Destroy(this);
            }
        }
    }
}

#endif
