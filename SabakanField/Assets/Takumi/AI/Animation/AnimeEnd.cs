using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().fireEvents = false;
    }

}
