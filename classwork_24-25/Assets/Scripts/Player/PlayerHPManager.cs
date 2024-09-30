public class PlayerHPManager
{
    private UnityEngine.GameObject[] _hearts;
    private int _hp;
    public PlayerHPManager(UnityEngine.GameObject[] hearts)
    {
        _hearts = hearts;
        _hp = hearts.Length;
    }

    public bool LoseHP()
    {
        _hearts[_hp - 1].SetActive(false);
        _hp--;

        if (_hp <= 0)
            return true;
        else
            return false;
    }
}
