using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class AreaExit : MonoBehaviour
    {

        public string areaToLoad;

        public string areaTransitionName;

        public AreaEntrance theEntrance;

        public float waitToLoad = 1f;
        private bool shouldLoadAfterFade;

        private void Start()
        {
            theEntrance.transitionName = areaTransitionName;
        }

        private void Update()
        {
            if (!shouldLoadAfterFade) return;
            waitToLoad -= Time.deltaTime;
            if (!(waitToLoad <= 0)) return;
            shouldLoadAfterFade = false;
            SceneManager.LoadScene(areaToLoad);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            shouldLoadAfterFade = true;

            GameManager.instance.fadingBetweenAreas = true;

            UIFade.Instance.FadeToBlack();

            PlayerController.Instance.areaTransitionName = areaTransitionName;
        }
    }
}