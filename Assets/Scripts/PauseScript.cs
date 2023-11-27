using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] InputAction pauseAction;

    public static bool isPaused = false;

    [SerializeField] GameObject pauseMenuUI;

    private void Start()
    {
        Resume();

        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void TimeScaleReset()
    {
        Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }
}
