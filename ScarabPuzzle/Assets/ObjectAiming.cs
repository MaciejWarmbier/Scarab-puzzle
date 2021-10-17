using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAiming : MonoBehaviour
{
    Camera cameraPointer;
    void Start()
    {
        cameraPointer = GetComponent<Camera>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            ResetObjects();
        }

        if(Input.GetMouseButtonDown(0)){
            ClickOnObject();
        }
    }

    void ResetObjects(){
        RaycastHit[] hits;
        Ray ray = cameraPointer.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider!=null){
                BoardManager boardManager = hit.collider.gameObject.GetComponent<BoardManager>();
                if(boardManager != null){
                    boardManager.ResetBoard();
                }
            }

            
        }
    }

    void ClickOnObject(){
        RaycastHit[] hits;
        Ray ray = cameraPointer.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider!=null){
                IClick click = hit.collider.gameObject.GetComponent<IClick>();
                if(click != null){
                    click.OnClick();
                }
            }

            
        }
        
    }
}
