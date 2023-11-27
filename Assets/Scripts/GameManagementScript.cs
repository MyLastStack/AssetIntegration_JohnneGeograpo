using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagementScript : MonoBehaviour
{
    [SerializeField] GameSceneManager gsc;
    [SerializeField] TextMeshProUGUI text;

    public int skellyCount;

    void Start()
    {
        gsc = gameObject.AddComponent<GameSceneManager>();
    }

    void Update()
    {
        skellyCount = GameObject.FindGameObjectsWithTag("AIControlled").Length;

        text.text = $"Remaining: {skellyCount}";

        if (skellyCount < 1)
        {
            Invoke("gmEndCredit", 3);
        }
    }

    private void gmEndCredit()
    {
        gsc.EndCredit();
    }
}
