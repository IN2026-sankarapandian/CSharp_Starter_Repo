# Inventory Manager Console Application - Developer Docs

## Overview  
It is a C# console based inventory management system that allow users to **Add**, **Show**, **Edit**, **Delete**, and **Search** products.  

---

## Important Components

---

## Product Class  
It represents a single product stores information such as Name, Id, Price, Quantity in a dictionary :
```cs
 Dictionary<string, object> _product
 ```

### Sample product template:

```json
{
    { Name, typeof(string) },
    { Id, typeof(string) },
    { Price, typeof(int) },
    { Quantity, typeof(int) },
}
```
Any new field can be added simply by modifying the template. This change will sync across inventory manager and core operations without needing to rewrite them manually.

---

## Product List Class  
Creates and manages the main list of Contact and provides core operations:

| **Function**                           | **Description**                                                                 |
|----------------------------------------|---------------------------------------------------------------------------------|
| `void Add(Product newProduct)`                 | Add a product to the product list               |
| `List<Product> Get()`          | Returns the entire list of products.                                            |
| `void Edit(int index, string field, object newValue)`        | Edits the value of a field in a Product at specified index in the inventory.                          |
| `void Delete(int index)`        | Delete the Product in inventory at given index.                                    |
| `List<Product> Search(string keyword)`     | Search for the keyword and return all the matched Products as list.               |
| `int Count()`      | Holds the count of Products in the list.           |
| `bool HasDuplicate(string field, object value)`  | Check for duplicates in specified field for specified value.  |


## Action handlers
Handle all the main features of contact manager Like Add, Show, Edit, Delete, Sort and Search. It has the access to contact repository and its methods.

| **Function**             | **Description**                                                                 |
|--------------------------|---------------------------------------------------------------------------------|
| `HandleAddProduct(ProductList list)`     | This method prompts the user to enter all the details for the product and create a new product with the user input details in ProductList It also validate every inputs from the user using Validator |
| `void HandleShowProducts(ProductList list)`    | This method lists all the products as a table format to the user. It informs user if product list was empty and ask user to return back to menu.  |
| `void HandleEditProduct(ProductList list)`    | This method lists all the products as a table format to the user and prompt the user to select a index of product to edit (It informs user if product list was empty and ask user to return back to menu). It list all the available fields of the product and prompt the user to select the field to edit. Then it prompt the user to enter the new value to change and apply changes to the list via ProductList. This function validates the index, field, and new value of the field with Validator |
| `void HandleDeleteProduct(ProductList list)`  | This method lists the products as table format with index to the user and prompt the user to select a index of product to delete. Deletes the product at specified index in ProductList. If no products are in the list, then it inform the user that there is no products in the list so navigate back to menu. It also check the input is a valid index using Validator. |
| `void HandleSearchProduct(ProductList list)`    | This method prompt the user to give keyword to search and pass the keyword to ProductList via search funcion and list the matched results, if there is no matches it will inform user that there is no matches so navigate to menu. If no products are in the list, then it inform the user that there is no products in the list so navigate back to menu. |
| `async Task<bool> ConfirmExitAsync()`  | Ask the user for confirmation to exit with a warning of loss of data. |
## Validator and Parsers
| **Function**             | **Description**                                                                 |
|--------------------------|---------------------------------------------------------------------------------|
|`bool Validate(ProductList list, string field, object? value, out string error)` | Validate the value based on the field name and will not validate if it is an unhandled field and out the appropriate error message if validation failed.|
|`bool TryParseValue(string input, Type type, out object result, out string errorMessage)` | Try to parse the input string to the specified data type. Out the appropriate error message if parsing faied. |



---

## ConsoleUI 
It has all necessary function to Console write and read operation, the entire styling can be created and edited via this functions.

| **Function**                               | **Validation / Description**                                                                 |
|--------------------------------------------|----------------------------------------------------------------------------------------------|
| `string PromptAndGetInput(string prompt, ConsoleColor color = ConsoleColor.White)`    | Prompt the user and returns the user input for the prompt.|
| `void Prompt(string info, ConsoleColor color = ConsoleColor.White)`            |Prompt the user with info with specified color.|                                                              |
| `void PromptLine(string prompt, ConsoleColor color = ConsoleColor.White)`                  |  Prompt the user with info in new line with specified color.                                                |
| `void PrintAsTable(List<Product> currentProductList)`             | Prints list of Products as a table in the console.                 |
| `void WaitAndNavigateToMenu()`       |  Wait for the keypress and clear the console and navigate back to the menu.                                                   |
| `void CreateNewPageFor(string action)`         | Clears the console and create new page for the actions.                                     |
---

## Constants
**Error messages** : contains all the error messages as constants.
**Product field names** : contains all the available field names.
