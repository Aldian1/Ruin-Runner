using UnityEngine;
using System.Collections;

public class SetRendererLayer : MonoBehaviour {

    public TrailRenderer trail;

    // Use this for initialization
    void Start()
    {
        trail = GetComponent<TrailRenderer>();

        trail.sortingOrder = 5;

    }
}
