<img src="https://raw.githubusercontent.com/existall/Shepherd/master/art/logo.png" alt="ExistsForAll">

# ExistsForAll.Shepherd.SimpleInjector


Love Dependency Injection, but tired of your herd of services scatter around your code freely? Want to feel in control of your dependecies again? Meet **Shepherd**. The DI wrapper that objects follow.

Shepherd provides dependency injection without the hassle of manually assigning interfaces and services. Shepherd will register your services automatically, and let you define specific registrations (for logging, storage access, etc.).

Shepherd currently wraps [SimpleInjector](https://simpleinjector.org/index.html), but is planned to support more DI tools in the future.

Shepherd should act as your Composition Root in your application startup. 

## Installation
```
	Install-Package ExistsForAll.Shepherd.SimpleInjector
```

## Table of Content
1. Getting started  
2. [SimpleInjector Configuration](https://github.com/existall/Shepherd/blob/master/documents/SimpleInjector_Configuration.md)
3. [Assemblies](https://github.com/existall/Shepherd/blob/master/documents/Assemblies.md)  
4. [Modules](https://github.com/existall/Shepherd/blob/master/documents/Modules.md)  
5. [Auto-Registration](https://github.com/existall/Shepherd/blob/master/documents/Auto-Registration.md)

### Getting Started

```C#
var shepherd = new Shepherd();
var container = shepherd.AddPublicTypesAssemblies(Assemblies)
			.AddModule(LoggingModule, DataAccessModule, ConfigurationModule)
			.Herd();
```

Shepherd will gather all the types from the given assemblies (all or only public), iterate over all the modules by order of insertion and auto-register all remaining domain services. The return value from the `Herd()` method is `SimpleInjector` container.

### Order of actions
1. The first thing `Shepherd` does after validation and configuration is to gather all the types from the given assemblies.  
2. Then `Shepherd` Will iterate over all the `Modules` in the order of insertion, after that `Shepherd` will index.
3. `Shepherd` will index all interfaces to matching implementation and will register them as explained in the [Auto-Registration](https://github.com/existall/Shepherd/blob/master/documents/Auto-Registration.md) section.

