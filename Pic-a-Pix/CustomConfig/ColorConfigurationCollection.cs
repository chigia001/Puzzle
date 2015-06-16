using System.Configuration;

namespace Pic_a_Pix.CustomConfig
{
    class ColorConfigurationCollection : ConfigurationElementCollection
    {
        public void Add(ColorConfigurationElement customElement)
        {
            BaseAdd(customElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ColorConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ColorConfigurationElement)element).HintCharacter;
        }

        public ColorConfigurationElement this[int Index]
        {
            get
            {
                return (ColorConfigurationElement)BaseGet(Index);
            }
            set
            {
                if (BaseGet(Index) != null)
                {
                    BaseRemoveAt(Index);
                }
                BaseAdd(Index, value);
            }
        }

        new public ColorConfigurationElement this[string HintCharacter]
        {
            get
            {
                return (ColorConfigurationElement)BaseGet(HintCharacter);
            }
        }

        public int indexof(ColorConfigurationElement element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(ColorConfigurationElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.HintCharacter);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string HintCharacter)
        {
            BaseRemove(HintCharacter);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
