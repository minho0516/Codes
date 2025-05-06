using RPG.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Enviroment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string _nextSceneName;
        [SerializeField] private GameEventChannelSO _uiEventChannel;

        private bool _isTriggered = false;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isTriggered) return;

            if(collision.CompareTag("Player"))
            {
                //플레이어한테 이동정지 명령 내려야한다

                FadeEvent evt = UIEvents.FadeEvent;
                evt.isFadeIn = false;
                evt.fadeTime = 0.5f;

                _uiEventChannel.AddListner<FadeCompleteEvent>(HandleFadeComplete);
                _uiEventChannel.RaiseEvent(evt);
            }
        }

        private void HandleFadeComplete(FadeCompleteEvent evt)
        {
            _uiEventChannel.RemoveListner<FadeCompleteEvent>(HandleFadeComplete);
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
