using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Rain").GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = false;
        GameObject.Find("filtre 1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("filtre 2").GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
