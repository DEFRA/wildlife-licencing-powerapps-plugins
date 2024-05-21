//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// Created via this command line: "C:\Users\vo000010\AppData\Roaming\MscrmTools\XrmToolBox\Plugins\DLaB.EarlyBoundGenerator\crmsvcutil.exe" /url:"https://defra-sdds-dev2.api.crm11.dynamics.com" /namespace:"SDDS.Plugin.EBG" /SuppressGeneratedCodeAttribute /out:"C:\Users\vo000010\AppData\Roaming\MscrmTools\XrmToolBox\Settings\EBG\CrmServiceContext.cs" /servicecontextname:"CrmServiceContext" /codecustomization:"DLaB.CrmSvcUtilExtensions.Entity.CustomizeCodeDomService,DLaB.CrmSvcUtilExtensions" /codegenerationservice:"DLaB.CrmSvcUtilExtensions.Entity.CustomCodeGenerationService,DLaB.CrmSvcUtilExtensions" /codewriterfilter:"DLaB.CrmSvcUtilExtensions.Entity.CodeWriterFilterService,DLaB.CrmSvcUtilExtensions" /namingservice:"DLaB.CrmSvcUtilExtensions.NamingService,DLaB.CrmSvcUtilExtensions" /metadataproviderservice:"DLaB.CrmSvcUtilExtensions.Entity.MetadataProviderService,DLaB.CrmSvcUtilExtensions" /username:"victor.onyebuchi@defradev.onmicrosoft.com" /password:"***************" 
//------------------------------------------------------------------------------

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]
[assembly: System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.95")]

namespace SDDS.Plugin.EBG
{
	
	/// <summary>
	/// Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities.
	/// </summary>
	public partial class CrmServiceContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public CrmServiceContext(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_application"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_application> sdds_applicationSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_application>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_designatedsitename"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_designatedsitename> sdds_designatedsitenameSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_designatedsitename>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_designatedsites"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_designatedsites> sdds_designatedsitesSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_designatedsites>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_licensableaction"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_licensableaction> sdds_licensableactionSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_licensableaction>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_license"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_license> sdds_licenseSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_license>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_licenseActivity"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_licenseActivity> sdds_licenseActivitySet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_licenseActivity>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_licensemethod"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_licensemethod> sdds_licensemethodSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_licensemethod>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_modificationrequest"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_modificationrequest> sdds_modificationrequestSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_modificationrequest>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_planningconsent"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_planningconsent> sdds_planningconsentSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_planningconsent>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="SDDS.Plugin.EBG.sdds_site"/> entities.
		/// </summary>
		public System.Linq.IQueryable<SDDS.Plugin.EBG.sdds_site> sdds_siteSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<SDDS.Plugin.EBG.sdds_site>();
			}
		}
	}
	
	internal sealed class EntityOptionSetEnum
	{
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public static System.Nullable<int> GetEnum(Microsoft.Xrm.Sdk.Entity entity, string attributeLogicalName)
		{
			if (entity.Attributes.ContainsKey(attributeLogicalName))
			{
				Microsoft.Xrm.Sdk.OptionSetValue value = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(attributeLogicalName);
				if (value != null)
				{
					return value.Value;
				}
			}
			return null;
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public static System.Collections.Generic.IEnumerable<T> GetMultiEnum<T>(Microsoft.Xrm.Sdk.Entity entity, string attributeLogicalName)
		
		{
			Microsoft.Xrm.Sdk.OptionSetValueCollection value = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>(attributeLogicalName);
			System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>();
			if (value == null)
			{
				return list;
			}
			list.AddRange(System.Linq.Enumerable.Select(value, v => (T)(object)v.Value));
			return list;
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public static Microsoft.Xrm.Sdk.OptionSetValueCollection GetMultiEnum<T>(Microsoft.Xrm.Sdk.Entity entity, string attributeLogicalName, System.Collections.Generic.IEnumerable<T> values)
		
		{
			if (values == null)
			{
				return null;
			}
			Microsoft.Xrm.Sdk.OptionSetValueCollection collection = new Microsoft.Xrm.Sdk.OptionSetValueCollection();
			collection.AddRange(System.Linq.Enumerable.Select(values, v => new Microsoft.Xrm.Sdk.OptionSetValue((int)(object)v)));
			return collection;
		}
	}
	
	/// <summary>
	/// Attribute to handle storing the OptionSet's Metadata.
	/// </summary>
	[System.AttributeUsageAttribute(System.AttributeTargets.Field)]
	public sealed class OptionSetMetadataAttribute : System.Attribute
	{
		
		/// <summary>
		/// Color of the OptionSetValue.
		/// </summary>
		public string Color { get; set; }
		
		/// <summary>
		/// Description of the OptionSetValue.
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Display order index of the OptionSetValue.
		/// </summary>
		public int DisplayIndex { get; set; }
		
		/// <summary>
		/// External value of the OptionSetValue.
		/// </summary>
		public string ExternalValue { get; set; }
		
		/// <summary>
		/// Name of the OptionSetValue.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionSetMetadataAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the value.</param>
		/// <param name="displayIndex">Display order index of the value.</param>
		/// <param name="color">Color of the value.</param>
		/// <param name="description">Description of the value.</param>
		/// <param name="externalValue">External value of the value.</param>
		[System.Diagnostics.DebuggerNonUserCode()]
		public OptionSetMetadataAttribute(string name, int displayIndex, string color = null, string description = null, string externalValue = null)
		{
			this.Color = color;
			this.Description = description;
			this.ExternalValue = externalValue;
			this.DisplayIndex = displayIndex;
			this.Name = name;
		}
	}
	
	/// <summary>
	/// Extension class to handle retrieving of OptionSetMetadataAttribute.
	/// </summary>
	public static class OptionSetExtension
	{
		
		/// <summary>
		/// Returns the OptionSetMetadataAttribute for the given enum value
		/// </summary>
		/// <typeparam name="T">OptionSet Enum Type</typeparam>
		/// <param name="value">Enum Value with OptionSetMetadataAttribute</param>
		[System.Diagnostics.DebuggerNonUserCode()]
		public static OptionSetMetadataAttribute GetMetadata<T>(this T value)
			where T :  struct, System.IConvertible
		{
			System.Type enumType = typeof(T);
			if (!enumType.IsEnum)
			{
				throw new System.ArgumentException("T must be an enum!");
			}
			System.Reflection.MemberInfo[] members = enumType.GetMember(value.ToString());
			for (int i = 0; (i < members.Length); i++
			)
			{
				System.Attribute attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(members[i], typeof(OptionSetMetadataAttribute));
				if (attribute != null)
				{
					return ((OptionSetMetadataAttribute)(attribute));
				}
			}
			throw new System.ArgumentException("T must be an enum adorned with an OptionSetMetadataAttribute!");
		}
	}
}