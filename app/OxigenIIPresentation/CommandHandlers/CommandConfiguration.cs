using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OxigenIIPresentation.CommandHandlers
{
  public class ChannelCommandConfigurationSection : ConfigurationSection
  {
    [ConfigurationProperty("channelCommands")]
    public ChannelCommandElementCollection ChannelCommands
    {
      get { return this["channelCommands"] as ChannelCommandElementCollection; }
    }
  }
  
  public class ChannelCommandElementCollection : ConfigurationElementCollection
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new ChannelCommandElement();
    }

    protected override Object GetElementKey(ConfigurationElement element)
    {
      return ((ChannelCommandElement)element).Name;
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

    public ChannelCommandElement this[int index]
    {
      get
      {
          return (ChannelCommandElement)BaseGet(index);
      }
      set
      {
        if (BaseGet(index) != null)
           BaseRemoveAt(index);

        BaseAdd(index, value);
      }
    }

    public new ChannelCommandElement this[string Name]
    {
      get { return (ChannelCommandElement)BaseGet(Name); }
    }

    public int IndexOf(ChannelCommandElement commandElement)
    {
      return BaseIndexOf(commandElement);
    }

    public void Add(ChannelCommandElement commandElement)
    {
      BaseAdd(commandElement);
    }

    protected override void BaseAdd(ConfigurationElement element)
    {
      BaseAdd(element, false);
    }

    public void Remove(ChannelCommandElement commandElement)
    {
      if (BaseIndexOf(commandElement) >= 0)
        BaseRemove(commandElement.Name);
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

  public class ChannelCommandElement : ConfigurationElement
  {
    [ConfigurationProperty("name", DefaultValue = "a", IsRequired = true, IsKey = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 100)]
    public string Name
    {
      get { return this["name"] as string; }
    }

    [ConfigurationProperty("type", DefaultValue="a", IsRequired = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 100)]
    public string Type
    {
      get { return this["type"] as string; }
    }
  }
}