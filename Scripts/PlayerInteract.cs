using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject infoTextComponent;


    public GameObject BookView;
    public GameObject bookTitleComponent;
    public GameObject bookTextComponent;

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
                    string bookTitle = hit.transform.GetComponent<BookInfo>().bookTitle;
                    infoTextComponent.GetComponent<Text>().text = "Read '" + bookTitle + "'";
                    if (Input.GetKeyDown(KeyCode.E)) {
                        BookView.SetActive(!BookView.activeSelf);
                        bookTitleComponent.GetComponent<Text>().text = bookTitle;
                        bookTextComponent.GetComponent<Text>().text = hit.transform.GetComponent<BookInfo>().bookText;
                        PlayerState.Pause();
                    }
                } else {
                    infoTextComponent.GetComponent<Text>().text = "";
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
