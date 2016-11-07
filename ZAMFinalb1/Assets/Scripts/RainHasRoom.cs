/*
 * Author:  Tim Graupmann
 * TAGENIGMA LLC, @copyright 2015-2016  All rights reserved.
 *
*/

#if RAIN_AI

using RAIN.Action;
using RAIN.Core;
using System;
using UnityEngine;

namespace SetupForFuseCC
{
    [RAINAction]
    public class RainHasRoom : RAINAction
    {
        public RainHasRoom()
        {
            actionName = "RainHasRoom";
        }

        public override ActionResult Execute(AI ai)
        {
            if (RainEnemy._sDict.ContainsKey(ai))
            {
                RainEnemy self = RainEnemy._sDict[ai];

                Ray ray = new Ray(self.transform.position, self.transform.forward);

                // allowed room for enemy
                const float radius = 0.5f;
                const float padding = 0.5f;
                const int defaultMask = 1;
                int enemyMask = 1 << LayerMask.NameToLayer("Enemy");
                int mask = defaultMask | enemyMask;

                // Cast a sphere wrapping character controller 10 meters forward
                // to see if it is about to hit anything.
                RaycastHit[] hits = Physics.SphereCastAll(ray, radius, padding, mask);

                foreach (RaycastHit hit in hits)
                {
                    GameObject go = hit.collider.gameObject;
                    if (null == go)
                    {
                        continue;
                    }
                    if (go == self)
                    {
                        continue;
                    }

                    //Debug.DrawLine(self.transform.position + Vector3.up, go.transform.position + Vector3.up, Color.green);

                    if (go.transform.position == self.transform.position)
                    {
                        //Debug.Log(string.Format("self={0} ({1}) vs={2} ({3})", self.name, self.GetInstanceID(), go.name, go.GetInstanceID()));
                        continue;
                    }

                    ///*

                    Vector3 dir = (go.transform.position - self.transform.position).normalized;
                    if (dir == Vector3.zero)
                    {
                        // add a reference to the gameObject AI variable so it can be used in custom actions
                        //ai.WorkingMemory.SetItem("varHasRoom", false, typeof(bool));

                        //return ActionResult.SUCCESS; // if facing toward enemy and too close, stop

                        continue;
                    }

                    const float los = 0f;
                    if (Vector3.Dot(dir, self.transform.forward) >= los) //other enemy is in front of you
                    {
                        Vector3 point1 = self.transform.position + Vector3.up * 0.1f; //raise slightly to be visible above floor
                        Vector3 point2 = self.transform.position + dir + Vector3.up * 0.1f; //raise slightly to be visible above floor

                        /*
                        Debug.DrawLine(point1, point2, Color.blue);
                        Debug.DrawLine(point1, point1 + Vector3.Lerp(-self.transform.right, self.transform.forward, los) * padding, Color.green);
                        Debug.DrawLine(point1, point1 + self.transform.forward * padding, Color.yellow);
                        Debug.DrawLine(point1, point1 + Vector3.Lerp(self.transform.right, self.transform.forward, los) * padding, Color.green);
                        Debug.DrawLine(point1, point2, Color.red);
                        */

                        // add a reference to the gameObject AI variable so it can be used in custom actions
                        ai.WorkingMemory.SetItem("varHasRoom", false, typeof(bool));

                        return ActionResult.SUCCESS; // if facing toward enemy and too close, stop
                    }

                    //*/
                }

                ai.WorkingMemory.SetItem("varHasRoom", true, typeof(bool));
                return ActionResult.SUCCESS;
            }
            else
            {
                return ActionResult.SUCCESS;
            }
        }
    }
}

#endif
