using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceObject : MonoBehaviour
{
    private static bool StartedOnScene0;
    private static ServiceObject instance;
    async void Start()
    {
        if(!StartedOnScene0)
            StartedOnScene0 = SceneManager.GetActiveScene().buildIndex == 0;
        if (!StartedOnScene0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        StartedOnScene0 = true;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        await ServiceController.InitializeServiceController();
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        ServiceController.UpdateServices();
    }
}
