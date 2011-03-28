using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OxigenIIPresentation.CommandHandlers
{
  public class QueryStringParameterValueConfiguration : ConfigurationSection
  {
    [ConfigurationProperty("queryStringParameterSet", IsRequired = false)]
    public QueryStringParameterCollection QueryStringParameters
    {
      get { return this["queryStringParameterSet"] as QueryStringParameterCollection; }
    }
  }

  public class QueryStringParameterCollection : ConfigurationElementCollection
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new ParameterValueElement();
    }

    protected override Object GetElementKey(ConfigurationElement element)
    {
      return ((ParameterValueElement)element).Name;
    }

    public new string AddElementName
    {
      get { return base.AddElementName; }
      set { base.AddElementName = value; }
    }

    public new string ClearElementName
    {
      get { return base.ClearElementName; }
      set { base.AddElementName = value; }
    }

    public new string RemoveElementName
    {
      get { return base.RemoveElementName; }
    }

    public new int Count
    {
      get { return base.Count; }
    }

    public ParameterValueElement this[int index]
    {
      get
      {
        return (ParameterValueElement)BaseGet(index);
      }
      set
      {
        if (BaseGet(index) != null)
          BaseRemoveAt(index);

        BaseAdd(index, value);
      }
    }

    public new ParameterValueElement this[string Name]
    {
      get { return (ParameterValueElement)BaseGet(Name); }
    }

    public int IndexOf(ParameterValueElement parameterValueElement)
    {
      return BaseIndexOf(parameterValueElement);
    }

    public void Add(ParameterValueElement parameterValueElement)
    {
      BaseAdd(parameterValueElement);
    }

    protected override void BaseAdd(ConfigurationElement element)
    {
      BaseAdd(element, false);
    }

    public void Remove(ParameterValueElement parameterValueElement)
    {
      if (BaseIndexOf(parameterValueElement) >= 0)
        BaseRemove(parameterValueElement.Name);
    }

    public void RemoveAt(int index)
    {
      BaseRemoveAt(index);
    }

    public void Remove(string name)
    {
      BaseRemove(name);
    }

    public void Clear()
    {
      BaseClear();
    }
  }

  public class ParameterValueElement : ConfigurationElement
  {
    [ConfigurationProperty("name", DefaultValue = "a", IsRequired = true, IsKey = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
    public string Name
    {
      get { return this["name"] as string; }
    }

    [ConfigurationProperty("value", DefaultValue = "a", IsRequired = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
    public string Value
    {
      get { return this["value"] as string; }
    }
  }
}

