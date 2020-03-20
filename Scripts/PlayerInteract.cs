using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject InfoText;
    public GameObject BookView;

    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.paused) {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(Vector3.forward), out hit, 5)){
                Debug.DrawRay(mainCamera.position, mainCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.tag == "Interactive")
                {
                    InfoText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E)) {
                        BookView.SetActive(!BookView.activeSelf);
                        PlayerState.Pause();
                    }
                } else {
                    InfoText.SetActive(false);
                }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.E)) {
                BookView.SetActive(!BookView.activeSelf);
                PlayerState.Pause();
            }
        }
    }
}
