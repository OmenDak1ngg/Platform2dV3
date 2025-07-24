using System.Collections;
using TMPro;
using UnityEngine;

public class TextHealthDisplayer : HealthDisplayer
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    protected override IEnumerator UpdateHealthDisplay()
    {
        _textMeshPro.text = $"{Health.Current}/{Health.Max}";

        yield return null;
    }
}
