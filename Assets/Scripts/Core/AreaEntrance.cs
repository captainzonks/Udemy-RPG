using Movement;
using UnityEngine;

namespace Core
{
    public class AreaEntrance : MonoBehaviour
    {
        public string TransitionName { set; get; }

        private void Start()
        {
            if (TransitionName == PlayerController.Instance.AreaTransitionName)
            {
                PlayerController.Instance.transform.position = transform.position;
            }

            UIFade.instance.FadeFromBlack();
            GameManager.Instance.ModifyFading(false);
        }
    }
}