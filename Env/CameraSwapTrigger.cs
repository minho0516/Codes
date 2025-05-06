using RPG.EventSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace RPG
{
    public class CameraSwapTrigger : MonoBehaviour
    {
        public CinemachineCamera leftCamera;
        public CinemachineCamera rightCamera;

        [SerializeField] private GameEventChannelSO _cameraChannel;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Vector2 exitDirection = (collision.transform.position - transform.position).normalized;

                if(leftCamera != null && rightCamera  != null)
                {
                    var evt = CameraEvents.SwapCameraEvent;
                    evt.leftCamera = leftCamera;
                    evt.rightCamera = rightCamera;
                    evt.direction = exitDirection;

                    _cameraChannel.RaiseEvent(evt);
                }
            }
        }
    }
}
