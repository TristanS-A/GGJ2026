using UnityEngine.Events;

public static class EventSystem
{
    public static UnityAction OnEventExample;
    public static void EventExample() { OnEventExample?.Invoke(); }

    public static UnityAction<UISlideOut.UIType, bool> OnSlideOutUI;
    public static void SlideOutUI(UISlideOut.UIType uiToSlideOut, bool slidingIn) { OnSlideOutUI?.Invoke(uiToSlideOut, slidingIn); }
}