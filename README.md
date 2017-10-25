<img src="https://raw.githubusercontent.com/existall/Shepherd/master/art/logo.png" alt="ExistsForAll">

# ExistsForAll.Shepherd.SimpleInjector

## installation
`Install-Package ExistsForAll.Shepherd.SimpleInjector`

### Intoduction
SimpleInjector is a great IoC Container, over the years it became my go to DI for every project.  
Thus we've developed methodologies to register infrastructure and domain services quickly.

## Table of Content
1. Getting started  
2. SimpleInjector Configuration
3. [Assemblies](https://github.com/existall/Shepherd/blob/master/documents/Assemblies.md)  
4. [Modules]()  
5. [Auto-Registration]()

### Getting Started

```C#
var shepherd = new Shepherd();
var container = shepherd.AddPublicTypesAssemblies(Assemblies)
			.AddModule(LoggingModule, DataAccessModule, ConfigurationModule)
			.Herd();
```

Shepherd will gather all the types from the given assemblies (all or only public), iterate over all the modules by order of insertion and auto-register all remaining domain services. The return value from the `Herd()` method is `SimpleInjector` container.

