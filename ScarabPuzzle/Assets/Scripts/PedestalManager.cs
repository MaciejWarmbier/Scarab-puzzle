using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalManager : MonoBehaviour, IClick
{
    [SerializeField] GameObject panel;
    [SerializeField] float timeOfDisplay= 3f;

    void Start()
    {
        StartCoroutine(DisplayInstruction());
    }

    public void OnClick(){
        StartCoroutine(DisplayInstruction());
    }

    IEnumerator DisplayInstruction(){
        panel.SetActive(true);
        yield return new WaitForSeconds(timeOfDisplay);

        panel.SetActive(false);
    }
}
