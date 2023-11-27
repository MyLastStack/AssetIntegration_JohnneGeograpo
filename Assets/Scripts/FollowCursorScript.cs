using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowCursorScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        myText.transform.position = mousePos;
    }
}
