using RPG.EventSystem;
using UnityEngine;

namespace RPG.SaveSystem
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _uiEventChannel;

        private void Start()
        {
            FadeEvent evt = new FadeEvent();
            evt.isFadeIn = true;
            evt.fadeTime = 0.5f;

            _uiEventChannel.RaiseEvent(evt);
        }
    }
}
