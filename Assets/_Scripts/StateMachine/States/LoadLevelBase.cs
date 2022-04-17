using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelBase
{
    protected async UniTask LoadScene(string sceneName)
    {
        var loadingView = Object.Instantiate(
            (LoadingView)await Resources.LoadAsync<LoadingView>("LoadingView"));

        Object.DontDestroyOnLoad(loadingView);

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single)
            .ToUniTask(Progress.CreateOnlyValueChanged(progress => loadingView.UpdateProgress(progress), EqualityComparer<float>.Default));

        loadingView.Stop();
    }
}

