using Quest;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LoadingScene : MonoBehaviour
    {
        public float waitToLoad;

        private void Update()
        {
            if (!(waitToLoad > 0)) return;
            waitToLoad -= Time.deltaTime;
            if (!(waitToLoad <= 0)) return;

            GameManager.Instance.LoadData();
            QuestManager.Instance.LoadQuestData();
        }
    }
}
