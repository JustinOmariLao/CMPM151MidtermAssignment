using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Script : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public TMP_Text killText; // UI for Kill Count

    // Update is called once per frame
    void Update()
    {   
        // Player movement
        var controller = GetComponent<CharacterController>();

        Vector3 hor = transform.right * Input.GetAxis("Horizontal");
        Vector3 vert = transform.forward * Input.GetAxis("Vertical");

        Vector3 movement = hor + vert;
        controller.Move(movement * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, 10000)) {
                if (hit.transform.gameObject.name == "Zombie(Clone)") {
                    Destroy(hit.transform.gameObject);
                    Game_Script.killCounter++;
                    killText.text = "Kill Counter: " + Game_Script.killCounter;
                }
            }
        }
    }
}
