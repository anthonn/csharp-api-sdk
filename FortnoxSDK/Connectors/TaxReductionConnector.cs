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
    public class TaxReductionConnector : SearchableEntityConnector<TaxReduction, TaxReductionSubset, TaxReductionSearch>, ITaxReductionConnector
	{


		/// <remarks/>
		public TaxReductionConnector()
		{
			Resource = "taxreductions";
		}
		/// <summary>
		/// Find a taxReduction based on id
		/// </summary>
		/// <param name="id">Identifier of the taxReduction to find</param>
		/// <returns>The found taxReduction</returns>
		public TaxReduction Get(string id)
		{
			return GetAsync(id).GetResult();
		}

		/// <summary>
		/// Updates a taxReduction
		/// </summary>
		/// <param name="taxReduction">The taxReduction to update</param>
		/// <returns>The updated taxReduction</returns>
		public TaxReduction Update(TaxReduction taxReduction)
		{
			return UpdateAsync(taxReduction).GetResult();
		}

		/// <summary>
		/// Creates a new taxReduction
		/// </summary>
		/// <param name="taxReduction">The taxReduction to create</param>
		/// <returns>The created taxReduction</returns>
		public TaxReduction Create(TaxReduction taxReduction)
		{
			return CreateAsync(taxReduction).GetResult();
		}

		/// <summary>
		/// Deletes a taxReduction
		/// </summary>
		/// <param name="id">Identifier of the taxReduction to delete</param>
		public void Delete(string id)
		{
			DeleteAsync(id).GetResult();
		}

		/// <summary>
		/// Gets a list of taxReductions
		/// </summary>
		/// <returns>A list of taxReductions</returns>
		public EntityCollection<TaxReductionSubset> Find(TaxReductionSearch searchSettings)
		{
			return FindAsync(searchSettings).GetResult();
		}

		public async Task<EntityCollection<TaxReductionSubset>> FindAsync(TaxReductionSearch searchSettings)
		{
			return await BaseFind(searchSettings).ConfigureAwait(false);
		}
		public async Task DeleteAsync(string id)
		{
			await BaseDelete(id).ConfigureAwait(false);
		}
		public async Task<TaxReduction> CreateAsync(TaxReduction taxReduction)
		{
			return await BaseCreate(taxReduction).ConfigureAwait(false);
		}
		public async Task<TaxReduction> UpdateAsync(TaxReduction taxReduction)
		{
			return await BaseUpdate(taxReduction, taxReduction.Id).ConfigureAwait(false);
		}
		public async Task<TaxReduction> GetAsync(string id)
		{
			return await BaseGet(id).ConfigureAwait(false);
		}
	}
}
