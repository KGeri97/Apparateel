using System;

public interface IClickable
{
    public Action OnClick { get; set; }

    public void RaiseOnClickEvent() {
        OnClick?.Invoke();
    }
}
