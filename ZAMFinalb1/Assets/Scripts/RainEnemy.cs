/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Core;
using System.Collections.Generic;
using UnityEngine;

namespace SetupForFuseCC
{
    public class RainEnemy : MonoBehaviour
    {
        public static Dictionary<AI, RainEnemy> _sDict = new Dictionary<AI, RainEnemy>();

        /// <summary>
        /// The move speed in which the character moves
        /// </summary>
        public float _mMoveSpeed = 1f;

        /// <summary>
        /// Is the character running
        /// </summary>
        public bool _mIsRunning = true;

        /// <summary>
        /// Is the character standing
        /// </summary>
        public bool _mIsStanding = true;

        public AIRig _mAI = null;

        // Use this for initialization
        void Awake()
        {
            _mAI = GetComponentInChildren<AIRig>();
            _sDict[_mAI.AI] = this;
        }

        void Start()
        {
            if (_mAI)
            {
                _mAI.AI.WorkingMemory.SetItem("moveSpeed", _mMoveSpeed);
                _mAI.AI.WorkingMemory.SetItem("isRunning", _mIsRunning);
                _mAI.AI.WorkingMemory.SetItem("isStanding", _mIsStanding);
            }
        }

        void OnDestroy()
        {
            if (_sDict.ContainsKey(_mAI.AI))
            {
                _sDict.Remove(_mAI.AI);
            }
        }
    }
}

#endif
