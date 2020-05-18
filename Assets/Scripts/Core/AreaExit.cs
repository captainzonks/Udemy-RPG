using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class AreaExit : MonoBehaviour
    {

        [SerializeField] string areaToLoad;

        [SerializeField] string areaTransitionName;

        [SerializeField] AreaEntrance theEntrance;

        [SerializeField] float waitToLoad = 1f;
        [SerializeField] bool shouldLoadAfterFade;

        // Start is called before the first frame update
        void Start()
        {
            theEntrance.SetTransitionName(areaTransitionName);
        }

        // Update is called once per frame
        void Update()
        {
            if (!shouldLoadAfterFade) return;
            waitToLoad -= Time.deltaTime;
            if (!(waitToLoad <= 0)) return;
            shouldLoadAfterFade = false;
            SceneManager.LoadScene(areaToLoad);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            shouldLoadAfterFade = true;

            GameManager.instance.ModifyFading(true);

            UIFade.instance.FadeToBlack();

            PlayerController.instance.SetAreaTransitionName(areaTransitionName);
        }
    }
}