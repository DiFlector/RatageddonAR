using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Enemy
{
    [InitializeOnLoad]
    public class WayPoints : MonoBehaviour
    {
        public Transform[] transforms;
        [HideInInspector] public int _groupCount = 0;
        public WaypointGroup[] wayPoints;

        static WayPoints()
        {
            EditorApplication.projectChanged += ResetGroup; 
        }
        static void ResetGroup()
        {
            WayPoints wayPoints = FindObjectOfType<WayPoints>();
            if (wayPoints != null)
            {
                var Random = new Random();
                wayPoints._groupCount = Random.Next(0, 100);
            }
        }

        public struct WaypointGroup
        {
            public Transform[] wayPoints;
            public int groupid;
        }

        public void AddTransformsToGroup()
        {
            WaypointGroup[] newWayPoints = new WaypointGroup[_groupCount + 1];
            for (int i = 0; i < _groupCount; i++)
            {
                newWayPoints[i] = wayPoints[i];
            }
            newWayPoints[_groupCount].wayPoints = transforms;
            newWayPoints[_groupCount].groupid = _groupCount;
            _groupCount++;
            wayPoints = newWayPoints;
            PrintArray(newWayPoints);
        }

        public void Test()
        {
            PrintArray(wayPoints);
        }

        public static void PrintArray(WaypointGroup[] array)
        {
            foreach (var item in array)
            {
                print(item.groupid);
                foreach (var x in item.wayPoints)
                {
                    print(x.gameObject.name);
                }
            }
            print("---------------------------------");
        }
    }
}
