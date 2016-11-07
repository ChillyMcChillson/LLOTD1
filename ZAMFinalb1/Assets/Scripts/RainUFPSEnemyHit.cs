/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI && UFPS

using RAIN.Core;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SetupForFuseCC
{
    public class RainUFPSEnemyHit : vp_DamageHandler
    {
        public AIRig _mAI = null;

        public AudioClip ImpactSound = null;

        void Start()
        {
            _mAI = GetComponentInChildren<AIRig>();
            if (_mAI)
            {
                _mAI.AI.WorkingMemory.SetItem<bool>("isStanding", true);
                _mAI.AI.WorkingMemory.SetItem<bool>("isRunning", true);
                _mAI.AI.WorkingMemory.SetItem<float>("moveSpeed", 4f);
            }

            Animator animator = GetComponent<Animator>();
            if (animator)
            {
                animator.SetBool("zombie_run_inPlace", true);
                animator.CrossFade("zombie_run_inPlace", 0, 0, Random.Range(0f, 1f));
            }
        }

        private void PlayImpactSound()
        {
            ///*
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource &&
                ImpactSound)
            {
                audioSource.PlayOneShot(ImpactSound);
            }
            //*/
            /*
            if (ImpactSound)
            {
                AudioSource.PlayClipAtPoint(ImpactSound, transform.position);
            }
            */
        }

        public override void Damage(vp_DamageInfo damageInfo)
        {
            base.Damage(damageInfo);

            PlayImpactSound();

            if (_mAI)
            {
                float t = Mathf.InverseLerp(0, MaxHealth, CurrentHealth);
                if (t < 0.6f)
                {
                    _mAI.AI.WorkingMemory.SetItem<bool>("isRunning", false);
                    _mAI.AI.WorkingMemory.SetItem<float>("moveSpeed", 2f);
                }
                if (t < 0.3f)
                {
                    _mAI.AI.WorkingMemory.SetItem<bool>("isStanding", false);
                    _mAI.AI.WorkingMemory.SetItem<float>("moveSpeed", 3f);
                }
            }
        }

        public override void Die()
        {
            Collider collider = gameObject.GetComponent<Collider>();
            if (collider)
            {
                collider.enabled = false;
            }

            Animator animator = gameObject.GetComponent<Animator>();
            if (animator)
            {
                //Debug.Log("Animator found! Parameters="+ animator.parameterCount);
                for (int p = 0; p < animator.parameterCount; ++p)
                {
                    AnimatorControllerParameter parameter = animator.GetParameter(p);
                    animator.SetBool(parameter.name, parameter.name == "zombie_death");
                    //Debug.Log(parameter.name);
                }
            }

            AIRig aiRig = gameObject.GetComponentInChildren<AIRig>();
            if (aiRig)
            {
                aiRig.enabled = false;
                StartCoroutine(DestroyRoutine(5f));
            }
            else
            {
                StartCoroutine(DestroyRoutine(1f));
            }
        }

        private void PlayDeathSound()
        {
            ///*
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource &&
                DeathSound)
            {
                audioSource.PlayOneShot(DeathSound);
            }
            //*/
            /*
            if (DeathSound)
            {
                AudioSource.PlayClipAtPoint(DeathSound, transform.position);
            }
            */
        }

        public IEnumerator DestroyRoutine(float waitSeconds)
        {
            PlayDeathSound();
            yield return new WaitForSeconds(waitSeconds);
            base.Die();
        }
    }
}

#endif
