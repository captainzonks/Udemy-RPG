using Movement;
using UnityEngine;

namespace Core
{
    public class AreaEntrance : MonoBehaviour
    {
        public string transitionName;

        // Start is called before the first frame update
        private void Start()
        {
            if (transitionName == PlayerController.Instance.areaTransitionName)
            {
                PlayerController.Instance.transform.position = transform.position;
            }

            UIFade.Instance.FadeFromBlack();
            GameManager.Instance.fadingBetweenAreas = false;
        }
    }
}