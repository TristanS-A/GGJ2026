using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkybox : MonoBehaviour
{
    public float rotationSpeed = 1f;

    void Update()
    {
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");
        currentRotation += rotationSpeed * Time.deltaTime;
        currentRotation %= 360f;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }
}
