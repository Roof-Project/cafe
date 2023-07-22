using System.Collections;
using System.Collections.Generic;
using System;

public static class EventBus
{
    public static Action<string> OnShowPlaces;//показать место
    public static Action<string> OnHideAPlace;//паказать место
}
