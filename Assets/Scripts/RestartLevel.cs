using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private Snake _snake;
    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdated += RestartLeve;
    }

    private void OnDisable()
    {
        _snake.SizeUpdated += RestartLeve;
    }

    private void RestartLeve(int size)
    {
        if (size <= 0)
        {
            StartCoroutine(IRestartLevel());
        }
    }

    private IEnumerator IRestartLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
