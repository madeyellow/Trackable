# What is it?
The `Trackable` is a generic property wrapper for **Unity projects** that tracks changes of nested property and invokes custom `UnityEvent` **when changes occured**.

## Getting started

For example you have some code like this:

```csharp
public bool isGrounded;
private bool _previousIsGrounded;

private void Update() {
  isGrounded = CheckIsGrounded();
  
  if (isGrounded != _previousIsGrounded) {
    isGrounded = _previousIsGrounded;
    
    OnIsGroundedChanged(); // OnIsGroundedChanged - is your's handler of isGrounded change event
  }
}
```

You can simplify code to this:

```csharp
using MadeYellow.Trackables;

...

public Trackable<bool> isGrounded = new Trackable<bool>();

private void Start() {
  isGrounded.onChanged.AddListener(OnIsGroundedChanged); // OnIsGroundedChanged - is your's handler of isGrounded change event
}

private void Update() {
  isGrounded.value = CheckIsGrounded();
}
```

Both will work the same, but with `Trackable` you will have power of `UnityEvent` and both readability. Whenever you set new value to `.value` - change tracking will happen automatically.

## How to install Trackable to my Unity project?

Use the Unity Package Manager (in Unity’s top menu: **Window > Package Manager**), click "+" icon, select **"Add package from git URL"** and type URL of this repository.

## Can i check changes not right after value is changed?

Yep! There is built-it `TrackingStrategy.Manual` that can be provided to `Trackable` constructor OR can be choosed ight in Unity editor. You have to call `.Commit();` at desired moment in order to change tracking happen. It will be proper strategy if your script changes variable several times during complex method execution and you want to perform single change tracking at the very end of method (when you have final value calculated) instead of multiple checks.

Here's how you may use it:

```csharp
using MadeYellow.Trackables;

...

public Trackable<bool> isGrounded = new Trackable<bool>(TrackingStrategy.Manual); // Here default auto detection strategy changed to manual

private void Update() {
  isGrounded.value = false;

  isGrounded.value = CheckIsGroundedOne();

  isGrounded.value = CheckIsGroundedTwo();

  isGrounded.Commit(); // Only last assigned value will be compared to previously commited value
}
```
