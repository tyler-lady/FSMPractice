using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using System.Linq;
using UnityStandardAssets.Utility;*/

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> checkpoints = new List<GameObject>();
    public List<GameObject> Checkpoints { get { return checkpoints; } }

    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoint"));

                //instance.checkpoints = instance.checkpoints.OrderBy(WaypointCircuit => WaypointCircuit.name).ToList(); //this line here forces an order out of our list of waypoints. Without this their pattern acan be random, and the patrol will have a random pattern
            }
            
            return instance;
        }
    }

}
