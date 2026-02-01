using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnywhereParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem system;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                system.transform.position = hit.point;
                system.Play();
            }
        }
    }
}
