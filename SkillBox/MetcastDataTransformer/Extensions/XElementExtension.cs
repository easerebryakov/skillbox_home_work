using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace MetcastDataTransformer
{
	public static class XElementExtension
	{
		/// <summary>
		/// Получить элементы с определенным именем
		/// </summary>
		/// <param name="elements">Целевая коллекция элементов</param>
		/// <param name="name">Имя элемента</param>
		/// <returns></returns>
		public static List<XElement> GetElementsByName(this List<XElement> elements, string name)
		{
			return elements.Where(e => e.Name.LocalName == name).ToList();
		}

		/// <summary>
		/// Скопировать атрибуты в целевой элемент
		/// </summary>
		/// <param name="targetElement">Целевой элемент</param>
		/// <param name="sourceAttributes">Атрибуты для копирования</param>
		/// <returns></returns>
		public static XElement CopyOfAttributes(this XElement targetElement, params XAttribute[] sourceAttributes)
		{
			if (sourceAttributes == null)
				return targetElement;
			foreach (var sourceAttribute in sourceAttributes)
			{
				if (sourceAttribute == null)
					continue;
				var nameAtr = sourceAttribute.Name;
				if (targetElement.Attribute(nameAtr) == null)
					targetElement.Add(new XAttribute(sourceAttribute));
			}

			return targetElement;
		}

		/// <summary>
		/// Скопировать элементы в целевой элемент
		/// </summary>
		/// <param name="targetElement">Целевой элемент</param>
		/// <param name="sourceElements">Элементы для копирования</param>
		/// <returns></returns>
		public static XElement CopyOfElements(this XElement targetElement, params XElement[] sourceElements)
		{
			if (sourceElements == null)
				return targetElement;
			foreach (var sourceElement in sourceElements)
				targetElement.Add(new XElement(sourceElement));
			return targetElement;
		}

		/// <summary>
		/// Добавить новый атрибут в элемент
		/// </summary>
		/// <param name="targetElement">Целевой элемент</param>
		/// <param name="newAttrName">Имя нового атрибута</param>
		/// <param name="newAttrValue">Значение нового атрибута</param>
		/// <returns></returns>
		public static XElement AddAttribute(this XElement targetElement, XName newAttrName, string newAttrValue)
		{
			try
			{
				if (targetElement.Attribute(newAttrName) != null || newAttrValue == null)
					return targetElement;
				targetElement.Add(new XAttribute(newAttrName, newAttrValue));
				return targetElement;
			}
			catch (Exception e)
			{
				throw new Exception($"Could not add attribute with name '{newAttrName}'", e);
			}
		}

		/// <summary>
		/// Добавить новый элемент к целевому элементу. Вернуть целевой элемент.
		/// </summary>
		/// <param name="targetElement">Целевой элемент</param>
		/// <param name="newElName">Имя нового элемент</param>
		/// <param name="value">Значение нового элемента</param>
		/// <returns></returns>
		public static XElement AddElementAndReturnSelf(this XElement targetElement, XName newElName, string value = null)
		{
			try
			{
				var newEl = new XElement(newElName);
				if (value != null)
					newEl.Value = value;
				targetElement.Add(newEl);
				return targetElement;
			}
			catch (Exception e)
			{
				throw new Exception($"Could not add element with name '{newElName}'", e);
			}
		}

		/// <summary>
		/// Добавить новый элемент к целевому элементу. Вернуть новый элемент.
		/// </summary>
		/// <param name="targetElement">Целевой элемент</param>
		/// <param name="newElName">Имя нового элемент</param>
		/// <param name="value">Значение нового элемента</param>
		/// <returns></returns>
		public static XElement AddElementAndReturnHim(this XElement targetElement, XName newElName, string value = null)
		{
			try
			{
				var newEl = new XElement(newElName);
				if (value != null)
					newEl.Value = value;
				targetElement.Add(newEl);
				return newEl;
			}
			catch (Exception e)
			{
				throw new Exception($"Could not add element with name '{newElName}'", e);
			}
		}

		/// <summary>
		/// Взять значение атрибута или вернуть пустое значение
		/// </summary>
		/// <param name="targetElement"></param>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		[NotNull]
		public static string GetAttributeValueOrEmpty(this XElement targetElement, XName attributeName)
		{
			var attribute = targetElement.Attribute(attributeName);
			return attribute?.Value ?? string.Empty;
		}

		/// <summary>
		/// Взять значение атрибута или вернуть null
		/// </summary>
		/// <param name="targetElement"></param>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		[CanBeNull]
		public static string GetAttributeValue(this XElement targetElement, XName attributeName)
		{
			var attribute = targetElement.Attribute(attributeName);
			return attribute?.Value;
		}

		/// <summary>
		/// Проверить наличие атрибута с именем в элементе
		/// </summary>
		/// <param name="targetElement"></param>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		public static bool HasAttribute(this XElement targetElement, XName attributeName)
		{
			var attr = targetElement.Attribute(attributeName);
			return attr != null;
		}

		/// <summary>
		/// Добавить новый атрибут после целевого атрибута
		/// </summary>
		/// <param name="targetElement"></param>
		/// <param name="newAttrName">Имя нового атрибута</param>
		/// <param name="newAttrValue">Значение нового атрибута</param>
		/// <param name="targetAttrName">Имя атрибута, после которого надо добавить атрибут</param>
		/// <returns></returns>
		public static XElement AddAttributeAfterTargetAttribute(this XElement targetElement, XName newAttrName, string newAttrValue, XName targetAttrName)
		{
			if (string.IsNullOrEmpty(newAttrName.ToString()) || newAttrValue == null)
				return targetElement;
			if (!targetElement.HasAttribute(targetAttrName))
				targetElement.AddAttribute(newAttrName, newAttrValue);
			else
			{
				var attributes = targetElement.Attributes();
				var newCollectionAttrs = new List<XAttribute>();
				foreach (var attr in attributes)
				{
					newCollectionAttrs.Add(attr);
					if (attr.Name == targetAttrName)
						newCollectionAttrs.Add(new XAttribute(newAttrName, newAttrValue));
				}
				targetElement.ReplaceAttributes(newCollectionAttrs);
			}
			return targetElement;
		}
	}
}