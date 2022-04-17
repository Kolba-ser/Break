using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoadingView : MonoBehaviour 
{
    [SerializeField] private Slider progressBar;

    [SerializeField] private Animator animator;

    private bool isVisible;

    public IEnumerator Show()
    {
        while (!isVisible)
            yield return null;
    }

    public void Stop()
    {
        animator.SetTrigger("Hide");
    }

    public void UpdateProgress(float progress)
    {
        if (progressBar == null)
            return;

        progressBar.value = progress;
    }

    public void ShowCompleted()
    {
        isVisible = true;
    }

    public void HideCompleted()
    {
        Destroy(gameObject);
    }
}

