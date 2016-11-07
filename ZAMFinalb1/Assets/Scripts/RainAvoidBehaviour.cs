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
    public class RainAvoidBehaviour : MonoBehaviour
    {
        public DateTime _mTimer = DateTime.MinValue;
        private float _mAvoidTime = 0f;
        private Vector3 _mPos = Vector3.zero;
        private Vector3 _mDir = Vector3.zero;
        private Vector3 _mAvoid = Vector3.zero;

        public void StartAvoidance(float seconds)
        {
            if (seconds < 0.0f)
            {
                enabled = false;
                Debug.LogError("Can't avoid for zero seconds!");
                return;
            }

            _mAvoidTime = seconds;
            _mDir = transform.forward;
            _mPos = transform.position;
            FindAvoidPoint();
            _mTimer = DateTime.Now + TimeSpan.FromSeconds(seconds);
        }

        private void FindAvoidPoint()
        {
            // allowed room for enemy
            const float radius = 0.5f;
            const float movement = 0.5f;
            const float padding = 0.5f;
            const int defaultMask = 1;
            int enemyMask = 1 << LayerMask.NameToLayer("Enemy");
            int mask = defaultMask | enemyMask;

            Vector3 castPos = transform.position + Vector3.up * 0.25f;

            Vector3[] directions =
            {
                transform.forward, //forward
                (transform.forward + transform.right).normalized, //forward right
                (transform.forward - transform.right).normalized, //forward left
                transform.right, //right
                -transform.right, //left
                (-transform.forward + transform.right).normalized, //backward right
                (-transform.forward - transform.right).normalized, //backward left
                -transform.forward, //backward
            };

            Ray ray;
            RaycastHit[] hits;
            foreach (Vector3 direction in directions)
            {
                ray = new Ray(castPos + direction * movement, direction);
                //hits = Physics.RaycastAll(ray, padding, mask);
                //hits = Physics.SphereCastAll(ray, movement, padding, mask);
                hits = Physics.CapsuleCastAll(transform.position + direction * radius, castPos + direction * radius, radius, direction, padding, mask);
                if (hits.Length == 0 ||
                    (hits.Length == 1 &&
                    hits[0].collider.gameObject == gameObject))
                {
                    //Debug.DrawLine(ray.origin, ray.origin + direction, Color.yellow);
                    _mDir = direction;
                    _mAvoid = _mPos + _mDir * movement;
                    return;
                }
                /*
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + direction, Color.red);
                }
                */
            }

            // move away from anything too close
            ray = new Ray(transform.position, transform.forward);
            hits = Physics.SphereCastAll(ray, radius, padding, mask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, hit.collider.transform.position);
                if (distance < 0.1f)
                {
                    _mDir = transform.forward;
                    _mAvoid = _mPos + _mDir * movement;
                    return;
                }
                else if (distance < 1f)
                {
                    _mDir = (transform.position - hit.collider.transform.position).normalized;
                    _mAvoid = _mPos + _mDir * movement;
                    return;
                }
            }

            // stay
            enabled = false;
        }

        public void RemoveBehaviour()
        {
            Destroy(this);
        }

        void Update()
        {
            if (_mTimer < DateTime.Now)
            {
                enabled = false;
                return;
            }

            MoveToAvoidPoint();
        }

        void MoveToAvoidPoint()
        {
            float elapsed = (float)(_mTimer - DateTime.Now).TotalSeconds;
            float t = (_mAvoidTime - elapsed) / _mAvoidTime;

            Vector3 newPos = Vector3.Lerp(_mPos, _mAvoid, t);
            transform.position = newPos;
            transform.forward = Vector3.Lerp(transform.forward, _mDir, Time.deltaTime);

            //Debug.DrawLine(_mPos, newPos, Color.red);
            //Debug.DrawLine(newPos, _mAvoid, Color.green);
        }
    }
}

#endif
