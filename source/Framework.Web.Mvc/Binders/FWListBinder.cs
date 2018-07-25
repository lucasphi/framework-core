using Framework.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Binders
{
    /// <summary>
    /// Extends the CollectionModelBinder class to add the IFWDetailList behavior.
    /// </summary>
    /// <typeparam name="TElement">Type of elements in the collection.</typeparam>
    public class FWListBinder<TElement> : CollectionModelBinder<TElement>
    {
        /// <summary>
        /// Creates a new <see cref="FWListBinder&lt;TElement&gt;" />
        /// </summary>
        /// <param name="elementBinder">The Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder for binding elements</param>
        public FWListBinder(IModelBinder elementBinder)
            : base(elementBinder)
        { }

        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        public override Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var response = base.BindModelAsync(bindingContext);

            if (bindingContext.Result.Model != null)
            {
                var deleted = bindingContext.HttpContext.Request.Form[$"__{bindingContext.ModelName}"].ToString();

                if (!string.IsNullOrEmpty(deleted))
                {
                    // Casts the removed indexes into a list
                    var deletedIndexes = deleted.Split(',').Select(f => int.Parse(f));

                    if (deletedIndexes != null)
                    {
                        // Sets the IFWList removed items
                        var model = bindingContext.Result.Model as IFWList;
                        var originalCount = model.Count;
                        model.RemoveItems(deletedIndexes);

                        // Removes the same amount of items removed from the validation state. All the extra validation items would be left unvalidated,
                        // making the modelstate invalid.
                        var maxLoop = originalCount - deletedIndexes.Count();
                        for (int i = originalCount; i > maxLoop; i--)
                        {
                            var stateKeys = bindingContext.ModelState.Where(f => f.Key.StartsWith($"{bindingContext.ModelName}[{i - 1}].") ||
                                                                                    f.Key.Contains($".{bindingContext.ModelName}[{i - 1}]."));
                            if (stateKeys != null)
                            {
                                foreach (var stateKey in stateKeys)
                                {
                                    bindingContext.ModelState.Remove(stateKey.Key);
                                }
                            }
                        }
                    }
                }
            }

            return response;
        }
    }
}