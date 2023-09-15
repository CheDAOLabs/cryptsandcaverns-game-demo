

using UnityEngine;
using System.Collections;
using MoreMountains.TopDownEngine;
using UnityEngine.SceneManagement;
public class ExitCollisionTrigger : 
    ButtonActivated
{
    [Header("Finish Level")]
    /// the exact name of the level to transition to 
    [Tooltip("the exact name of the level to transition to ")]
    public string LevelName;

    /// <summary>
    /// When the button is pressed we start the dialogue
    /// </summary>
    public override void TriggerButtonAction()
    {
        if (!CheckNumberOfUses())
        {
            return;
        }
        base.TriggerButtonAction ();
        Debug.Log("Trigger Enter: ");
        Application.ExternalCall("window.redirectToNextId()");
        
    }

    public void Test()
    {
        Debug.Log("Trigger Enter: ");
        Application.ExternalCall("window.redirectToNextId()");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当触发器与其他碰撞体发生触发碰撞时调用
        Debug.Log("Trigger Enter: " + other.gameObject.name);
        Application.ExternalCall("window.redirectToNextId()");

        // 在这里添加你的逻辑处理代码
    }

    private void OnTriggerExit(Collider other)
    {
        // 当触发器与其他碰撞体结束触发碰撞时调用
        Debug.Log("Trigger Exit: " + other.gameObject.name);
        
        // 在这里添加你的逻辑处理代码
    }
}