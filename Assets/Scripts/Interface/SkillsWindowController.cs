using UnityEngine;

public class SkillsWindowController : MonoBehaviour
{
    [SerializeField] private GameObject upgradesWindow;

    public void Open()
    {
        if (upgradesWindow != null)
            upgradesWindow.SetActive(true);
    }

    public void Close()
    {
        if (upgradesWindow != null)
            upgradesWindow.SetActive(false);
    }

    public void Toggle()
    {
        if (upgradesWindow != null)
            upgradesWindow.SetActive(!upgradesWindow.activeSelf);
    }
}
