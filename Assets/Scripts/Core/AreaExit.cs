using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class AreaExit : MonoBehaviour
    {

        [SerializeField] private string areaToLoad;

        [SerializeField] private string areaTransitionName;

        [SerializeField] private AreaEntrance theEntrance;

        [SerializeField] private float waitToLoad = 1f;
        [SerializeField] private bool shouldLoadAfterFade;

        private void Start()
        {
            theEntrance.TransitionName = areaTransitionName;
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

            GameManager.Instance.ModifyFading(true);

            UIFade.instance.FadeToBlack();

            PlayerController.Instance.AreaTransitionName = areaTransitionName;
        }
    }
}