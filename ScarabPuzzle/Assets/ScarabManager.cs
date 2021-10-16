using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabManager : MonoBehaviour
{
    [SerializeField] List<ScarabManager> neighbors;
    List<(ScarabManager, bool)> connections;
    bool isChosen;
    bool isConnected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
