# Auto-Registration

The main feature `Shepherd` provides is auto registration of application domain objects, each unique registration method can be replaced or extended yet the order is set by `Shepherd` and can not be changed.

## Service Index (`IServiceIndexer`)

Prior to auto register `Shepherd` tries to index all interface to their Implementations, this is can be extended or replaced yet the default `ServiceIndexer` provides two filtering capabilities.

```C#
public interface IServiceIndexer
{
	FilterCollection Filters { get; }
	IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes);
}When 
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



