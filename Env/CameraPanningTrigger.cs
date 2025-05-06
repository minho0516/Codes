using RPG.EventSystem;
using RPG.Players;
using UnityEngine;

namespace RPG
{
    public class CameraPanningTrigger : MonoBehaviour
    {
        public PanDirection panDirection;
        public float panDistance;
        public float panTime = 0.35f;

        [SerializeField] private GameEventChannelSO _cameraChannel;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                SendPanningEvent(false);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                SendPanningEvent(true);
            }
        }

        public void SendPanningEvent(bool isRewing)
        {
            var evt = CameraEvents.panEvent;
            evt.penTime = panTime;
            evt.direction = panDirection;
            evt.distance = panDistance;
            evt.isRewindToStart = isRewing;

            _cameraChannel.RaiseEvent(evt);
        }
    }
}
