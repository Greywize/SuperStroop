using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInterface : MonoBehaviour
{
    Canvas canvas;
    TweenController tweenController;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        tweenController = GetComponent<TweenController>();
    }
    // Navigates the UI to the target CanvasInterface
    public void Navigate(CanvasInterface target)
    {
        if (tweenController)
        {
            tweenController.BeginStage(1).onComplete.AddListener(() => 
            {
                Hide();
                target.tweenController.BeginStage(0).onComplete.AddListener(target.Show);
            });
        }
        else
        {
            // Hide this canvas
            Hide();
            // Show the target canvas
            target.Show();
        }
    }
    public void Show()
    {
        // Enable canvas
        if (!canvas.enabled)
            canvas.enabled = true;
    }
    public void Hide()
    {
        // Disable canvas
        if (canvas.enabled)
            canvas.enabled = false;
    }
}