/*
 * **************************************************************************
 *
 * Copyright (c) The IronSmalltalk Project. 
 *
 * This source code is subject to terms and conditions of the 
 * license agreement found in the solution directory. 
 * See: $(SolutionDir)\License.htm ... in the root of this distribution.
 * By using this source code in any fashion, you are agreeing 
 * to be bound by the terms of the license agreement.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * **************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IronSmalltalk.Tools.ClassLibraryBrowser.Definitions.Implementation
{
    public class Method : Definition<Class>
    {
        public string Selector { get; set; }
        public string Source { get; set; }

        public Method(Class parent)
            : base(parent)
        {
        }

        public Method(Class parent, XmlNode xml, XmlNamespaceManager nsm)
            : base(parent, xml, nsm)
        {
            if (xml == null)
                throw new ArgumentNullException();
            if (nsm == null)
                throw new ArgumentNullException();

            XmlAttribute attr = xml.SelectSingleNode("@selector", nsm) as XmlAttribute;
            if (attr != null)
                this.Selector = attr.Value;

            XmlNode node = xml.SelectSingleNode("si:Source", nsm);
            if (node != null)
                this.Source = node.InnerText.Trim();
        }

        public void Save(XmlWriter xml)
        {
            xml.WriteStartElement("Method");
            xml.WriteAttributeString("selector", this.Selector);

            xml.WriteStartElement("Source");
            xml.WriteString(this.Source);
            xml.WriteEndElement();

            this.Annotations.Save(xml);

            xml.WriteEndElement();
        }
    }
}
