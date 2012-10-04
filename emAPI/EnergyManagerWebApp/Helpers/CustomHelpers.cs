
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * CustomHelpers.cs 
 * Code source: Handwritten
 */
		

using System.Web.Mvc;

namespace MvcHtmlHelpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString status(bool isChecked, bool consumptionIsValid, bool costIsValid)
        {
            string result;

            if (isChecked)
            {
                if (consumptionIsValid && costIsValid)
                {
                    result = "2"; ///a tick since invoice is valid
                }
                else if (consumptionIsValid | costIsValid)
                {
                    result = "!"; ///an exclamation mark since partially valid
                }
                else
                {
                    result = "D"; ///a cross since it is invalid
                }
            }
            else
            {
                result = "?";  ///a question mark since validity has not been assessed
            }

            return MvcHtmlString.Create(result);
        }
    }
}