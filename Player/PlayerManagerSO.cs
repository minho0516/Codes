using UnityEngine;

namespace RPG.Players
{
    [CreateAssetMenu(fileName = "PlayerManagerSO", menuName = "SO/PlayerManagerSO")]
    public class PlayerManagerSO : ScriptableObject
    {
        private Player _player;

        public Player Player
        {
            get
            {
                if(_player == null )
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    Debug.Assert(_player != null, "No Player Found");
                }

                return _player;
            }
            set
            {
                _player = value;
            }

        }
        public Transform PlayerTrm => Player.transform;
    }
}
