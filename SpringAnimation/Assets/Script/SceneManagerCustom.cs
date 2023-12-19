using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class SceneManagerCustom : MonoBehaviour
{
    public MMF_Player sceneFeedback;

    public void ChangeScene()
    {
        sceneFeedback.PlayFeedbacks();
    }
}
