using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateLives : MonoBehaviour
{
    public Object bat;
    TextMeshProUGUI textMP;
    PlayerMove pm;

    // Start is called before the first frame update
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        pm = bat.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        textMP.text = "Lives: " + pm.lives;
        textMP.text += "\nSpeed: " + pm.maxSpeed.ToString();
    }
}
