# SimpleInjector Configuration

`Shepherd` can be initialized with an existing `SimpleInjector` instance via the constructor or let `shepherd` creates a new one for you.

If you choose to pass an existing container do it like so, 

```C#
var container = new SimpleInjector();
var shepherd = new Shepherd(container);
```
This way you can configure `SimpleInjector` via `SimpleInjectorOptions`. If you choose the latter, you can't access the container until you call `Herd()` or via a `Module`.

`Shepherd` expose an option that will let you configure the container when `Herd` is invoked, to be more specific this is the first action `Shepherd` does.

To configure `SimpleInjector` in Herd time you need to override 

```C#
public interface IContainerOptionsConfiguration
{
	void Configure(ContainerOptions containerOptions);
}
```

And override the default option,

```C#
shepherd.Options.ConfigureContainerOptions = new MyContainerOptionsConfiguration();
```

