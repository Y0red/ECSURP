using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(delegate { Debug.Log("Hellow from the other side"); });
    }

}
