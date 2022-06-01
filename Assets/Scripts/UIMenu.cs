using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _panel2;
    [SerializeField] private GameObject _panel3;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private CinemachineVirtualCamera _camera;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        _panel.SetActive(true);
        _audio.Pause();
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _panel.SetActive(false);
        _audio.Play();
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            _camera.Priority = 5;
            _panel2.SetActive(true);
            Invoke("EndGame", 5f);
        }
        if (other.gameObject.CompareTag("FallCheck"))
        {
            _panel3.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void EndGame()
    {
        Time.timeScale = 0;
    }
}