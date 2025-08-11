# Exploration answers

## What the .NET platform is and its primary purpose ?
 
* .Net is a software development framework created to develop windows and web app initially now it is a ecosystem that includes development frameworks, programming languages, runtime, libraries, etc. Now we can develop .NET apps for MacOS, Linux, mobile, IoT, gaming etc.
* It provides a runtime where it converts any .NET  programming languages to intermediate language which can run into any machines as it is independent of CPU and OS. In short term we can say that .NET allows us "Compile once and run anywhere"
* It supports any CLS followed languages to develop a .NET assembly as all this are converted to IL when it comes to runtime which means the code developed in one .NET language can be used in another.
* Apart from this it also provides other key features like base libraries, automatic memory management, exception handling, asynchronous programming, type safety check, LINQ etc.
 
 
## Key components of .NET platform.
 
* The key component of the .NET platform is its runtime environment and its base class library.
  * Common Language runtime is a virtual machine with a goal to make programming easy. It manages all the low level operations on its own makes the .NET languages simpler (C# is simpler than C++). The main features of the CLR is garabage collection, cross language, type safety, threading, program isolation with app domains, etc.
  * Base class library is a standard set of libraries containing reusable classes and types like string, int, collections, file handling, task etc.. It can used by all .NET languages


## CLR and CTS.
 
| Common Language Runtime                                                                            | Common Type System                                                                      |
| -------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| CLR is an runtime environment that is responsible runs the program, manage memory and threads etc. | CTS is a part of CLR that ensures type safety of code as .NET supports cross languages. |
| It provides an environment to run the code.                                                        | It enables type safety between languages by ensuring common set of data types.          |
| It is an implementation as software.                                                               | It is a specification like set of rules.                                                |
 

 ## Role of Global Assembly Cache.
 
* Global assembly cache is the global storage for all code assemblies that are accessible to all application running in a specific machine.
* It is advised to share a assembly by installing it in an GAC to all application only when it is explicitly needed because it can cause a problem if a application depends on GAC, it wont run on the another computer unless that dependency assembly is also installed in that computer.## Global assembly cache
 
* Global assembly cache is the global storage for all code assemblies that are accessible to all application running in a specific machine.
* It is advised to share a assembly by installing it in an GAC to all application only when it is explicitly needed because it can cause a problem if a application depends on GAC, it wont run on the another computer unless that dependency assembly is also installed in that computer.


## Value types and reference types.
 
| Value types                                                                                                                                                      | Reference types                                                                                                                                                           |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Value type have well defined sizes at compile time itself.                                                                                                       | Reference type's size ca be varied.                                                                                                                                       |
| Value types are always stored directly in stack frame, unless it is enclosed by any object (Reference type).                                                     | Reference type data are always stored at the heap. Only its references were stored at the stack frame.                                                                    |
| Value type can't be null be null by default -  unless it's explicitly declared as nullable. It stores some default value until declared with some value instead. | Reference value can be nulled. When we null a reference only the address of the data stored will be erased but the data stored at remains and waits to be collected by GC.|
| Value type only exists at its scope of living																													   | Only the references are destroyed after scope, but the data will remain at heap until it is collected by GC															   |
| When copied the entire data is duplicated.                                                                                                                       | When copied only the reference is copied which is still pointing to the same data in heap.                                                                                |
| Examples : int, bool, struct                                                                                                                                     | Example : List, array, string, class                                                                                                                                      |
 
## Garbage collections in .NET and its advantages.
* Garbage collection is term used to define automatic memory manage system of CLR of .NET.
* This GC tracks all the objects in the heap and sweep the unused objects periodically. 
* GC uses the three generational collection to remove unused object. Most of the objects dies at gen 0, the object didn't die at gen 0 will be moved to gen 1. It will moved to gen 2 if it survives the gen 1.
* The frequency between this collections differs. gen 0 is collected very frequently while gen 2 is collected rarely.
* Gen 0 is also includes in gen 1 and gen 2 collection and that's why gen 2 is called as full GC.
* There are two heaps small object heap and large object heap, here SOH will get three generational collections and LOH will only visited at gen 2 as large objects usually used for long time.
* GC tracks the living object from roots like a tree, it leaves all the connected object to root and marks sweeps other object.
* GC don't sweeps the static objects but still tracks them.
* After sweeping GC will do compact to defragment everything.

## Globalization and localization.
 
* Globalization is the way of developing an that can adapted in any other culture and languages.
* The design and development of apps that's support globalization should be done by awarding language or culture specific content as hard coded. Instead it uses `.resx` files to store texts, designing UI as it supports both left to right and right to left languages, support of all kind of date formats etc.
* Localization is process of adapting a converting a product specific language and culture. This can be only done after the globalization.
* Hence globalization is the way of designing a app that can work in any culture or language and localization is the process of that conversion the app work in a specific culture and language before release.
 
 ## Role of CIL and JIT.
 
* Common intermediate is CPU independent language in which all .NET languages are compiled into before getting executed.
* It enables cross language support to .NET, it also makes easy to optimize at runtime with help of JIT.
* This IL is stored in a .dll or .exe assemblies that can be executed at any OS or CPU with a .NET runtime environment or reused in other projects
* Just in time, as name implies this CIL is only compiled into machine at runtime only when method is needed.
* This compiling at runtime allows JIT to optimize the code based the specific hardware and runtime environment.
 
 