using Movement;
using UnityEngine;

namespace Core
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] string transitionName;

        void Start()
        {
            if (transitionName == PlayerController.instance.GetAreaTransitionName())
            {
                PlayerController.instance.transform.position = transform.position;
            }

            UIFade.instance.FadeFromBlack();
            GameManager.instance.ModifyFading(false);
        }

        public string GetTransitionName()
        {
            return transitionName;
        }

        public void SetTransitionName(string newTransitionName)
        {
            transitionName = newTransitionName;
        }
    }
}