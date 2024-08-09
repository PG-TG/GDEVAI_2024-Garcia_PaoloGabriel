using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public sealed class World : MonoBehaviour {
    private static readonly World instance = new World();
    private static GameObject[] hidingSpots;

    static World() {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() {}

    public static World Instance {
        get { return instance; }
    }

    public GameObject[] GetHidingSpots() {
        return hidingSpots;
    }
}
