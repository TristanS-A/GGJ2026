using System.Collections.Generic;
using UnityEngine.Events;

public static class EventSystem
{
    public static UnityAction<UISlideOut.UIType, bool> OnSlideOutUI;
    public static void SlideOutUI(UISlideOut.UIType uiToSlideOut, bool slidingIn) { OnSlideOutUI?.Invoke(uiToSlideOut, slidingIn); }

    public static UnityAction<List<TrayIngredient>> OnStartSkiball;
    public static void StartSkiball(List<TrayIngredient> ingredients) { OnStartSkiball?.Invoke(ingredients); }

    public static UnityAction OnCompletedBallThrow;
    public static void CompletedBallThrow() { OnCompletedBallThrow?.Invoke(); }

    public static UnityAction<int> OnAddSkiballPoints;
    public static void AddSkiBallPoints(int pointsToAdd) { OnAddSkiballPoints?.Invoke(pointsToAdd); }

    public static UnityAction<NPCDialogueData, int> OnStartNextOrder;
    public static void StartNextOrder(NPCDialogueData customerData, int orderIndex) { OnStartNextOrder?.Invoke(customerData, orderIndex); }

    public static UnityAction<OrderQuality> OnRateOrder;
    public static void RateOrder(OrderQuality quality) { OnRateOrder?.Invoke(quality); }

    public static UnityAction<bool> OnRateShot;
    public static void RateShot(bool goodShot) { OnRateShot?.Invoke(goodShot); }
}