# Advanced language feature of C Sharp

I have created a single console application to run all the tasks that demonstrates the working of each advanced language features of c sharp
You can choose which task to run in the main menu.

## Task 1 : Event and delegates

- I have created a notifier class that allows the notification based methods to subscribe it's event `OnAction` and contains a method to trigger the event.
- The OnAction event is based on delegate `notify` which gets a string and returns a void.
- I have subscribed the `OnAction` event some display methods that will display the user input message and it details to the user.

## Task 2 : Var and Dynamic

- This task is to demonstrate how var and dynamic and var works.
- when we declare variable with `var` keyword we have to initiate it in the same line because compiler sets the variable type declared with `var` keyword at compile time.
- So, we can't assign another type value to variable initiated with `var` keyword.
- Variables declared with `dynamic` keyword can change its type at runtime. so we can assign any type of values to that variable.

## Task 3 : Anonymous function

- Anonymous method is unnamed block of code that can be declared to any delegate.
- It we will be used when we need to pass a small blocks of code as a argument without declaring it as a separate named method.
- Here I have created a comparison delegate ```public delegate int Comparison<in T>(T x, T y);``` to sort an array in ascending order.\

## Task 4 : Lambda expressions

- Lambda expressions are used as a short hand method to write anonymous function in c sharp.
- All lambda expression are always anonymous.

## Task 5 : Delegates advanced

- In this tasks I have used this delegates to reuse the sorting logics.
- I have created a `Product` type with properties name, category, price.
- A `SortDelegate` is used to pass different sorting logic into a single method `SortAndDisplay`.
- With that I have sorted products by Name, Category and Price using the same method but with different delegate functions.hanging the method.

## Task 6 : Records

- Records are the special reference type with immutability and value based equality
- I have tested this features by trying to change the values and comparing two different records with same value with `==` operator.
- I have used deconstruction to extract the individual components of the record.

## Task 7 : Pattern matching

- Pattern matching is used to simplify conditional logic using patterns.
- Here I have used pattern matching to identify the type of the shape and prints details according to its type.

> ## Additional things

### Microsoft dependency injection extension

- I have used Microsoft dependency injection to inject services
- I have chose singelton for UI service as it only need the single instance for entire application
- I have chose transient for task as it is short lived and related only one specific action.