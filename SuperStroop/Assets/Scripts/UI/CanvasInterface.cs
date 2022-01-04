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
                if (target.tweenController)
                {
                    target.tweenController.BeginStage(0).onComplete.AddListener(target.Show);
                }
                else
                {
                    target.Show();
                }
                // Set the current interface to be the target
                InterfaceManager.current = target;
            });
        }
        else
        {
            // Hide this canvas
            Hide();
            // Show the target canvas
            target.Show();
            // Set the current interface to be the target
            InterfaceManager.current = target;
        }
    }
    public void Show()
    {
        // First enable the object if it ins't already
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        // Enable canvas
        if (canvas && !canvas.enabled)
        {
            canvas.enabled = true; 
        }
    }
    public void Hide()
    {
        // Disable canvas
        if (canvas && canvas.enabled)
            canvas.enabled = false;
        // If there is no canvas, disable the object itself instead
        else if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}