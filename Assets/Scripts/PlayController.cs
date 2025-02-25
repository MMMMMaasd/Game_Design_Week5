using UnityEngine;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    public Canvas canvasToDisable; // Reference to the Canvas you want to disable

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Method to disable the canvas when the button is clicked
    void OnButtonClick()
    {
        if (canvasToDisable != null)
        {
            canvasToDisable.enabled = false; // Disables the Canvas
        }
    }
}
