using System.Collections;
using UnityEngine;

public class RPGHealth : MonoBehaviour
{
    private GameObject[] _hearts;

    [Header("Our Health")]
    [Tooltip("Health from 0 to 10")]
    [Range(0, 10)]
    [SerializeField] private float _HP;

    private void Start()
    {
        if (_HP <= 0) _HP = 10f;
        _hearts = GameObject.FindGameObjectsWithTag("Heart");
        StartCoroutine(ShowWork());
    }

    private void Damage(float damage)
    {
        if (damage > 0 && damage < 10) _HP -= damage;
        Draw();
        // if there is no HP left, delete player
        if (_HP <= 0) Destroy(gameObject);
    }
    private void Draw()
    {
        int start = 0;
        int end = ((int)this._HP * 2) / 4;
        for (int i = start; i < _hearts.Length; i++)
            _hearts[i].SetActive(false);
        for (int i = start + 1; i < end; i++)
            _hearts[i].SetActive(true);
    }
    private IEnumerator ShowWork()
    {
        System.Random r = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(r.Next(2, 4));
            Damage(r.Next(1, 4));
        }
    }
}
