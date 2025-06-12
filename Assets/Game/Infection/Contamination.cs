using System.Collections.Generic;
using UnityEngine;
using System.Collections;



public class Contamination
{
    private float _infectionRadius;
    private LayerMask _infectableLayer;
    private DeathInfection _infection;
    private GameObject _contaminationSpherePrefab;

    public Contamination(DeathInfection infection, float radius, LayerMask infectableLayer)
    {
        _infection = infection;
        _infectionRadius = radius;
        _infectableLayer = infectableLayer;
    }

    public void StartContamination(Vector3 position)
    {
        Collider[] infectables = Physics.OverlapSphere(position, _infectionRadius, _infectableLayer);
        HashSet<Infectable> alreadyInfected = new HashSet<Infectable>();

        foreach (var item in infectables)
        {
            if (item.TryGetComponent<Infectable>(out var infection))
            {
                if (alreadyInfected.Add(infection))
                {
                    infection.ApplyInfection(GameObject.Instantiate(_infection));
                }
            }
        }
    }

    public void StartContaminationWithVisualize(GameObject spherePrefab, Vector3 position)
    {
        CoroutineRunner.Instance.RunCoroutine(ContaminationSequence(spherePrefab, position));
    }

    private IEnumerator ContaminationSequence(GameObject spherePrefab, Vector3 position)
    {
        // Создаем визуальный объект сферы
        GameObject sphereInstance = GameObject.Instantiate(spherePrefab, position, Quaternion.identity);
        sphereInstance.transform.localScale = Vector3.zero;

        // Получаем материал сферы (важно, чтобы у объекта был материал с прозрачностью)
        Material sphereMaterial = sphereInstance.GetComponent<MeshRenderer>().material;
        Color originalColor = sphereMaterial.color;
        originalColor.a = 0.6f; 

        float appearTime = 0.5f;
        float fadeOutTime = 0.5f;
        Vector3 targetScale = Vector3.one * _infectionRadius * 2f;

        // Рост
        float timer = 0f;
        while (timer < appearTime)
        {
            timer += Time.deltaTime;
            float t = timer / appearTime;
            sphereInstance.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }
        sphereInstance.transform.localScale = targetScale;

        // Растворение (fade out)
        timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float t = timer / fadeOutTime;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(originalColor.a, 0f, t);
            sphereMaterial.color = newColor;
            yield return null;
        }

        GameObject.Destroy(sphereInstance);

        // После визуализации — запускаем заражение
        StartContamination(position);
    }
}


