using InventoryManager.Constants;
using InventoryManager.Models;
using InventoryManager.UI;

namespace InventoryManager.Handlers;

/// <summary>
/// Provide methods to get input from user
/// </summary>
public class FormHandler : IFormHandler
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormHandler"/> class.
    /// </summary>
    /// <param name="userInterface">Provide methods to interact with user via UI</param>
    public FormHandler(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <inheritdoc/>
    public int GetIndex(ProductList productList)
    {
        do
        {
            string indexString = this._userInterface.PromptAndGetInput("Enter the index of the product : ");
            if (!int.TryParse(indexString, out int index))
            {
                this._userInterface.PromptLine(ErrorMessages.NotValidIndex, ConsoleColor.Yellow);
                continue;
            }

            if (index > productList.Count() || index <= 0)
            {
                this._userInterface.PromptLine(ErrorMessages.NoProductAtGivenIndex + index, ConsoleColor.Yellow);
                continue;
            }

            return index;
        }
        while (true);
    }

    /// <inheritdoc/>
    public string GetFieldName(ProductList productList)
    {
        do
        {
            string[] fields = Product.GetFields();
            for (int i = 1; i < fields.Length; i++)
            {
                this._userInterface.Prompt($"{i} {fields[i]} ");
            }

            string fieldChoiceString = this._userInterface.PromptAndGetInput("\nEnter a field : ");
            if (!(int.TryParse(fieldChoiceString, out int fieldChoice) && fieldChoice < fields.Length && fieldChoice > 0))
            {
                this._userInterface.PromptLine(ErrorMessages.NotValidField, ConsoleColor.Yellow);
                continue;
            }

            return fields[fieldChoice];
        }
        while (true);
    }

    /// <inheritdoc/>
    public bool ShowProducts(ProductList productList)
    {
        if (productList.Count() <= 0)
        {
            this._userInterface.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow);
            this._userInterface.WaitAndNavigateToMenu();
            return false;
        }

        List<Product> currentProductList = productList.Get();
        this._userInterface.PrintAsTable(currentProductList);
        return true;
    }
}
