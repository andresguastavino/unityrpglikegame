using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsStatsScript : MonoBehaviour
{

    public Transform cam;
    public Slider healthBarSlider;
    public Text healthBarText;
    public Text objectNameText;
    public int maxHealth;
    public int currentHealth;
    public string objectName;
    Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;        
    }

    private void Update()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        healthBarText.text = currentHealth + "/" + maxHealth;
    }

    private void LateUpdate()
    {
        Vector3 lookAtOffset = transform.position + cam.forward;
        Vector3 lookAtPosition = new Vector3(lookAtOffset.x, initialPosition.y, lookAtOffset.z);
        transform.LookAt(lookAtPosition);

        Vector3 adjustedPos = cam.position - transform.position;
        adjustedPos = new Vector3(adjustedPos.x, initialPosition.y, adjustedPos.z).normalized;
        transform.position = initialPosition + adjustedPos;
    }

    public void SetObjectName(string objectName)
    {
        this.objectName = objectName;
        this.objectNameText.text = objectName;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = Mathf.FloorToInt(health);
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.FloorToInt(health);
    }

   

}
