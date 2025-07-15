# Contact Manager Console Application - Developer Docs

## Overview  
It is a C# console based contact management system that allow users to **Add**, **Show**, **Edit**, **Delete**, **Sort**, and **Search** contacts.  

---

## Control flow  
![Flow Chart](./Images/flowChart.png)

---

## Important Components

---

## Contact Class  
It represents a single contact stores information such as name, phone, email, notes in a dictionary :
```cs
 public Dictionary<string, string> ContactInfo
 ```

### Sample contact template:

```json
{
    "Name": "Sankar",
    "Phone": "99894485773",
    "Email": "sankar@dev.com",
    "Notes": "yoyo"
}
```
Any new field can be added simply by modifying the template. This change will sync across contact manager and controller's core operations without needing to rewrite them manually.

---

## Contact Repository Class  
Creates and manages the main list of Contact and provides core operations:

| **Function**                           | **Description**                                                                 |
|----------------------------------------|---------------------------------------------------------------------------------|
| `static Dictionary<string, string> GetContactTemplate()` | Returns the contact template.<br>Current template : ```{ "Name": "", "Phone": "", "Email": "", "Notes": "" } ``` |
| `Contact AddContact()`                 | Add a contact object to the list and returns the added contact.                |
| `List<Contact> GetContacts()`          | Returns the entire list of contacts.                                            |
| `Contact GetContact(int index)`        | Returns the contact object at the specified index.                              |
| `void EditContact(int index, string field, string newValue)` | Updates contact based on specified index, field and new value to change.         |
| `void DeleteContact(int index)`        | Removes selected contact from list at specifiedindex.                                    |
| `List<Contact> Search(field, key)`     | Searches contacts by field and keyword, returns the matched list.               |
| `void SortContacts(string field)`      | Sorts the contact list by the given field ( name, created time).           |
| `bool HaveDuplicate(string field, string value)` | Returns true if a duplicate field value exists,else return false.        |
| `bool IsValidIndex(int index)`         | Returns true if the index is in range, false if out of range.                   |

---

## User Form Handler  
These handlers get the field input until user gives a valid input. Each function has certain validation. More validation can be added to respective functions :

| **Function**      | **Validation / Description**                                                                 |
|-------------------|----------------------------------------------------------------------------------------------|
| `Get(field)`      | Dynamically invokes `Get<Field>()` function. If no handler exists, uses `GetWithoutValidation()` to prevent crashes and allow future expansion with validation for fields like `GetAddress()`, `GetBirthDay()`, etc. |
| `GetName()`       | Currently no validation.                                                                      |
| `GetEmail()`      | Uses built-in email validator attribute.                                                      |
| `GetPhone()`      | Must be a number and must be 10 digits.                                                       |
| `GetNotes()`      | Currently no validation.                                                                      |
| `GetField()`      | Lists available fields from contact repository and allows user to select one of them. Dynamically adjusts to field additions. |
| `GetOptions()`    | Currently supports only "sort by name" and "sort by time".                                    |


---

## Contact controller
Handle all the main features of contact manager Like Add, Show, Edit, Delete, Sort and Search. It has the access to contact repository and its methods.

| **Function**             | **Description**                                                                 |
|--------------------------|---------------------------------------------------------------------------------|
| `ManageAddContact()`     | Handle the process of adding contact. Create an empty new contact dictionary in contact format, get all the required inputs for each field (key) in dictionary, add a new contact in repository and navigate to menu with user option. |
| `ManageShowContact()`    | Handles the process of displaying contacts. Get all the contacts from contact repository and store them in a list and display as a table and navigate to menu with user option.  |
| `ManageEditContact()`    | Handles the process of editing a contact. Get all contacts and list them to user, prompt the user to enter a valid index, field, and value to edit. Apply the changes and navigate to menu with user option. |
| `ManageDeleteContact()`  | Handles the process of deleting a contact. Get all contacts and list them to user, prompt the user to select index of contact to delete and delete it. Navigate to menu based on user option. |
| `ManageSortContact()`    | Sorts contacts alphabetically. Get all contacts and list them to the user with sort operations, prompt the user to select a sort operation and sort it via contact repository, display sorted contacts and navigate menu based on user option. |
| `ManageSearchContact()`  | Searches contacts by field. Prompt the user to select the field to search and keyword. Get the matched results from contact repository and list them. Navigate to menu based on user option. |






---

## ConsoleUI 
It has all necessary function to Console write and read operation, the entire styling can be created and edited via this functions.

| **Function**                               | **Validation / Description**                                                                 |
|--------------------------------------------|----------------------------------------------------------------------------------------------|
| `PrintContacts(List<Contact> contacts)`    | Print the entire contact list in table format                               |
| `PrintContacts(Contact contact)`            | Print the contact in table format.   |                                                              |
| `WaitAndReturnToMenu()`                  | Prompt the user waits for the key press and navigates to menu                                                 |
| `PrintAppHeader(string modeName)`             | Prints the header with title and current mode name at top of ebery page.                       |
| `PromptWarning(string prompt)`       | Prints warning message with custom styling.                                                   |
| `PromptSuccess(string prompt)`       | Prints success message with custom styling.                                                   |
| `PromptInfo(string prompt)`              | Prints message with custom styling.                                                           |
| `GetInputWithPrompt(string prompt)`         | Prompts the user, and returns the input from user.                                            |
| `GetRowFormat()`                  | Create a row format according to the number of fields, column width should manually customised in code if needed |
| `PrintTableHeader()`       | Prints the header of the table with field names  |
---