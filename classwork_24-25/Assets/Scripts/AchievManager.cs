using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievManager : MonoBehaviour
{
    private const float _ACHIEV_SHOW_TIME = 5.0f;
    private const float _DELAY_BETWEEN_ACHIEVS = 1.0f;

    [SerializeField] private Text _text;
    [SerializeField] private GameObject _achievementObject;

    [SerializeField] private List<Achievement> _achievements = new List<Achievement>()
    {
        new Achievement("I'm home!", "Get to a safe spot with fences."),
        new Achievement("Who lives here?", "Inspect a strange hut on the outskirt of the map."),
        new Achievement("I'm no tree, I'm an Ent.", "Hobbits? Hmm... Never heard of a hobbit before."),
        new Achievement("Hobbits always surprise you.", "Kill all enemies."),
    };

    private Queue<string> _achievInQueue = new Queue<string>();
    private bool _isShowing = false;
    private bool changed = false;

    public void AchievementDone(string name)
    {
        _achievInQueue.Enqueue(name);

        if (!_isShowing)
            StartCoroutine(PlayAchievs());
    }
    private IEnumerator PlayAchievs()
    {
        _isShowing = true;
        while (_achievInQueue.Count > 0)
        {
            ShowAchiev(_achievInQueue.Dequeue());
            yield return new WaitForSeconds(_ACHIEV_SHOW_TIME);
            _achievementObject.SetActive(false);
            Debug.Log(ToString());
            yield return new WaitForSeconds(_DELAY_BETWEEN_ACHIEVS);
        }
        _isShowing = false;
    }
    private void ShowAchiev(string name)
    {
        Achievement achiev = _achievements.Find(x => x.Name == name);
        achiev.AchievementDone();
        _text.text = achiev.Text;
        _achievementObject.SetActive(true);
    }


    private void Start()
    {
        _achievementObject.SetActive(false);
    }
    public override string ToString()
    {
        string res = "";
        foreach (var achievement in _achievements)
            res += achievement.ToString() + "\n";
        
        return res;
    }
}


public class Achievement
{
    public string Name { get => _name; }
    public string Text { get => _text; }
    public bool IsAchieved { get => _isAchieved; }
    private string _name;
    private string _text;
    private bool _isAchieved = false;
    public Achievement(string name, string text)
    {
        _name = name;
        _text = text;
    }
    ~Achievement() 
    {
    }
    public void AchievementDone()
        => _isAchieved = true;

    public override string ToString()
    {
        return $"name: {_name}, status: {(_isAchieved ? "achieved" : "notAchieved")}";
    }
}
