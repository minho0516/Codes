using UnityEngine;

namespace RPG.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void CreateFeedback();
        public abstract void FinishFeedback();
    }
}
