using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCameraScript : MonoBehaviour
{

    public float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HelloClem");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Skybox>().material.SetFloat("_Rotation", Time.time * speedMultiplier); 
    }
}
