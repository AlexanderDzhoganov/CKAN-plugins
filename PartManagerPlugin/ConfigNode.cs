using System;
using System.Collections.Generic;

namespace PartManagerPlugin
{
    public class ConfigNode
    {
        public string id = String.Empty;
        public string name = String.Empty;

        private List<KeyValuePair<string, string>> _values = new List<KeyValuePair<string, string>>();
        private List<ConfigNode> _nodes = new List<ConfigNode>();

        public ConfigNode()
        {
        }

        public ConfigNode(string name)
        {
            this.name = name;
        }

        public ConfigNode[] nodes
        {
            get
            {
                return _nodes.ToArray();
            }
        }

        public KeyValuePair<string, string>[] values
        {
            get
            {
                return _values.ToArray();
            }
        }

        public int CountNodes
        {
            get
            {
                return this._nodes.Count;
            }
        }

        public int CountValues
        {
            get
            {
                return this._values.Count;
            }
        }

        public ConfigNode AddConfigNode(ConfigNode node)
        {
            _nodes.Add(node);
            return node;
        }

        public void AddValue(string key, string value)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key, value);
            _values.Add(kvp);
        }

        public void ClearData()
        {
            _values.Clear();
            _nodes.Clear();
        }

        public void ClearNodes()
        {
            this._nodes.Clear();
        }

        public void ClearValues()
        {
            this._values.Clear();
        }

        public ConfigNode GetNode(string name, int index)
        {
            int currentIndex = 0;
            foreach (ConfigNode node in _nodes)
            {
                if (node.name == name)
                {
                    if (currentIndex == index)
                    {
                        return node;
                    }
                    currentIndex++;
                }
            }
            return null;
        }

        public ConfigNode GetNode(string name)
        {
            foreach (ConfigNode node in _nodes)
            {
                if (node.name == name)
                {
                    return node;
                }
            }
            return null;
        }

        public ConfigNode GetNodeID(string id)
        {
            foreach (ConfigNode node in _nodes)
            {
                if (node.id == id)
                {
                    return node;
                }
            }
            return null;
        }

        public ConfigNode[] GetNodes(string name)
        {
            List<ConfigNode> foundNodes = new List<ConfigNode>();
            foreach (ConfigNode node in _nodes)
            {
                if (node.name == name)
                {
                    foundNodes.Add(node);
                }
            }
            return foundNodes.ToArray();
        }

        public string GetValue(string name, int index)
        {
            int currentIndex = 0;
            foreach (KeyValuePair<string, string> value in _values)
            {
                if (value.Key == name)
                {
                    if (currentIndex == index)
                    {
                        return value.Value;
                    }
                    currentIndex++;
                }
            }
            return null;
        }

        public string GetValue(string name)
        {
            foreach (KeyValuePair<string, string> value in _values)
            {
                if (value.Key == name)
                {
                    return value.Value;
                }
            }
            return null;
        }

        public string[] GetValues(string name)
        {
            List<string> foundValues = new List<string>();
            foreach (KeyValuePair<string, string> value in _values)
            {
                if (value.Key == name)
                {
                    foundValues.Add(value.Value);
                }
            }
            return foundValues.ToArray();
        }

        public string[] GetValuesStartsWith(string name)
        {
            List<string> foundValues = new List<string>();
            foreach (KeyValuePair<string, string> value in _values)
            {
                if (value.Key.StartsWith(name))
                {
                    foundValues.Add(value.Value);
                }
            }
            return foundValues.ToArray();
        }

        public bool HasNode()
        {
            return this._nodes.Count > 0;
        }

        public bool HasNode(string name)
        {
            foreach (ConfigNode cn in _nodes)
            {
                if (cn.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasNodeID(string id)
        {
            foreach (ConfigNode cn in _nodes)
            {
                if (cn.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasValue()
        {
            return this._values.Count > 0;
        }

        public bool HasValue(string name)
        {
            foreach (KeyValuePair<string, string> value in _values)
            {
                if (value.Key == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static ConfigNode Load(string fileFullName)
        {
            return ConfigNodeReader.FileToConfigNode(fileFullName);
        }

        public static void Merge(ConfigNode mergeTo, ConfigNode mergeFrom)
        {
            //TODO: Implement
            throw new NotImplementedException();
        }

        public void RemoveNode(string name)
        {
            foreach (ConfigNode node in _nodes.ToArray())
            {
                if (node.name == name)
                {
                    _nodes.Remove(node);
                    return;
                }
            }
        }

        public void RemoveNodes(string name)
        {
            foreach (ConfigNode node in _nodes.ToArray())
            {
                if (node.name == name)
                {
                    _nodes.Remove(node);
                }
            }
        }

        public void RemoveNodesStartsWith(string name)
        {
            foreach (ConfigNode node in _nodes.ToArray())
            {
                if (node.name.StartsWith(name))
                {
                    _nodes.Remove(node);
                }
            }
        }

        public void RemoveValue(string name)
        {
            foreach (KeyValuePair<string, string> value in _values.ToArray())
            {
                if (value.Key == name)
                {
                    _values.Remove(value);
                    return;
                }
            }
        }

        public void RemoveValues(string[] names)
        {
            foreach (string name in names)
            {
                RemoveValues(name);
            }
        }

        public void RemoveValues(string name)
        {
            foreach (KeyValuePair<string, string> value in _values.ToArray())
            {
                if (value.Key == name)
                {
                    _values.Remove(value);
                }
            }
        }

        public void RemoveValuesStartsWith(string startsWith)
        {
            foreach (KeyValuePair<string, string> value in _values.ToArray())
            {
                if (value.Key.StartsWith(name))
                {
                    _values.Remove(value);
                }
            }
        }

        public bool SetNode(string name, ConfigNode newNode, int index)
        {
            //This is probably incorrect, but it already doesn't make sense in the first place...
            ConfigNode oldNode = GetNode(name, index);
            if (oldNode == null)
            {
                //Index doesn't exist - Add it to the node list anyway.
                _nodes.Add(newNode);
            }
            else
            {
                //Index exists - Replace the node
                int oldPos = _nodes.IndexOf(oldNode);
                _nodes[oldPos] = newNode;
            }
            return true;
        }

        public bool SetNode(string name, ConfigNode newNode)
        {
            return SetNode(name, newNode, 0);
        }

        public bool SetValue(string name, string newValue, int index)
        {
            //This is probably incorrect, but it already doesn't make sense in the first place...
            KeyValuePair<string, string> oldValue = new KeyValuePair<string, string>(name, GetValue(name, index));
            if (oldValue.Value == null)
            {
                //Index doesn't exist - Add it to the node list anyway.
                _values.Add(new KeyValuePair<string, string>(name, newValue));
            }
            else
            {
                //Index exists - Replace the node
                int oldPos = _values.IndexOf(oldValue);
                _values[oldPos] = new KeyValuePair<string, string>(name, newValue);
            }
            return true;
        }

        public bool SetValue(string name, string newValue)
        {
            return SetValue(name, newValue, 0);
        }

     
    }
}
