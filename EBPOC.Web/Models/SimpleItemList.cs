using System.Collections.Generic;

namespace EBPOC.Web.Models
{
    public class SimpleItemList
  {
    public string Title { get; protected set; }
    public IEnumerable<SimpleItem> Items { get; protected set; }

    public SimpleItemList(string title, IEnumerable<SimpleItem> items)
    {
      Title = title;
      Items = items;
    }
  }
}