using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MaterialChangeFeedback : Feedback
{
    public Material Material;
    public MeshRenderer[] Renderers;
    public float Duration;

    int count;
    
    
    public Material[] OriginalMaterials;

    private void Start() {
        if (Renderers == null) return;

        OriginalMaterials = new Material[Renderers.Length];

        for (var i = 0; i < Renderers.Length; i++) {
            OriginalMaterials[i] = Renderers[i].material;
        }
    }

    public override void Invoke(FeedbackSettings _feedbackSettings) {
        base.Invoke(_feedbackSettings);

        ChangeMaterialsAsync(Duration);
    }

    public async Task ChangeMaterialsAsync(float _duration) {
        foreach (var r in Renderers) {
            r.material = Material;
        }
        count++;
        await Task.Delay((int)(_duration * 1000));
        count--;

        if (count == 0) {
            for (var index = 0; index < Renderers.Length; index++) {
                Renderers[index].material = OriginalMaterials[index];
            }
        }
    }
}
