using UnityEngine;

public class Achievement_2 : MonoBehaviour
{
    [SerializeField] private string _name = "I'm no tree, I'm an Ent.";
    [SerializeField] private AchievManager _achievManager;

    private bool _stay = false;
    private float _timeToStay = 2.0f;
    private float _timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            _stay = true;
    }
    private void FixedUpdate()
    {
        if (_stay)
        {
            if (_timer < _timeToStay)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _achievManager.AchievementDone(_name);
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _stay = false;
            _timer = 0.0f;
        }
    }
}
