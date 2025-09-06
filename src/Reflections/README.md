# Reflections

In the assignment I have learned about the use of reflection and implemented via tasks given in the assignment. I 
have developed this as a single console app which accepts plugins for each task. 

- Main app provide all the required methods to interact with user, form handler, utilities, assembly helpers.
- `IUserInterface` provides method to interact with user.
- `formHandlers` provide methods to get assembly related from user like path, type, properties, arguments etc. It also allows filters to restrict user asking for unsupported types.
- `AssemblyHelper` provide methods to handle assembly related things like loading a assembly, invoking a method, creating a instance.
- It also provide a template for task plugin through interface `ITask`, each task class implementing `ITask` represents a subtask from assignment which can be added as a plugin to the main app.
- Plugins can use all the classes provided by main app.

- 

## Task 1: Inspect Assembly Metadata 

- This plugin allows users to view all the information about assembly.
- User can load a valid `.dll` files once by giving it path.
- After plugin load the assembly user can view all the info by selecting the type and which kind of members they want to view.
- In the version of plugin user can view fields, methods, properties, events of any type.
- They can also view the values of properties if the type is instantiated (not static, abstract and have constructor or generic parameters).

## Task 2: Dynamic Object Inspector

- This plugin allow user to view the properties of supported type.
- User can load a valid `.dll` files once by giving it path.
- After plugin load the assembly user can view all the properties by selecting supported types.
- Allows user to set a value for the property by dynamically creating instance for that type.
- In this version of plugin user can set string, decimal and any primitive typed properties.

## Task 3: Dynamic Method Invoker

- This plugin allow user to invoke supported methods of any type in assembly.
- User can load a valid `.dll` files once by giving it path.
- After plugin load the assembly user can view all the methods and can invoke it by selecting supported methods.
- In this version of plugin user can invoke any method that asks for arguments with primitive type, decimal or string.

## Task 4: Dynamic Type Builder

- This plugin allows user to create limited types and properties and methods for it dynamically.
- User should provide the name for class, property and a string value for it.
- User should also provide a name for the methods which will print the property value set by user in console.

## Task 5: Plugin System

In this task I was asked to develop a plugin like app, which provide a common interface for plugins, and 
plug in can use that interface to develop it. Main app should load `.dll` files and run all this plugins in runtime via reflections.

I have developed this entire assignments in this way, here each task is considered as plugin, that will loaded at runtime from the main app.

## Task 6: Mocking Framework

- This task I have created a dynamic type builder for `ICalculator` interface.
- This task was not added as a separate plugin as it is not a tool.
- I have added this task with the main project with some testing methods.
- I have also added a N unit testing for this dynamic built type.

## Task 7: Serialization API

- 
