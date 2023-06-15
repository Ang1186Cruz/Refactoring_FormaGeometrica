using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Resources
{

    public class ResourceHelpers<T>
    {
        private readonly CulturePreference culture;
        private readonly ResourceManager resourceManager;

        public ResourceHelpers(CulturePreference culture)
        {
            this.culture = culture;
            this.resourceManager = new ResourceManager(typeof(T));

        }
        public string GetValue(string key)
        {
            return resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(GetCultureString()));
        }

        private string GetCultureString()
        {
            switch(culture)
            {
                case CulturePreference.English: return GeneralConstants.ENGLISH_CULTURE;
                case CulturePreference.Español: return GeneralConstants.ESPAÑOL_CULTURE;
                case CulturePreference.Italiano: return GeneralConstants.ITALIANO_CULTURE;
            }
            return GeneralConstants.ESPAÑOL_CULTURE;
        }
    }
}
