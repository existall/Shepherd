# Auto-Registration

The main feature `Shepherd` provides is auto registration of application domain objects, each unique registration method can be replaced or extended yet the order is set by `Shepherd` and can not be changed.

## Service Index (`IServiceIndexer`)

Prior to auto register `Shepherd` tries to index all interface to their Implementations, this is can be extended or replaced yet the default `ServiceIndexer` provides two filtering capabilities.

```C#
public interface IServiceIndexer
{
	FilterCollection Filters { get; }
	IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes);
}
```

### Filters

#### `IInterfaceAccumulationFilter`
The default behavior of `ServiceIndexer` is to accumulate all of the interfaces first, when this accumulation occur all `IInterfaceAccumulationFilter` will be invoked.

```C#
public interface IInterfaceAccumulationFilter : IFilter
{
	bool ShouldExcludeInterface(Type @interface);
}
```

#### `IImplementationAccumulationFilter`
While `Shepherd` accumulate the implementations it will invoke `IImplementationAccumulationFilter`.

```C#
public interface IImplementationAccumulationFilter : IFilter
{
	bool ShouldExcludeClass(Type implementation);
}
```

## Order of Registration 

After the `ServiceIndex` map all services it will register interfaces to services using behaviors.

1. The first behavior `Shepherd` invokes is the `IRegistrationConstraintBehavior` which decided rather or not the service should be registered. This is a general behavior and will be checked against all services. For example the default behavior is not to register an interface without any implementation.
2. All further behaviors are in the same manner, it asks should it register the following Services and how to do it. All behaviors implement the following interface.


```C#
public interface IRegistrationBehavior
{
	bool ShouldRegister(IServiceDescriptor descriptor);
	void Register(IRegistrationContext context, Container container);
}
```

The order in which the behavior are invoked is:
1. `IGenericRegistrationBehavior`
2. `IDecoratorRegistrationBehavior`
3. `ICollectionRegistrationBehavior`
4. `ISingleServiceRegistrationBehavior`

There is no way to change the order of invocation, i found that this is the best way to auto register service.

