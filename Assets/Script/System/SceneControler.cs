using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControler : MonoBehaviour
{
    public bool LoadFromAnother { get { return loadFromAnother; } }

    AsyncOperation async;

    bool loadFromAnother;
    bool loading;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string name)
    {
        CameraControl crt = Camera.main.gameObject.GetComponent<CameraControl>();
        if (crt)
        {
            crt.Fadin();
        }

        StartCoroutine(LoadingAsync(name));
    }

    private IEnumerator LoadingAsync(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loading = true;
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }
        loading = false;
    }

    public void DoChangeScene()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        while (loading)
        {
            yield return new WaitForEndOfFrame();
        }
        async.allowSceneActivation = true;
        loadFromAnother = true;
    }
}
