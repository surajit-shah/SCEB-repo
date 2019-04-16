using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;

// This is an example of a computed field in Sitecore.  It is added to the index as the item is saved just like standard fields.
// I wanted an easy way to determine if an Item had a layout specified so I can exclude it from results if needed on the public site.
namespace EBPOC.Web.Configuration.Search.ComputedFields
{
  public class ArticleCategoriesComputedField : IComputedIndexField
  {
    public string FieldName { get; set; }

    public string ReturnType { get; set; }

    public object ComputeFieldValue(IIndexable indexable)
    {
     
        Item i = ((Item)(indexable as SitecoreIndexableItem));
                if (i != null && i.TemplateName == "EBArticleTemplate")
                {
                    MultilistField articleCategories = i.Fields["Categories"];
                    List<string> list = new List<string>();
                    foreach (ID id in articleCategories.TargetIDs)
                    {
                        Item category = i.Database.GetItem(id);
                        if (articleCategories != null)
                        {
                            list.Add(category.DisplayName);

                        }
                      //  return list;
                    }
                    return list;
                   
                }
                return null;

           
    }
  }
}