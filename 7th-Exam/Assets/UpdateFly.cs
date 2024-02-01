using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateFly : MonoBehaviour
{
    public Object bat;
    TextMeshProUGUI textMP;
    PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        pm = bat.GetComponent<PlayerMove>();
        textMP.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.canFly || pm.invincible)
        {
            string text = "";
            if (pm.canFly)
                text = "FLY " + pm.flyTimer.ToString("F2") + " seconds\n";
            if (pm.invincible)
                text += "Invincible " + pm.invTime.ToString("F2") + " seconds";

            textMP.text = text;

        }
        else
            textMP.text = "";
    }
}
