using UnityEngine;

public class Achievement_0 : MonoBehaviour
{
    [SerializeField] private string _name = "I'm home!";
    [SerializeField] private AchievManager _achievManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _achievManager.AchievementDone(_name);
            Destroy(this.gameObject);
        }
    }
}
