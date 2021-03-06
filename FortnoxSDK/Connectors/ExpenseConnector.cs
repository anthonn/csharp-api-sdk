using System.Threading.Tasks;
using Fortnox.SDK.Connectors.Base;
using Fortnox.SDK.Entities;
using Fortnox.SDK.Interfaces;
using Fortnox.SDK.Search;
using Fortnox.SDK.Utility;

// ReSharper disable UnusedMember.Global

namespace Fortnox.SDK.Connectors
{
    /// <remarks/>
    public class ExpenseConnector : SearchableEntityConnector<Expense, ExpenseSubset, ExpenseSearch>, IExpenseConnector
	{


		/// <remarks/>
		public ExpenseConnector()
		{
			Resource = "expenses";
		}
		/// <summary>
		/// Find a expense based on id
		/// </summary>
		/// <param name="id">Identifier of the expense to find</param>
		/// <returns>The found expense</returns>
		public Expense Get(string id)
		{
			return GetAsync(id).GetResult();
		}

		/// <summary>
		/// Creates a new expense
		/// </summary>
		/// <param name="expense">The expense to create</param>
		/// <returns>The created expense</returns>
		public Expense Create(Expense expense)
		{
			return CreateAsync(expense).GetResult();
		}

		/// <summary>
		/// Gets a list of expenses
		/// </summary>
		/// <returns>A list of expenses</returns>
		public EntityCollection<ExpenseSubset> Find(ExpenseSearch searchSettings)
		{
			return FindAsync(searchSettings).GetResult();
		}

		public async Task<EntityCollection<ExpenseSubset>> FindAsync(ExpenseSearch searchSettings)
		{
			return await BaseFind(searchSettings).ConfigureAwait(false);
		}
		public async Task<Expense> CreateAsync(Expense expense)
		{
			return await BaseCreate(expense).ConfigureAwait(false);
		}
		public async Task<Expense> GetAsync(string id)
		{
			return await BaseGet(id).ConfigureAwait(false);
		}
	}
}
