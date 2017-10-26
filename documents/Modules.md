# Modules

A Module is simply a class to be invoked by `Shepherd` providing it with `SimpleInjector` container, all of the types `Shepherd` has gathered and Assemblies, thus allowing you to use it as you see fit.

```C#
public interface IModule
{
	void Configure(IModuleContext context);
}

public interface IModuleContext
{
	IEnumerable<Assembly> Assemblies { get; }
	IEnumerable<Type> Types { get; }
	Container Container { get; }
}

class RepositoryModule : IModule
{
	public void Configure(IModuleContext context)
	{
		var repositories = context.Types.Where(type =>  type implements IRepository<>)
		context.Container.Register(IRepository, repositories);
	}
}
```