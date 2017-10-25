# Assemblies

`Shepherd` gathers all your application types in order to serve them for registration, either during the `Modules` iterations or for the Auto-Register process. In order to do so Shepherd needs to know from which `Assemblies` to extract the types.

`Shepherd` Expose a property named `Shepherd.Assemlies`, the `Assemblies` is of `AssemblyCollection` type, this collection have `Add` method accepts `IAssemblyLoader`,   

Out of the box `Shepherd` provide two ways to extract types from an Assembly:<br>
* `PublicTypesAssemblyLoader` - Gather all public types from the Assembly.
* `CompleteTypeAssemblyLoader` - Gather public, internal and private types from the assembly.

```C#
var shepherd = new Shepherd();
shepherd.Assemblies
	.AddCompleteTypeAssemblies(ApplicationAssembly, DataAccessAssembly)
	.AddPublicTypesAssemblies(FrameworkAssembly);
```

### Extend
As written above The `AssemblyCollection.Add()` method accepts an interface of `IAssemblyLoader`, the `IAssemblyLoader` expose `GetTypes() : IEnumerable<Type>` method. So if in need a new class can be implemented in order achieve that.

### Extensions
`Shepherd` provides two extension methods to add assemblies easier like so:

```C#
shepherd.AddCompleteTypeAssemblies(ApplicationAssembly, DataAccessAssembly)
        .AddPublicTypesAssemblies(FrameworkAssembly);
```