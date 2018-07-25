using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Creates a mask for a string property.
    /// </summary>
    public class FWMaskAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Gets or sets the mask as reversible.
        /// </summary>
        public bool Reverse { get; set; }

        /// <summary>
        /// Gets the value mask.
        /// </summary>
        public string Mask { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Framework.DataAnnotation.ViewModel.FWMask class.
        /// </summary>
        /// <param name="mask">The mask value.</param>
        public FWMaskAttribute(string mask)
        {
            Mask = mask;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.DataAnnotation.ViewModel.FWMask class.
        /// </summary>
        /// <param name="mask">The mask option.</param>
        public FWMaskAttribute(FWMasks mask)
        {
            switch(mask)
            {
                case FWMasks.Cpf:
                    Mask = "000.000.000-00";
                    break;
            }
        }
    }

    /// <summary>
    /// Enumerates the framework known masks.
    /// </summary>
    public enum FWMasks
    {
        /// <summary>
        /// Cpf mask.
        /// </summary>
        Cpf
    }
}
