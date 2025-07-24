# Expense tracker Console Application - Developer Docs

## Overview  
It is a C# console based expense tracker system that allow users to **Add**, **view** transactions.  

---

## Important Components

---
## Interfaces
### IAccount
```cs
public interface IAccount
```
Represents an user account with balance and transaction info and the core operations.
| **Member**                                                                 | **Description**                                                                 |
|----------------------------------------------------------------------------|---------------------------------------------------------------------------------|
| `decimal CurrentBalance { get; }`                                          | Gets the current balance of the account.                                        |
| `decimal TotalIncome { get; }`                                             | Gets the total income of the account.                                           |
| `decimal TotalExpense { get; }`                                            | Gets the total expense of the account.                                          |
| `List<ITransaction> TotalTransactionDataList { get; }`                     | Gets the list of all transactions.                                              |
| `List<string> Categories { get; set; }`                                    | Gets or sets the list of available categories.                                  |
| `List<string> Sources { get; set; }`                                       | Gets or sets the list of available sources.                                     |
| `void AddIncome(decimal incomeAmount, string source)`                      | Creates an income transaction with the specified amount and source.             |
| `void AddExpense(decimal expenseAmount, string category)`                  | Creates an expense transaction with the specified amount and category.          |
| `void EditTransactionAmount(int index, decimal newAmountValue)`            | Edit the amount of transaction at specified index.							   |
| `void EditIncomeTransactionSource(int index, string newSourceValue)`       | Edits the source of income transaction of specified index.			           |
| `void EditExpenseTransactionCategory(int index, string newCategoryValue)`  | Edits category of expense transaction of specified index.				       |
| `void DeleteTransaction(int index)`									     | Deletes a transaction in a specified index.									   |

### ITransaction interface
 ```cs
 public interface ITransaction
 ```
This is the base interface for all types transaction data like `ExpenseTransactionData`, `IncomeTransactionData`. It contains common properties like Amount, CreatedAt.
| **Property**                  | **Description**                                                                 |
|------------------------------|---------------------------------------------------------------------------------|
| `decimal Amount { get; set; }`   | Gets or sets the amount transferred in the transaction.                        |
| `DateTime CreatedAt { get; set; }` | Gets or sets the time the transaction was initiated.                           |

### IController interface
```cs
public interface IController
```
Represents the controller for expense tracker which should allow user to add and view transactions.
| **Function / Property**                          | **Description**																		      |
|--------------------------------------------------|----------------------------------------------------------------------------------------------|
| `IAccount UserAccount { set; }`                  | Sets the user account to implement user actions.											  |
| `IUserInterface UserInterface { set; }`          | Sets the user interface object for this controller.										  |
| `void HandleMenu()`                              | Displays the menu, prompts the user to select an action, and handles the selected action.    |
| `void HandleAddIncome()`                         | Handles user input and adds an income transaction to the user's account.					  |
| `void HandleAddExpense()`                        | Handles user input and adds an expense transaction to the user's account.				      |
| `void HandleViewTransactions()`                  | Displays transaction details from the user's account.									      |
| `void HandleEditTransaction()`                   | Handle getting user inputs and edit a transaction to user's account.                         |
| `void HandleDeleteTransaction()`                 | Handle getting user inputs and delete a transaction from user's account.				      |


### IUserInterface
```cs
public interface IUserInterface
```
Represents the user interface and its methods to interact with user.

| **Function**                                                                                      | **Description**                                                                 |
|----------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------|
| `string? PromptAndGetInput(string prompt)`                                                          | Prompts the user and returns a input.                                  |
| `void MoveToAction(string action)`                                                                 | Displays a title or action message to the user.                                 |
| `void ShowInfoMessage(string info)`                                                                | Displays informational messages to the user.                                    |
| `void ShowWarningMessage(string warningMessage)`                                                   | Displays warning messages to the user.                                          |
| `void ShowSuccessMessage(string successMessage)`                                                   | Displays success messages to the user.                                          |
| `void ShowTransactionList(List<ITransaction> userTransactionDataList, bool showIncome = true, bool showExpense = true)` | Displays a list of transactions with optional filters for income and expense.   |
| `void ShowTransactionData(ITransaction transactionData)`                                           | Displays a single transaction's data to the user.                               |


## Constants
**Error messages** : contains all the error messages.
**Status messages** : contains all the status messages.
**Prompt messages** : contains all the prompt messages.
**Headings** : contains all the headings.
