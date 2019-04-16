using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;

namespace EBPOC.Web.Configuration.Search.ComputedFields
{
  public class HasPresentationComputedField : IComputedIndexField
  {
    public string FieldName { get; set; }

    public string ReturnType { get; set; }

    public object ComputeFieldValue(IIndexable indexable)
    {
      Item i = ((Item)(indexable as SitecoreIndexableItem));
      if (i != null && i.Visualization.Layout != null) return true;
      return null;
    }
  }
}