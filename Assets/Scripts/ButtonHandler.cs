using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

public class ButtonHandler : MonoBehaviour
{
    private GameObject details;
    

    private void Start()
    {
        UniTask.WaitForEndOfFrame();


        details = GameObject.FindWithTag("Details");
        var button = GetComponent<Button>();
        button.OnClickAsObservable()
            .Subscribe(_ => SetGameObjectActiveState())
            .AddTo(this);
    }

    private void SetGameObjectActiveState()
    {
        details.SetActive(!details.activeSelf);
    }
}